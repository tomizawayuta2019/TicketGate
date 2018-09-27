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
    private int minute;
    private int now_hour;
    private float starttime;
    private int num;
    
	void Start () {
        info_txt = GetComponentInChildren<Text>();
        num = (max_hour - min_hour) * 6;
        starttime = Time.time;		
	}
	
	void Update () {
		if (now_hour < 12) { AM_time(); }
        else if (now_hour >= 12) { PM_time(); }
    }
    void AM_time()
    {
        info_txt.text = "AM" + now_hour.ToString("00") + ":" + minute.ToString("00");
    }
    void PM_time()
    {
        info_txt.text = "PM" + now_hour.ToString("00") + ":" + minute.ToString("00");
    }
}
