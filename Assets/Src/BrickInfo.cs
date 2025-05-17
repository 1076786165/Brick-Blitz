using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickInfo
{
    private string _name{get;}
    private int[,] _shape{get;}

    public BrickInfo(string name , int[,] shape)
    {
        _name = name;
        _shape = shape;
    }

    public void EachBrickInfo(Action<int, int> act){
        for(int i = 0 ; i < 4 ; i++){
            for(int j = 0 ; j < 4 ; j++){
                if(_shape[i , j] > 0){
                    act(j , 3 - i);
                }
            }
        }
    }
}
