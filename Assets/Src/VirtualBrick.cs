using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using System;

public struct DetectResult{
    public bool is_illegal{get;set;}
    public Vector2Int coor{get;set;}
}

public class VirtualBrick
{
    private BrickInfo _brick_info{get;set;}
    private Vector2Int _coor{get;set;}
    private List<Vector2Int> _virtual_bricks{get;set;}
    public int _group_id{get;set;}  

    public VirtualBrick(BrickGroup brick_group , Vector2Int coor)
    {
        _brick_info = brick_group._brick_info;
        _coor = coor;
        _group_id = brick_group._group_id;

        _virtual_bricks = new List<Vector2Int>();
        _brick_info.EachBrickShape((int x, int y) => {
            Vector2Int virtual_brick_coor = new Vector2Int(x + _coor.x, y + _coor.y);
            _virtual_bricks.Add(virtual_brick_coor);
        });
    }


    public DetectResult DetectingCoor(int[,] _board_map){
        DetectResult result = new DetectResult();
        result.is_illegal = true;
        result.coor = _coor;

        do{
            // Debug.Log("------------------------------" + "DetectingCoor in " + _coor.ToString());
            if(_coor.x < 0 || _coor.x >= Config.BOARD_SIZE.x || _coor.y < 0 || _coor.y >= Config.BOARD_SIZE.y){
                result.is_illegal = false;
                // Debug.Log("illegal coor in " + _coor.ToString());
                break;
            }

            eachVirtualBrick((Vector2Int virtual_brick) => {
                // Debug.Log("cursor brick in " + virtual_brick.ToString());
                if (virtual_brick.x < 0 || virtual_brick.x >= Config.BOARD_SIZE.x || virtual_brick.y < 0 || virtual_brick.y >= Config.BOARD_SIZE.y){
                    result.is_illegal = false;
                    // Debug.Log("illegal brick in " + virtual_brick.ToString());
                    return true;
                }
                if (_board_map[virtual_brick.x, virtual_brick.y] != 0){
                    result.is_illegal = false;
                    // Debug.Log("illegal brick in " + virtual_brick.ToString());
                    return true;
                }
                return false;
            });
        }while(false);

        return result;
    }

    public void eachVirtualBrick(Func<Vector2Int , bool> act){
        foreach(Vector2Int virtual_brick in _virtual_bricks){
            if(act(virtual_brick)){
                return;
            }
        }
    }
}
