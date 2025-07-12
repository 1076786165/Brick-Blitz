using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class BrickController : MonoBehaviourSingleton<BrickController>
{
    // object绑定
    public Transform root;
    public List<GameObject> brick_slots;
    
    //棋盘相关
    private int[,] _board_map;
    private int _slots_size = 0;
    private Dictionary<string , BrickGroup> _brick_slots;
    private int _setdown_flag = 0;
    private List<BrickGroup> _frozen_brick_groups;
    
    // 检测相关
    private BrickCursor _brick_cursor;
    private Vector2Int _last_Detect_coor;

    //运行时数据
    private int _create_id = 0;

    protected override void Start()
    {
        _board_map = new int[Config.BOARD_SIZE.x , Config.BOARD_SIZE.y];
        _slots_size = brick_slots.Count;
        _brick_slots = new Dictionary<string , BrickGroup>();
        for(int i = 0 ; i < _slots_size ; i++)
        {
            UpdateSlotsFlag(i , null);
        }
        _frozen_brick_groups = new List<BrickGroup>();

        StartCoroutine(createStratBricks());
    }

    private void UpdateSlotsFlag(int idx , BrickGroup brick_group){
        _brick_slots["slot_" + idx.ToString()] = brick_group;
    }

    private BrickGroup GetSlotsFlag(int idx){
        return _brick_slots["slot_" + idx.ToString()];
    }

    private bool IsSlotsEmpty(){
        for(int i = 0 ; i < _slots_size ; i++)
        {
            if(GetSlotsFlag(i) != null){
                return false;
            }
        }
        return true;
    }

    public IEnumerator createStratBricks(){
        yield return null;

        createLiveBrickGroup(0);
        createLiveBrickGroup(1);
        createLiveBrickGroup(2);
    }

    public void fillBrickSlost(){
        for(int i = 0 ; i < _slots_size ; i++)
        {
            if(GetSlotsFlag(i) == null){
                createLiveBrickGroup(i);
            }
        }
    }

    public void createLiveBrickGroup(int slot_id)
    {
        Debug.Assert(GetSlotsFlag(slot_id) == null , "slot is already occupied！ " + slot_id);
        BrickGroup brick_group = Instantiate(BrickDef.Instance._brick_group_prefab , root).GetComponent<BrickGroup>();
        brick_group.InitInSlot(BrickDef.Instance.GetRandomBrickDef() , _create_id++ , slot_id);
        UpdateSlotsFlag(slot_id , brick_group);
    }

    public void createLooseBrickGroup(BrickInfo brick_info , Vector2Int coor)
    {
        // BrickGroup brick_group = Instantiate(BrickDef.Instance._brick_group_prefab , root).GetComponent<BrickGroup>();
        // brick_group.Init(brick_info , coor , _create_id++ , 1);
    }

    public void CreateBrickCursor(BrickGroup brick_group){
        if(_brick_cursor != null){
            Destroy(_brick_cursor.gameObject);
            _brick_cursor = null;
        }
        _brick_cursor = Instantiate(BrickDef.Instance._brick_cursor_prefab , root).GetComponent<BrickCursor>();
        _brick_cursor.InitWithBrickGroup(brick_group);
    }

    public void onBrickDrag(BrickGroup brick_group , PointerEventData eventData , Vector2 brick_group_postion){
        // 检测玩家拖动位置是否合法 并更新BrickCursor的显示
        Vector2Int brick_group_coor = CalcTools.convertPostionToCoor(brick_group_postion);
        if(brick_group_coor == _last_Detect_coor){
            return;
        }
        _last_Detect_coor = brick_group_coor;
        DetectResult detect_result = new VirtualBrick(brick_group , brick_group_coor).DetectingCoor(_board_map);
        if(_brick_cursor == null || _brick_cursor._group_id != brick_group._group_id){
            CreateBrickCursor(brick_group);
        }
        _brick_cursor.UpdateCoorWithDetect(detect_result);
    }

    public void onBrickDragEnd(BrickGroup brick_group , PointerEventData eventData){
        if (_brick_cursor != null && _brick_cursor.IsCursorIllegal(brick_group)){
            BrickGroupSetDown(brick_group , _brick_cursor._detect_result.coor);
        }
        else{
            brick_group.BackToSlot();
        }
        
    }

    public void BrickGroupSetDown(BrickGroup brick_group , Vector2Int coor){
        brick_group.SetDown(coor);
        brick_group.EachBrick((Brick brick) => {
            Debug.Assert(_board_map[brick._global_coor.x , brick._global_coor.y] == 0 , "brick position is already occupied！ " + brick._global_coor.ToString());

            _board_map[brick._global_coor.x , brick._global_coor.y] = 1;
            // Debug.Log("brick set down at " + brick._global_coor.ToString());
            return false;
        });

        RoundController.Instance.OnBrickSet(brick_group.GetBrickNum());

        _frozen_brick_groups.Add(brick_group);

        Destroy(_brick_cursor.gameObject);
        _brick_cursor = null;

        UpdateSlotsFlag(brick_group._slot_id , null);
        _setdown_flag++;
        if (IsSlotsEmpty()){
            fillBrickSlost();
        }
        

        CheckEliminate();

        CheckBoardFull();
    }

    public void CheckBoardFull(){
        bool is_full = true;
        for(int idx = 0 ; idx < _slots_size ; idx++)
        {
            BrickGroup bg = GetSlotsFlag(idx);
            if (bg != null){
                for (int i = 0 ; i < Config.BOARD_SIZE.y ; i++){
                    for (int j = 0 ; j < Config.BOARD_SIZE.x ; j++){
                        DetectResult detect_result = new VirtualBrick(bg , new Vector2Int(j , i)).DetectingCoor(_board_map);
                        if(detect_result.is_illegal){
                            is_full = false;
                            break;
                        }
                    }
                }
                
            }
        }

        Debug.Log("is_full = " + is_full);
    }

    public void CheckEliminate(){
        List<int> eliminate_list_x = new List<int>();

        for (int i = 0 ; i < Config.BOARD_SIZE.y ; i++){
            int y_connt = 0;
            for (int j = 0 ; j < Config.BOARD_SIZE.x ; j++){
                if(_board_map[j , i] == 1){
                    y_connt++;
                }
                if(y_connt >= Config.BOARD_SIZE.x){
                    eliminate_list_x.Add(i);
                }
            }
        }


        List<int> eliminate_list_y = new List<int>();
        for (int i = 0 ; i < Config.BOARD_SIZE.x ; i++){
            int y_connt = 0;
            for (int j = 0 ; j < Config.BOARD_SIZE.y ; j++){
                if(_board_map[i , j] == 1){
                    y_connt++;
                }
                if(y_connt >= Config.BOARD_SIZE.y){
                    eliminate_list_y.Add(i);
                }
            }
        }

        foreach(BrickGroup bg in _frozen_brick_groups){
            bg.checkEliminateX(eliminate_list_x , (Vector2Int global_coor)=>{
                _board_map[global_coor.x , global_coor.y] = 0;
            });

            bg.checkEliminateY(eliminate_list_y , (Vector2Int global_coor)=>{
                _board_map[global_coor.x , global_coor.y] = 0;
            });
        }

        RoundController.Instance.OnEliminated(eliminate_list_x.Count + eliminate_list_y.Count);
    }

    public void EachBoardCoor(Action<int , int> act){
        for(int i = 0 ; i < Config.BOARD_SIZE.x ; i++){
            for(int j = 0 ; j < Config.BOARD_SIZE.y ; j++){
                act(j , i);
            }
        }
    }

    public Vector2 GetSlotPosition(int slot_id){
        return brick_slots[slot_id].transform.localPosition;
    }
}
