using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickDef : MonoBehaviourSingleton<BrickDef>
{
    public GameObject _brick_prefab;
    public GameObject _brick_group_prefab;
    public GameObject _brick_cursor_prefab;

    Dictionary<string, BrickInfo> _brick_def = new Dictionary<string, BrickInfo>();


    protected override void Start()
    {
        BrickInfo L1 = new BrickInfo("L1", new int[4 , 4]{
            {1 , 0 , 0 , 0},
            {1 , 0 , 0 , 0},
            {1 , 0 , 0 , 0},
            {1 , 1 , 1 , 1},
        });
        _brick_def.Add("L1", L1);

    }

    public BrickInfo GetBrickDef(string shape_name){
        return _brick_def[shape_name];
    }
}
