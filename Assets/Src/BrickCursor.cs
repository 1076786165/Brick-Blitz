using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using System;

public class BrickCursor : BrickGroup
{
    public DetectResult _detect_result{get;set;}

    public void InitWithBrickGroup(BrickGroup brick_group){
        _brick_info = brick_group._brick_info;
        _group_id = brick_group._group_id;

        _bricks = new List<Brick>();
        _brick_info.EachBrickInfo((int i , int j) => {
            GameObject o_brick = Instantiate(BrickDef.Instance._brick_prefab , transform);
            Brick brick = o_brick.GetComponent<Brick>();
            brick.setCoor(new Vector2Int(0 , 0) , new Vector2Int(i , j));
            brick.SetIsCursor();

            _bricks.Add(brick);
        });
    }

    public void UpdateCoorWithDetect(DetectResult detect_result){
        _detect_result = detect_result;
        if(!_detect_result.is_illegal){
            gameObject.SetActive(false);

        }else{
            gameObject.SetActive(true);
            gameObject.transform.localPosition = CalcTools.convertCoorToPosition(detect_result.coor);

        }
    }

    public bool IsCursorIllegal(BrickGroup brick_group){
        return _detect_result.is_illegal && brick_group._group_id == _group_id;
    }
}
