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
    private int[] _slot_flags;

    // 检测相关
    private BrickCursor _brick_cursor;
    private Vector2Int _last_Detect_coor;

    //运行时数据
    private int _create_id = 0;

    protected override void Start()
    {
        _board_map = new int[Config.BOARD_SIZE.x , Config.BOARD_SIZE.y];
        _slots_size = brick_slots.Count;
        _slot_flags = new int[_slots_size];


        StartCoroutine(test());
    }

    public IEnumerator test(){
        yield return null;

        createLiveBrickGroup(BrickDef.Instance.GetBrickDef("L1") , 0);
        createLiveBrickGroup(BrickDef.Instance.GetBrickDef("O") , 1);
        createLiveBrickGroup(BrickDef.Instance.GetBrickDef("T") , 2);
    }

    public void createLiveBrickGroup(BrickInfo brick_info , int slot_id)
    {
        Debug.Assert(_slot_flags[slot_id] == 0 , "slot is already occupied！ " + slot_id);
        BrickGroup brick_group = Instantiate(BrickDef.Instance._brick_group_prefab , root).GetComponent<BrickGroup>();
        brick_group.InitInSlot(brick_info , _create_id++ , slot_id);
        _slot_flags[slot_id] = 1;
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
            Debug.Log("brick set down at " + brick._global_coor.ToString());
            return false;
        });

        Destroy(_brick_cursor.gameObject);
        _brick_cursor = null;

        _slot_flags[brick_group._slot_id] = 0;
        createLiveBrickGroup(BrickDef.Instance.GetBrickDef("L1") , brick_group._slot_id);
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
