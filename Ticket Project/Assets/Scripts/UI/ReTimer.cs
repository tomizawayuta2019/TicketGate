using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReTimer : MonoBehaviour {

    [SerializeField]
    private float retime; //制限時間

    private Text time_txt;
    private float seconds;
    private float oldSeconds;
    void Start () {
        time_txt = GetComponentInChildren<Text>();
        oldSeconds = 0f;
    }
	
	void Update () {
        if (retime <= 0f) { return; }

        retime -= Time.deltaTime;
        seconds = (int)retime;

        if ((int)seconds != (int)oldSeconds)
        {
            time_txt.text = retime.ToString("000") + "秒";
        }
        oldSeconds = seconds;

	}
}
