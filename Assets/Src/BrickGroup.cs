using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BrickGroup : MonoBehaviour , IDragHandler , IEndDragHandler
{
    private BrickInfo _brick_info{get;set;}
    private List<Brick> _bricks;

    public void Init(BrickInfo brick_info){
        _bricks = new List<Brick>();

        _brick_info = brick_info;

        _brick_info.eachBrickInfo((int i , int j) => {
            GameObject o_brick = Instantiate(BrickDef.Instance._brick_prefab , transform);
            Brick brick = o_brick.GetComponent<Brick>();
            brick.setLocalCoor(new Vector2Int(i , j));

            _bricks.Add(brick);
        });
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    void Start(){
        
    }

}
