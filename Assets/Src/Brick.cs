using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Brick : MonoBehaviour
{
    private BrickInfo _brick_info{get;set;}
    public Vector2Int _local_coor{get;set;}
    public Vector2Int _global_coor{get;set;}

    public void Init(BrickInfo brick_info){
        _brick_info = brick_info;
    }

    public void setCoor(Vector2Int group_coor , Vector2Int local_coor){
        _global_coor = group_coor + local_coor;
        _local_coor = local_coor;

        updateCoor();
    }
    public void setGroupCoor(Vector2Int group_coor){
        _global_coor = group_coor + _local_coor;
    }

    public void updateCoor(){
        gameObject.transform.localPosition = CalcTools.convertCoorToPosition(_local_coor);
    }

    public void SetIsCursor(){
        Image targetImage = GetComponent<Image>();
        Color newColor = targetImage.color;
        newColor.a = 0.5f;
        targetImage.color = newColor;
    }

    void Start(){
        
    }


}
