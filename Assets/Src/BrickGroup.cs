using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using System;

public class BrickGroup : MonoBehaviour , IDragHandler , IEndDragHandler , IBeginDragHandler
{
    public BrickInfo _brick_info{get;set;}
    private List<Brick> _bricks;
    private Vector2Int _coor{get;set;}
    private Vector2Int _begin_coor;
    private bool _is_frozen{get;set;}

    public void Init(BrickInfo brick_info , Vector2Int coor){
        _brick_info = brick_info;
        setCoor(coor);

        _bricks = new List<Brick>();
        _brick_info.eachBrickInfo((int i , int j) => {
            GameObject o_brick = Instantiate(BrickDef.Instance._brick_prefab , transform);
            Brick brick = o_brick.GetComponent<Brick>();
            brick.setCoor(_coor , new Vector2Int(i , j));

            _bricks.Add(brick);
        });
    }

    public void setCoor(Vector2Int coor){
        _coor = coor;

        updateCoor();
    }

    public void updateCoor(){
        gameObject.transform.localPosition = new Vector2(_coor.x * Config.BRICK_SIZE.x , _coor.y * Config.BRICK_SIZE.y);
    }

    public void eachBrick(Func<Brick , bool> act){
        foreach(Brick brick in _bricks){
            if(act(brick)){
                return;
            }
        }
    }

    public void setBeginCoor(Vector2Int coor){
        _begin_coor = coor;
    }

    public void restoreBeginCoor(){
        setCoor(_begin_coor);
    }

    // DARG
    public void OnBeginDrag(PointerEventData eventData){
        if(_is_frozen){
            return;
        }
        
        setBeginCoor(_coor);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(_is_frozen){
            return;
        }
        transform.position = Input.mousePosition + new Vector3(-168 / 2 , 100 , 0);

        BrickController.Instance.onBrickDrag(this , eventData , CalcTools.convertPostionToCoor(transform.localPosition));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // bool is_legal_coor = BrickController.Instance.onBrickDragEnd(this , eventData);
        // if (!is_legal_coor){
        //     setCoor((Vector2Int)_begin_coor);
        // }
    }

}
