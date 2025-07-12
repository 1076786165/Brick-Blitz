using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;
using TMPro;

public class RoundController : MonoBehaviourSingleton<RoundController>
{
    public int _cur_socre = 0;

    public void OnEliminated(int eliminate_lines){
        _cur_socre = _cur_socre + eliminate_lines * 10;
        OnSocreChanged();
    }
    public void OnBrickSet(int brick_num){
        _cur_socre = _cur_socre + brick_num;
        OnSocreChanged();
    }

    public void OnSocreChanged(){
        EventManager.SendEvent(Config.EVENT_UPDATE_UI);
    }
}
