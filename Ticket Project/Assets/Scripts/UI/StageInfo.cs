using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageInfo : MonoBehaviour {

    //[SerializeField]
    //private StageController _controller;
    [SerializeField]
    private Sprite[] firstnumber = new Sprite[10];
    [SerializeField]
    private Sprite[] secondnumber = new Sprite[3];


    private int min_hour;   //始発
    private int max_hour;   //終電
    private int now_hour;   //現時間
    private int now_hour_first;   //現時間  →  一桁目
    private int now_hour_second;   //現時間  →  二桁目

    [SerializeField]
    private Image first_spr;
    [SerializeField]
    private Image second_spr;
    private float num;
    private float time; 
    
	void Start () {
        second_spr.sprite = secondnumber[0];

        //_controller = StageController.instance;
        //_stage = _controller.nowStage;
        //min_hour = _controller.nowStage.StartHour;
        //max_hour = _controller.nowStage.EndHour;
        min_hour = 5;
        max_hour = 23;

        num = 180.0f / (max_hour - min_hour);
        ///時間が１進むのに必要な秒数を求める
        now_hour_first = now_hour = min_hour;
        first_spr.sprite = firstnumber[now_hour];

	}
	
	void Update () {
        if (now_hour > max_hour) { return; }
        
        time += Time.deltaTime;

        //numの値をtimeが超えたら現時間を１進める
        if (time >= num)
        {
            now_hour += 1;
            now_hour_first += 1;
            if (now_hour_first > 9)
            {
                now_hour_first = 0;
                now_hour_second += 1;
                second_spr.sprite = secondnumber[now_hour_second];
            }
            time -= num;
            //余剰時間以外の部分を減らす
        }
        first_spr.sprite = firstnumber[now_hour_first];
    }
}
