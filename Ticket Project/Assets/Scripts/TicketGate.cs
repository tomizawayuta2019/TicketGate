using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketGate : MonoBehaviour
{
    //testする！！！！

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
    TicketType _ticket;

    // Use this for initialization
    void Start()
    {
        timeOn = false;
        timer = 0;
        SetTicket(TicketType.paper);
    }

    // Update is called once per frame
    void Update()
    {
        if (_gate == GateContoroller.Start) return;
        if (timeOn) { timer += Time.deltaTime; }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TicketSort();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_gate == GateContoroller.Ticket_OK1) {
                _gate = GateContoroller.Ticket_OK2;
                //Debug.Log("↓");
            }
            else{
                //ポコポコ怒りアニメーション
                //Debug.Log("??");
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_gate == GateContoroller.Ticket_OK2)
            {
                Completed();
                //Debug.Log("→");
            }
            else{
                //ポコポコ怒りアニメーション
                //Debug.Log("??");
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_gate == GateContoroller.Pasumo_OK)
            {
                Completed();
                //Debug.Log("↑");
            }
            else{
                //ポコポコ怒りアニメーション
                //Debug.Log("??");
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_gate == GateContoroller.NO)
            {
                //正しく止めたことを人クラスに渡す（関数呼び出し）
                timeOn = false;
                timer = 0;
                _gate = GateContoroller.Start;
                //Debug.Log("SPACE");
            }
            else{
                //間違えて止めたことを人クラスに渡す（関数呼び出し）
                timeOn = false;
                timer = 0;
                _gate = GateContoroller.Start;
                //Debug.Log("SPACE_BAD");
            }
        }
    }

    public void SetTicket(TicketType type){
        //人の持っているチケット情報をこのスクリプトのenum変数に取得する
        _ticket = type;
        //チケットの種類を見て、_gateの値を変える(switch)
        switch (_ticket)
        {
            case TicketType.paper:
            case TicketType.paper_miss:
                _gate = GateContoroller.Ticket;
                //Debug.Log("TICKET");
                break;
            case TicketType.suica:
                _gate = GateContoroller.Pasumo_OK;
                //Debug.Log("PASUMO");
                break;
            case TicketType.suica_miss:
                _gate = GateContoroller.NO;
                //Debug.Log("PASUMO_NO");
                break;
        }
        //タイマーを開始する
        timeOn = true;
    }

    private void TicketSort()
    {
        if (_gate == GateContoroller.Ticket)
        {
            //切符が間違っているかどうかを取得(間違ってたらNO,あってたらTicket_OK1)
            if (_ticket == TicketType.paper)
            {
                _gate = GateContoroller.Ticket_OK1;
                //Debug.Log("GOOD");
            }
            if (_ticket == TicketType.paper_miss)
            {
                _gate = GateContoroller.NO;
                //Debug.Log("NO_GOOD");
            }
            //Debug.Log("←");
        }
        else
        {
            //ポコポコ怒りアニメーション
            //Debug.Log("??");
        }
    }


    private void Completed()
    {
        timeOn = false;
        Debug.Log(timer);
        /*人クラスにタイムと終了したかを渡す関数呼び出し*/
        timer = 0;
        _gate = GateContoroller.Start;
        //Debug.Log("END");
    }
}
