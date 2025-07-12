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
    public int _slot_id;

    private Vector2Int _begin_coor;
    private bool _is_frozen{get;set;}
    public String _img_name;

    public void InitInSlot(BrickInfo brick_info , int group_id , int slot_id){
        _brick_info = brick_info;
        _group_id = group_id;
        _slot_id = slot_id;
        _coor = new Vector2Int(0 , 0);

        if(_brick_info._img_name == "random")
        {   
            _img_name = BrickDef.Instance.getRandomBrickTextureName();
            
        }else{
            _img_name = _brick_info._img_name;
        }

        _bricks = new List<Brick>();
        _brick_info.EachBrickShape((int i , int j) => {
            GameObject o_brick = Instantiate(BrickDef.Instance._brick_prefab , transform);
            Brick brick = o_brick.GetComponent<Brick>();
            brick.Init(_brick_info , _img_name);
            brick.SetCoor(_coor , new Vector2Int(i , j));

            _bricks.Add(brick);
        });
        
        // GetComponent<RectTransform>().sizeDelta = _brick_info.GetBrickActualSize();

        BackToSlot();
    }

    public void BackToSlot(){
        gameObject.transform.localPosition = CalcTools.GetCenterPosition(BrickController.Instance.GetSlotPosition(_slot_id) , _brick_info.GetBrickActualNum());
        gameObject.transform.localScale = new Vector3(0.7f , 0.7f , 0.7f);
    }

    public void SetCoor(Vector2Int coor){
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
        SetCoor(_begin_coor);
    }

    public void checkEliminateX(List<int> eliminate_list , Action<Vector2Int> on_eliminate){
        List<Brick> bricks_to_eliminate = new List<Brick>();
        EachBrick((Brick brick) => {
            if(eliminate_list.Contains(brick._global_coor.y)){
                brick.Eliminate();
                bricks_to_eliminate.Add(brick);
                on_eliminate(brick._global_coor);
            }
            return false;
        });

        foreach(Brick brick in bricks_to_eliminate){
            _bricks.Remove(brick);
        }
    }

    public void checkEliminateY(List<int> eliminate_list , Action<Vector2Int> on_eliminate){
        List<Brick> bricks_to_eliminate = new List<Brick>();
        EachBrick((Brick brick) => {
            if(eliminate_list.Contains(brick._global_coor.x)){
                brick.Eliminate();
                bricks_to_eliminate.Add(brick);
                on_eliminate(brick._global_coor);
            }
            return false;
        });

        foreach(Brick brick in bricks_to_eliminate){
            _bricks.Remove(brick);
        }
    }

    // DARG
    public void OnBeginDrag(PointerEventData eventData){
        if(_is_frozen){
            return;
        }
        gameObject.transform.localScale = new Vector3(0.7f , 0.7f , 0.7f);
        setBeginCoor(_coor);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(_is_frozen){
            return;
        }

        Vector2 actual_size = _brick_info.GetBrickActualSize();
        Vector2 new_pos = new Vector2(Input.mousePosition.x - actual_size.x / 2 , Input.mousePosition.y + 50);
        
        transform.position = new_pos;
        transform.SetAsLastSibling();

        BrickController.Instance.onBrickDrag(this , eventData , transform.localPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(_is_frozen){
            return;
        }
        gameObject.transform.localScale = Vector3.one;
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
        SetCoor(coor);
        _is_frozen = true;
    }

    public int GetBrickNum(){
        int num = 0;
        EachBrick((brick) => {
            num++;
            return false;
        });
        return num;
    }
}
