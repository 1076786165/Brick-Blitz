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
    public Vector2Int _coor{get;set;}
    public int _group_id{get;set;}
    public List<Brick> _bricks{get;set;}

    private Vector2Int _begin_coor;
    private bool _is_frozen{get;set;}

    public void Init(BrickInfo brick_info , Vector2Int coor , int group_id){
        _brick_info = brick_info;
        _group_id = group_id;

        _bricks = new List<Brick>();
        _brick_info.EachBrickInfo((int i , int j) => {
            GameObject o_brick = Instantiate(BrickDef.Instance._brick_prefab , transform);
            Brick brick = o_brick.GetComponent<Brick>();
            brick.setCoor(_coor , new Vector2Int(i , j));

            _bricks.Add(brick);
        });
        
        setCoor(coor);
    }

    public void setCoor(Vector2Int coor){
        _coor = coor;

        updateCoor();
    }

    public void updateCoor(){
        gameObject.transform.localPosition = CalcTools.convertCoorToPosition(_coor);
        EachBrick((Brick brick) => {
            brick.setGroupCoor(_coor);
            return false;
        });
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
        
        // transform.position = Input.mousePosition + new Vector3(-168 / 2 , 100 , 0);
        transform.position = Input.mousePosition;
        transform.SetAsLastSibling();

        BrickController.Instance.onBrickDrag(this , eventData , transform.localPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        BrickController.Instance.onBrickDragEnd(this , eventData);
    }

    public void EachBrick(Func<Brick , bool> act){
        foreach(Brick brick in _bricks){
            if(act(brick)){
                return;
            }
        }
    }

    public void SetDown(Vector2Int coor){
        setCoor(coor);
        _is_frozen = true;
    }
}
