using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private BrickInfo _brick_info{get;set;}

    public Vector2 _local_coor{get;set;}
    public Vector2 _global_coor{get;set;}

    public void Init(BrickInfo brick_info){
        _brick_info = brick_info;
    }

    public void setLocalCoor(Vector2Int local_coor){
        _local_coor = local_coor;
        gameObject.transform.localPosition = new Vector2(_local_coor.x * Config.BRICK_SIZE.x , _local_coor.y * Config.BRICK_SIZE.y);
    }

    void Start(){
        
    }


}
