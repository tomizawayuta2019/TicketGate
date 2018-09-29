using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageInfo : MonoBehaviour {
    
    private StageController _controller;
    [SerializeField]
    private Sprite[] _number = new Sprite[10];  // 0,1,2 格納

    private float _maxtime; // 制限時間
    private int _first;     // 一桁目
    private int _second;    // 二桁目
    private int _third;     // 三桁目


    [SerializeField]
    private Image[] _spr = new Image[3];    // 三桁の数字のimage格納
    private float time;
    
	void Start () {

        _controller = StageController.instance;
        //min_hour = _controller.nowStage.StartHour;
        //max_hour = _controller.nowStage.EndHour;

        //num = 180.0f / (max_hour - min_hour);
        Debug.Log(_controller.MaxTime);
        _maxtime = _controller.MaxTime + 1;
        ReduceTimer(_maxtime);
	}
	
	void Update () {
        if(_maxtime < 0) { return; }
        time = TimeManager.DeltaTime;

        _maxtime -= time;
        ReduceTimer(_maxtime);
    }

    void ReduceTimer(float time)
    {
        if (_maxtime < 10) // 2桁目
        {
            _spr[1].sprite = _number[0];
            _spr[0].sprite = _number[(int)_maxtime];
        }
        else if ((_maxtime > 10) && (_maxtime < 100))
        {
            _spr[2].sprite = _number[0];
            _spr[1].sprite = _number[(int)(_maxtime / 10)];
            _spr[0].sprite = _number[(int)(_maxtime % 10)];
        }
        else if ((_maxtime >= 100) && (_maxtime < 1000))
        {
            _spr[2].sprite = _number[(int)(_maxtime / 100)];
            _spr[1].sprite = _number[(int)(_maxtime % 100) / 10];
            _spr[0].sprite = _number[(int)(_maxtime % 10)];
        }
    }
}
