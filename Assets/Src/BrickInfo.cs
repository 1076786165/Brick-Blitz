using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickInfo
{
    public string _name;
    public int[,] _shape;
    public string _img_name = "random";

    public BrickInfo(){
    }

    public BrickInfo(string name, int[,] shape){
        _name = name;
        _shape = shape;
    }

    public void EachBrickShape(Action<int, int> act){
        for(int i = 0 ; i < _shape.GetLength(0) ; i++){
            for(int j = 0 ; j < _shape.GetLength(1) ; j++){
                if(_shape[i , j] > 0){
                    act(j , 3 - i);
                }
            }
        }
    }

    public Vector2 GetBrickActualSize(){
        int width = 0;
        int height = 0;
        EachBrickShape((x, y) => {
            if(x > width - 1) width = x + 1;
            if(y > height - 1) height = y + 1;
        });
        return new Vector2(width, height);
    }
}
