using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using System;

public class VirtualBrick
{
    private BrickInfo _brick_info{get;set;}
    private Vector2Int _coor{get;set;}

    public VirtualBrick(BrickInfo brick_info , Vector2Int coor)
    {
        _brick_info = brick_info;
        _coor = coor;
    }
}
