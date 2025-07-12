using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickDef : MonoBehaviourSingleton<BrickDef>
{
    public GameObject _brick_prefab;
    public GameObject _brick_group_prefab;
    public GameObject _brick_cursor_prefab;

    Dictionary<string, BrickInfo> _brick_defs = new Dictionary<string, BrickInfo>();
    List<BrickInfo> _brick_defs_list = new List<BrickInfo>();

    String[] _brick_textures_names;

    protected override void Start()
    {
        // L
        BrickInfo L1 = new BrickInfo(){
            _name = "L1",
            _shape = new int[3 , 3]{
                {1 , 1 , 1},
                {0 , 0 , 1},
                {0 , 0 , 1},
            },
        };
        _brick_defs.Add("L1", L1);

        BrickInfo L2 = new BrickInfo(){
            _name = "L2",
            _shape = new int[2 , 2]{
                {1 , 1},
                {0 , 1},
            },
        };
        _brick_defs.Add("L2", L2);

        BrickInfo L3 = new BrickInfo(){
            _name = "L3",
            _shape = new int[2 , 3]{
                {1 , 1 , 1},
                {0 , 0 , 1},
            },
        };
        _brick_defs.Add("L3", L3);

        BrickInfo L4 = new BrickInfo(){
            _name = "L4",
            _shape = new int[2 , 3]{
                {1 , 1 , 1},
                {1 , 0 , 0},
            },
        };
        _brick_defs.Add("L4", L4);

        BrickInfo L5 = new BrickInfo(){
            _name = "L5",
            _shape = new int[3 , 2]{
                {0 , 1},
                {0 , 1},
                {1 , 1},
            },
        };
        _brick_defs.Add("L5", L5);

        BrickInfo L6 = new BrickInfo(){
            _name = "L6",
            _shape = new int[2 , 3]{
                {1 , 0 , 0},
                {1 , 1 , 1},
            },
        };
        _brick_defs.Add("L6", L6);

        BrickInfo L7 = new BrickInfo(){
            _name = "L7",
            _shape = new int[3 , 3]{
                {1 , 1 , 1},
                {1 , 0 , 0},
                {1 , 0 , 0},
            },
        };
        _brick_defs.Add("L7", L7);

        //O
        BrickInfo O1 = new BrickInfo(){
            _name = "O1",
            _shape = new int[3 , 2]{
                {1 , 1},
                {1 , 1},
                {1 , 1},
            },
        };
        _brick_defs.Add("O1", O1);

        BrickInfo O2 = new BrickInfo(){
            _name = "O2",
            _shape = new int[2 , 3]{
                {1 , 1 , 1},
                {1 , 1 , 1},
            },
        };
        _brick_defs.Add("O2", O2);

        BrickInfo O3 = new BrickInfo(){
            _name = "O3",
            _shape = new int[3 , 3]{
                {1 , 1 , 1},
                {1 , 1 , 1},
                {1 , 1 , 1},
            },
        };
        _brick_defs.Add("O3", O3);

        BrickInfo O4 = new BrickInfo(){
            _name = "O4",
            _shape = new int[2 , 2]{
                {1 , 1},
                {1 , 1},
            },
        };
        _brick_defs.Add("O4", O4);

        //I
        BrickInfo I1 = new BrickInfo(){
            _name = "I1",
            _shape = new int[1 , 3]{
                {1 , 1 , 1},
            },
        };
        _brick_defs.Add("I1", I1);

        BrickInfo I2 = new BrickInfo(){
            _name = "I2",
            _shape = new int[1 , 4]{
                {1 , 1 , 1 , 1},
            },
        };
        _brick_defs.Add("I2", I2);

        BrickInfo I3 = new BrickInfo(){
            _name = "I3",
            _shape = new int[4 , 1]{
                {1},
                {1},
                {1},
                {1},
            },
        };
        _brick_defs.Add("I3", I3);

        BrickInfo I4 = new BrickInfo(){
            _name = "I4",
            _shape = new int[1 , 3]{
                {1 , 1 , 1},
            },
        };
        _brick_defs.Add("I4", I4);

        BrickInfo I5 = new BrickInfo(){
            _name = "I5",
            _shape = new int[3 , 1]{
                {1},
                {1},
                {1},
            },
        };
        _brick_defs.Add("I5", I5);

        BrickInfo I6 = new BrickInfo(){
            _name = "I6",
            _shape = new int[1 , 2]{
                {1 , 1},
            },
        };
        _brick_defs.Add("I6", I6);


        BrickInfo I7 = new BrickInfo(){
            _name = "I7",
            _shape = new int[5 , 1]{
                {1},
                {1},
                {1},
                {1},
                {1},
            },
        };
        _brick_defs.Add("I7", I7);

        BrickInfo I8 = new BrickInfo(){
            _name = "I8",
            _shape = new int[1 , 5]{
                {1 , 1 , 1 , 1 , 1},
            },
        };
        _brick_defs.Add("I8", I8);

        //Z
        BrickInfo Z1 = new BrickInfo(){
            _name = "Z1",
            _shape = new int[2 , 3]{
                {0 , 1 , 1},
                {1 , 1 , 0},
            },
        };
        _brick_defs.Add("Z1", Z1);

        BrickInfo Z2 = new BrickInfo(){
            _name = "Z2",
            _shape = new int[3 , 2]{
                {0 , 1},
                {1 , 1},
                {1 , 0},
            },
        };
        _brick_defs.Add("Z2", Z2);

        BrickInfo Z3 = new BrickInfo(){
            _name = "Z3",
            _shape = new int[2 , 3]{
                {1 , 1 , 0},
                {0 , 1 , 1},
            },
        };
        _brick_defs.Add("Z3", Z3);

        //T
        BrickInfo T1 = new BrickInfo(){
            _name = "T1",
            _shape = new int[3 , 2]{
                {1 , 0},
                {1 , 1},
                {1 , 0},
            },
        };
        _brick_defs.Add("T1", T1);

        BrickInfo T2 = new BrickInfo(){
            _name = "T2",
            _shape = new int[2 , 3]{
                {1 , 1 , 1},
                {0 , 1 , 0},
            },
        };
        _brick_defs.Add("T2", T2);

        //X
        BrickInfo X1 = new BrickInfo(){
            _name = "X1",
            _shape = new int[3 , 3]{
                {1 , 0 , 0},
                {0 , 1 , 0},
                {0 , 0 , 1},
            },
        };
        _brick_defs.Add("X1", X1);

        BrickInfo X2 = new BrickInfo(){
            _name = "X2",
            _shape = new int[3 , 3]{
                {0 , 0 , 1},
                {0 , 1 , 0},
                {1 , 0 , 0},
            },
        };
        _brick_defs.Add("X2", X2);

        BrickInfo X3 = new BrickInfo(){
            _name = "X3",
            _shape = new int[2 , 2]{
                {0 , 1},
                {1 , 0},
            },
        };
        _brick_defs.Add("X3", X3);

        BrickInfo X4 = new BrickInfo(){
            _name = "X4",
            _shape = new int[2 , 2]{
                {1 , 0},
                {0 , 1},
            },
        };
        _brick_defs.Add("X4", X4);



        foreach(BrickInfo brick_def in _brick_defs.Values){
            _brick_defs_list.Add(brick_def);
        }

        _brick_textures_names = new String[]{
            "img_brick_1",
            "img_brick_2",
            "img_brick_3",
            "img_brick_4",
            "img_brick_5",
            "img_brick_6",
            "img_brick_7",
            "img_brick_8",
            "img_brick_9"
        };
    }

    public string getRandomBrickTextureName(){
        int index = UnityEngine.Random.Range(0 , _brick_textures_names.Length - 1);
        return _brick_textures_names[index];
    }

    public BrickInfo GetRandomBrickDef(){
        int index = UnityEngine.Random.Range(0 , _brick_defs_list.Count - 1);
        return _brick_defs_list[index];
    }

    public BrickInfo GetBrickDef(string shape_name){
        return _brick_defs[shape_name];
    }

    public BrickInfo CreateBrickInfoWithShape(int[,] shape , string name = "custom"){
        BrickInfo L1 = new BrickInfo(name , shape);
        return L1;
    }
}
