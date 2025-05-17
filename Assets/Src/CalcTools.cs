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
}
