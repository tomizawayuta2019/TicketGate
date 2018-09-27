using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishedScore : MonoBehaviour {

    private int totalscore;
    private Text score_txt;

	void Start () {
        score_txt = GetComponent<Text>();
	}
	void Update () {
        score_txt.text = totalscore.ToString();
	}
    public int InScore(int _score)
    {
        totalscore += _score;
        return totalscore;
    }
}
