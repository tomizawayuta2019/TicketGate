﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour {
    [SerializeField] StageState[] stage_num; //ステージ情報
    [SerializeField] Text[] stage_text; //ステージの名前
    [SerializeField] Text[] score_text; //スコア

    public static StageState[] stages;

    private void Awake()
    {
        stages = stage_num;
    }

    [SerializeField] Button[] stage_button;  //ボタン
    void Start () {


        //ステージ名とスコア表示
        for (int i = 0; i < stage_num.Length; i++)
        {
            stage_text[i].text = stage_num[i].StageName;
            score_text[i].text = ((int)stage_num[i].MaxScore).ToString();
        }

        //ステージのクリア情報
        for (int i = 0; i < stage_num.Length-1; i++) {
            if (stage_num[i].IsClear == false){
                stage_button[i].interactable = false;
            }else{
                stage_button[i].interactable = true;
            }
        }
    }

    public void StageChange(int stagenum)   //ステージ移行
    {
        StageController.SetStage(stage_num[stagenum]);
        Fade.instance.FadeStart("Main");
    }

}
