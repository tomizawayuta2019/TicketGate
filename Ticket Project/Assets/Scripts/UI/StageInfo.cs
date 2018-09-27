using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageInfo : MonoBehaviour {
    [SerializeField]
    private int min_hour;   //始発
    [SerializeField]
    private int max_hour;   //終電

    private Text info_txt;
    private int now_hour;   //現時間
    private float num;
    private float time;
    
	void Start () {
        info_txt = GetComponentInChildren<Text>();
        num = 180.0f / (max_hour - min_hour);
        ///時間が１進むのに必要な秒数を求める
        now_hour = min_hour;
	}
	
	void Update () {
        time += Time.deltaTime;

        //numの値をtimeが超えたら現時間を１進める
        if (time >= num)
        {
            now_hour += 1;
            time -= num;
            //余剰時間以外の部分を減らす
        }

        if (now_hour > max_hour) { return; }
        if (now_hour < 12) { AM_time(); }
        else if (now_hour >= 12) { PM_time(); }

    }

    void AM_time()
    {
        info_txt.text = "AM" + now_hour.ToString("00") + "時";
    }
    void PM_time()
    {
        info_txt.text = "PM" + now_hour.ToString("00") + "時";
    }
}
