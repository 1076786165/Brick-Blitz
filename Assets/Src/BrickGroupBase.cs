using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using System;

public interface BrickGroupBase
{
    public BrickInfo _brick_info{get;set;}
    public Vector2Int _coor{get;set;}

    public List<Brick> _bricks{get;set;}

    public void EachBrick(Func<Brick , bool> act){
        foreach(Brick brick in _bricks){
            if(act(brick)){
                return;
            }
        }
    }
}
