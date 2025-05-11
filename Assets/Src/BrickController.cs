using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class BrickController : MonoBehaviourSingleton<BrickController>
{
    public Transform root;
    private int[,] _board_map;
    private BrickCursor _brick_cursor;

    protected override void Start()
    {
        _board_map = new int[Config.BOARD_SIZE.x , Config.BOARD_SIZE.y];

        StartCoroutine(test());
    }

    public IEnumerator test(){
        yield return null;
        GameObject o_brick_group = Instantiate(BrickDef.Instance._brick_group_prefab , root);
        BrickGroup brick_group = o_brick_group.GetComponent<BrickGroup>();
        brick_group.Init(BrickDef.Instance.GetBrickDef("L1") , new Vector2Int(1 , 2));

    }

    public void createBrickCursor(BrickGroup brick_group){
        _brick_cursor = Instantiate(BrickDef.Instance._brick_cursor_prefab , root).GetComponent<BrickCursor>();
        _brick_cursor.Init(brick_group._brick_info , new Vector2Int(0 , 0));
        // _brick_cursor.gameObject.transform.SetAsLastSibling();
    }

    public void onBrickDrag(BrickGroup brick_group , PointerEventData eventData , Vector2Int cursor_coor){
        if(_brick_cursor == null){
            createBrickCursor(brick_group);
        }
        brick_group.gameObject.transform.SetAsLastSibling();
        Debug.Log(cursor_coor.ToString());
        _brick_cursor.updateCoor(cursor_coor);
    }

    public bool onBrickDragEnd(BrickGroup brick_group , PointerEventData eventData){
        // Vector2Int? v_coor = CalcTools.convertPostionToCoor(transform.localPosition);
        // if(v_coor.HasValue){
        //     Vector2Int coor = (Vector2Int)v_coor;
        //     VirtualBrick virtual_brick = new VirtualBrick(brick_group._brick_info , coor);

        // }else{
        //     return false;
        // }
        

        // List<Vector2Int> illegal_brick = checkBrickGroupFit(brick_group);
        // if(illegal_brick.Count > 0){
        //     return false;
        // }else{
        //     // brick_group.setCoor();
        //     return true;
        // }
        return true;
    }

    public List<Vector2Int> checkBrickGroupFit(BrickGroup brick_group){
        List<Vector2Int> illegal_brick = new List<Vector2Int>();

        brick_group.eachBrick((Brick brick) => {
            if(_board_map[brick._global_coor.x , brick._global_coor.y] == 1 || brick._global_coor.x < 0 || brick._global_coor.x >= Config.BOARD_SIZE.x || brick._global_coor.y < 0 || brick._global_coor.y >= Config.BOARD_SIZE.y){
                illegal_brick.Add(brick._local_coor);
                return true;
            }
            return false;
        });
        return illegal_brick;
    }

    public void setBrickGroup(BrickGroup brick_group){
        brick_group.eachBrick((Brick brick) => {
            Debug.Assert(_board_map[brick._global_coor.x , brick._global_coor.y] == 0 , "brick position is already occupied！ " + brick._global_coor.ToString());

            _board_map[brick._global_coor.x , brick._global_coor.y] = 1;
            return false;
        });
    }

    public void eachBoardCoor(Action<int , int> act){
        for(int i = 0 ; i < Config.BOARD_SIZE.x ; i++){
            for(int j = 0 ; j < Config.BOARD_SIZE.y ; j++){
                act(j , i);
            }
        }
    }
}
