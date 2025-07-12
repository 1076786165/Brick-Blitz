using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System;
using TMPro;

public class UIController : MonoBehaviourSingleton<UIController>
{
    // object绑定
    public TMP_Text lb_socre;

    protected override void Start()
    {
        EventManager.AddListener(Config.EVENT_UPDATE_UI , () => {
            if(lb_socre){
                lb_socre.text = RoundController.Instance._cur_socre.ToString();
            }
        });
    }
}
