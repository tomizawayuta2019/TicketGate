using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishedScore : MonoBehaviour {

    private float totalscore;
    private float oldscore;
    [SerializeField]
    private Sprite[] _number = new Sprite[10];
    // _number[0] → ０の画像
    // _number[1] → １の画像
    [SerializeField]
    private StageController _controller;
    [SerializeField]
    private Image[] _img = new Image[4];
    // 1桁目は０で固定
    // _img[0] → 2桁目の値

	void Start () {
        _controller = StageController.instance;

        // score初期化
        totalscore = 0;
        for (int i = 0; i < 5; i++)
        {
            _img[i].sprite = _number[0];
        }
    }
	void Update () {
        if (totalscore == oldscore) { return; }

        totalscore = _controller.Score;
        totalscore = totalscore / 10;

        if (totalscore < 10) // 2桁目
        {
            _img[0].sprite = _number[(int)totalscore];
        }
        else if ((totalscore > 10) && (totalscore < 100 )) // 3桁目
        {
            _img[1].sprite = _number[(int)(totalscore / 10)];
            _img[0].sprite = _number[(int)(totalscore % 10)];
        }
        else if ((totalscore >= 100) && (totalscore < 1000)) // 4桁目
        {
            _img[2].sprite = _number[(int)(totalscore / 100)];
            _img[1].sprite = _number[(int)(totalscore / 10)];
            _img[0].sprite = _number[(int)(totalscore % 10)];
        }
        else if (totalscore >= 1000) // 5桁目
        {
            _img[3].sprite = _number[(int)(totalscore / 1000)];
            _img[2].sprite = _number[(int)(totalscore / 100)];
            _img[1].sprite = _number[(int)(totalscore / 10)];
            _img[0].sprite = _number[(int)(totalscore % 10)];
        }

        oldscore = totalscore * 10;
    }
}
