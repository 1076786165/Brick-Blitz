using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2Int _board_size = Config.BOARD_SIZE;
    private int[,] _board_map;

    void Start()
    {
        _board_map = new int[_board_size.x , _board_size.y];
        eachBoardCoor((x , y) => {
            Debug.Log("each board " + x + " " + y);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void eachBoardCoor(Action<int , int> act){
        for(int i = 0 ; i < _board_size.x ; i++){
            for(int j = 0 ; j < _board_size.y ; j++){
                act(j , i);
            }
        }
    }
}
