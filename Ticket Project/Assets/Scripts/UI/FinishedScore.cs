using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishedScore : MonoBehaviour {

    private float totalscore;
    private Text score_txt;

    StageController stagecont;

	void Start () {
        score_txt = GetComponent<Text>();
        stagecont = StageController.instance;

        totalscore = stagecont.Score;
        score_txt.text = totalscore.ToString();
    }
	void Update () {

	}
}
