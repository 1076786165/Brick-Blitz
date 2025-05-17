using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class BrickController : MonoBehaviourSingleton<BrickController>
{
    public Transform root;
    private int[,] _board_map;

    // 检测相关
    private BrickCursor _brick_cursor;
    private Vector2Int _last_Detect_coor;

    //运行时数据
    private int _create_id = 0;

    protected override void Start()
    {
        _board_map = new int[Config.BOARD_SIZE.x , Config.BOARD_SIZE.y];

        StartCoroutine(test());
    }

    public IEnumerator test(){
        yield return null;

        createBrickGroup(BrickDef.Instance.GetBrickDef("L1") , new Vector2Int(1 , 2));
    }

    public void createBrickGroup(BrickInfo brick_info , Vector2Int coor)
    {
        BrickGroup brick_group = Instantiate(BrickDef.Instance._brick_group_prefab , root).GetComponent<BrickGroup>();
        brick_group.Init(brick_info , coor , _create_id++);
    }

    public void CreateBrickCursor(BrickGroup brick_group){
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
        if(_brick_cursor == null){
            CreateBrickCursor(brick_group);
        }
        _brick_cursor.UpdateCoorWithDetect(detect_result);
    }

    public void onBrickDragEnd(BrickGroup brick_group , PointerEventData eventData){
        if (_brick_cursor != null && _brick_cursor.IsCursorIllegal(brick_group)){
            BrickGroupSetDown(brick_group , _brick_cursor._detect_result.coor);
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
        createBrickGroup(BrickDef.Instance.GetBrickDef("L1") , new Vector2Int(1 , 2));
    }

    public void eachBoardCoor(Action<int , int> act){
        for(int i = 0 ; i < Config.BOARD_SIZE.x ; i++){
            for(int j = 0 ; j < Config.BOARD_SIZE.y ; j++){
                act(j , i);
            }
        }
    }
}
