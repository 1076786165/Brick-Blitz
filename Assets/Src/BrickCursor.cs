using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using System;

public class BrickCursor:MonoBehaviour
{
    public void Init(BrickInfo _brick_info , Vector2Int coor){
            _brick_info.eachBrickInfo((int i , int j) => {
            GameObject o_brick = Instantiate(BrickDef.Instance._brick_prefab , transform);
            Brick brick = o_brick.GetComponent<Brick>();
            brick.setCoor(coor , new Vector2Int(i , j));
            brick.setIsCursor();
        });
    }

    public void updateCoor(Vector2Int coor){
        if(coor.x < 0){
            gameObject.SetActive(false);
        }else{
            gameObject.SetActive(true);
            gameObject.transform.localPosition = new Vector2(coor.x * Config.BRICK_SIZE.x , coor.y * Config.BRICK_SIZE.y);

        }
    }
}
