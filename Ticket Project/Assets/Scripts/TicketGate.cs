using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketGate : MonoBehaviour
{

    private enum GateContoroller
    {
        //人が来たらチケット情報取得
        Start,
        //↑ボタン押されるまで待機
        Pasumo_OK,
        //←ボタン押されるまで待機
        Ticket,
        //↓ボタン押されるまで待機
        Ticket_OK1,
        //→ボタン押されるまで待機
        Ticket_OK2,
        //スペース以外の受付NG
        NO
    }

    private float timer;
    private bool timeOn = false;
    private GateContoroller _gate;
    /* 人のチケット情報enum */

    // Use this for initialization
    void Start()
    {
        timeOn = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeOn) { timer += Time.deltaTime; }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_gate == GateContoroller.Ticket)
            {
                //切符が間違っているかどうかを取得(間違ってたらNO,あってたらTicket_OK1)
                _gate = GateContoroller.Ticket_OK1;
            }else{
                //ポコポコ怒りアニメーション
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_gate == GateContoroller.Ticket_OK1) {
                _gate = GateContoroller.Ticket_OK2;
            }else{
                //ポコポコ怒りアニメーション
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_gate == GateContoroller.Ticket_OK2)
            {
                Completed();
            }else{
                //ポコポコ怒りアニメーション
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_gate == GateContoroller.Pasumo_OK)
            {
                Completed();
            }
            else{
                //ポコポコ怒りアニメーション
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_gate == GateContoroller.NO)
            {
                //正しく止めたことを人クラスに渡す（関数呼び出し）
                timeOn = false;
                timer = 0;
            }else{
                //間違えて止めたことを人クラスに渡す（関数呼び出し）
                timeOn = false;
                timer = 0;
            }
        }
    }

    public void SetTicket(/* 人クラスの持ってるチケット情報enum */){
        //人の持っているチケット情報をこのスクリプトのenum変数に取得する
        //チケットの種類を見て、_gateの値を変える(switch)
        //タイマーを開始する
        timeOn = true;
    }

    private void Completed()
    {
        timeOn = false;
        //人クラスにタイムと終了したかを渡す関数呼び出し
        timer = 0;
        _gate = GateContoroller.Start;
    }
}
