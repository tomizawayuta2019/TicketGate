using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour {
    [SerializeField] StageState[] stage_num; //ステージ情報
    [SerializeField] Text[] stage_text; //ステージの名前
    [SerializeField] Text[] score_text; //スコア
    [SerializeField] Button[] stage_button;  //ボタン
    // Use this for initialization
    void Start () {
        //ステージ名とスコア表示
        for (int i = 0; i < stage_num.Length; i++)
        {
            stage_text[i].text = stage_num[i].StageName;
            score_text[i].text = stage_num[i].MaxScore.ToString();
        }
        //ステージのクリア情報
        for (int i = 0; i < stage_num.Length; i++) {
            if (stage_num[i].IsClear == false){
                stage_button[i].interactable = false;
            }else{
                stage_button[i].interactable = true;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void StageChange()   //ステージ移行
    {
        SceneManager.LoadScene("Scene/Main");
    }
}
