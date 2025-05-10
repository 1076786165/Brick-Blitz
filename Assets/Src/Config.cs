using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviourSingleton<Config>
{
    public static Vector2Int BOARD_SIZE{get;} = new Vector2Int(8 , 8);

    public static Vector2Int BRICK_SIZE{get;} = new Vector2Int(42 , 42);
}
