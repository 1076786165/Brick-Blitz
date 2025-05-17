using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CalcTools : MonoBehaviourSingleton<CalcTools>
{
    public static Vector2Int convertPostionToCoor(Vector2 pos){
        int x = (int)pos.x / Config.BRICK_SIZE.x;
        int y = (int)pos.y / Config.BRICK_SIZE.y;

        return new Vector2Int(x, y);
    }

    public static Vector2 convertCoorToPosition(Vector2Int coor){
        float x = coor.x * Config.BRICK_SIZE.x;
        float y = coor.y * Config.BRICK_SIZE.y;

        return new Vector2(x, y);
    }

    public static Vector2 GetCenterPosition(Vector2 leftdown_pos , Vector2 group_size){
        leftdown_pos.x -= Config.BRICK_SIZE.x * group_size.x / 2;
        leftdown_pos.y -= Config.BRICK_SIZE.y * group_size.y / 2;

        return leftdown_pos;
    }
}
