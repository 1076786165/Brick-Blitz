using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CalcTools : MonoBehaviourSingleton<CalcTools>
{
    public static Vector2Int? convertPostionToCoor2(Vector2 pos){
        int x = (int)pos.x / Config.BRICK_SIZE.x;
        int y = (int)pos.y / Config.BRICK_SIZE.y;

        if(x < 0 || x >= Config.BOARD_SIZE.x || y < 0 || y >= Config.BOARD_SIZE.y){
            return null;
        }
        return new Vector2Int(x, y);
    }

    public static Vector2Int convertPostionToCoor(Vector2 pos){
        int x = (int)pos.x / Config.BRICK_SIZE.x;
        int y = (int)pos.y / Config.BRICK_SIZE.y;

        if(x < 0 || x >= Config.BOARD_SIZE.x || y < 0 || y >= Config.BOARD_SIZE.y){
            return new Vector2Int(-1, -1);
        }
        return new Vector2Int(x, y);
    }
}
