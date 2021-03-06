﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicketGate : MonoBehaviour
{

    public enum GateContoroller
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
    [SerializeField]
    private GateContoroller _gate;
    //人のチケット情報enum
    private TicketType _ticket;

    public GameObject staff;
    private Animator staffAnim;
    [SerializeField]
    Canvas canvas;
    public GameObject setumei;
    private Image setumei_S;

    /// <summary>
    /// 0,← 1,→ 2,↑ 3,↓ 4,space
    /// </summary>
    public Sprite[] setumei_image = new Sprite[5];

    public static TicketGate instance;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        setumei_S = setumei.GetComponent<Image>();
        staffAnim = staff.GetComponent<Animator>();
        timeOn = false;
        timer = 0;
        // test用 inspectorで呼び出す
        //setTest();
    }

    /// <summary>
    /// test用
    /// </summary>
    [ContextMenu("start")]
    private void setTest()
    {
        int rand = Random.Range(1, 4);
        switch (rand)
        {
            case 1:
                SetTicket(TicketType.paper);
                break;
            case 2:
                SetTicket(TicketType.paper_miss);
                break;
            case 3:
                SetTicket(TicketType.suica);
                break;
            case 4:
                SetTicket(TicketType.suica_miss);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeManager.IsTimeStop || StageController.instance && StageController.instance.isGameEnd) { return; }
        if (_gate == GateContoroller.Start) return;
        if (timeOn) { timer += TimeManager.DeltaTime; }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TicketChoice(GateContoroller.Ticket);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            TicketChoice(GateContoroller.Ticket_OK1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TicketChoice(GateContoroller.Ticket_OK2);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TicketChoice(GateContoroller.Pasumo_OK);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceDoor();
        }
    }

    /// <summary>
    /// 人クラスから呼び出す(引数にチケットの種類入力)
    /// </summary>
    /// <param name="type"></param>
    public void SetTicket(TicketType type){
        //人の持っているチケット情報をこのスクリプトのenum変数に取得する
        _ticket = type;
        //チケットの種類を見て、_gateの値を変える(switch)
        switch (_ticket)
        {
            case TicketType.paper:
            case TicketType.paper_miss:
                _gate = GateContoroller.Ticket;
                StartCoroutine(TicketPreview(_ticket == TicketType.paper));
                SpriteChange(0);
                //Debug.Log("TICKET");
                break;
            case TicketType.suica:
                StartCoroutine(LampPreview(true));
                _gate = GateContoroller.Pasumo_OK;
                SpriteChange(2);
                //Debug.Log("PASUMO");
                break;
            case TicketType.suica_miss:
                StartCoroutine(LampPreview(false));
                _gate = GateContoroller.NO;
                SpriteChange(4);
                //Debug.Log("PASUMO_NO");
                break;
        }
        //タイマーを開始する
        timeOn = true;
    }


    /// <summary>
    /// 切符が間違っているかどうかを取得(間違ってたらNO,あってたらTicket_OK1)
    /// </summary>
    private void TicketSort()
    {
        //切符が間違っているかどうかを取得(間違ってたらNO,あってたらTicket_OK1)
        if (_ticket == TicketType.paper)
        {
            _gate = GateContoroller.Ticket_OK1;
            SpriteChange(3);
            //Debug.Log("GOOD");
        }
        if (_ticket == TicketType.paper_miss)
        {
            _gate = GateContoroller.NO;
            SpriteChange(4);
            //Debug.Log("NO_GOOD");
        }
    }


    /// <summary>
    /// 特定の列挙かどうか判断、および列挙変更(スペース以外)
    /// </summary>
    /// <param name="gate"></param>
    private void TicketChoice(GateContoroller gate)
    {
        if (_gate != gate)
        {
            //ポコポコ怒りアニメーション
            //Debug.Log("??");
            return;
        }
        switch (gate)
        {
            case GateContoroller.Pasumo_OK:
                staffAnim.SetTrigger("pasumo");
                Completed();
                //Debug.Log("↑");
                EffectManager.instance.PlayEffect(EffectManager.EffectType.Peep, new Vector2(-300, 0));
                SoundManager.instance.PlaySE(Random.Range(0, 3) == 0 ? SoundManager.SoundType.peep_ka : SoundManager.SoundType.peep_ra);
                SpriteChange(5);
                break;
            case GateContoroller.Ticket:
                staffAnim.SetTrigger("toGet");
                TicketSort();
                //Debug.Log("←");
                break;
            case GateContoroller.Ticket_OK1:
                staffAnim.SetTrigger("toPush");
                _gate = GateContoroller.Ticket_OK2;
                SpriteChange(1);
                //Debug.Log("↓");
                break;
            case GateContoroller.Ticket_OK2:
                staffAnim.SetTrigger("toOut");
                SpriteChange(5);
                Completed();
                //Debug.Log("→");
                break;
            default:
                return;
        }
    }


    /// <summary>
    /// スペース時の判別
    /// </summary>
    private void SpaceDoor()
    {
        staffAnim.SetTrigger("toGate");
        if (_gate == GateContoroller.NO || (_gate == GateContoroller.Ticket && _ticket == TicketType.paper_miss))
        {
            //正しく止めたことを人クラスに渡す（関数呼び出し）
            //Debug.Log("SPACE");
            HumanManager.instance.GateClose(true);
        }
        else
        {
            //間違えて止めたことを人クラスに渡す（関数呼び出し）
            //Debug.Log("SPACE_BAD");
            HumanManager.instance.GateClose(false);
        }
        timeOn = false;
        timer = 0;
        _gate = GateContoroller.Start;
        SpriteChange(5);
        EffectManager.instance.PlayEffect(EffectManager.EffectType.cutIn, Vector3.zero);
        SoundManager.instance.PlaySE(SoundManager.SoundType.Stop);
        //HumanManager.instance.EndPosComplate();
    }


    private void Completed()
    {
        timeOn = false;
        //Debug.Log(timer);
        /*人クラスにタイムと終了したかを渡す関数呼び出し*/
        HumanManager.instance.ActionComplete(timer);
        timer = 0;
        _gate = GateContoroller.Start;
        HumanManager.instance.EndPosComplate();
        SpriteChange(5);
        Debug.Log("END");
    }

    public IEnumerator WaitTicketTiming(System.Action action) {
        GateContoroller nowType = _gate;
        while (nowType == _gate) {
            yield return null;
        }

        action();
    }

    public IEnumerator WaitStartTiming(System.Action action)
    {
        while (HumanManager.instance.IsMoceNow || GateContoroller.Start != _gate)
        {
            yield return null;
        }

        action();
    }

    private Vector2 defPos = new Vector2(0, -500);
    private IEnumerator TicketPreview(bool check) {
        GameObject prefab = Resources.Load(check ? "Prefabs/BigTicket" : "Prefabs/BigTicket_miss") as GameObject;
        GameObject ticket = Instantiate(prefab)as GameObject;
        ticket.transform.SetParent(canvas.transform);
        RectTransform rect = ticket.GetComponent<RectTransform>();
        Vector2 targetPos = prefab.GetComponent<RectTransform>().localPosition;
        rect.localPosition = new Vector2(targetPos.x, defPos.y);


        while ((_gate == GateContoroller.Ticket || _gate == GateContoroller.Ticket_OK1 || _gate == GateContoroller.Ticket_OK2) && rect.localPosition.y < targetPos.y) {
            yield return null;
            rect.localPosition = (Vector2)rect.localPosition + (new Vector2(0, 2000) * TimeManager.DeltaTime);
        }

        //rect.localPosition = targetPos;
        while (_gate == GateContoroller.Ticket) { yield return null; }
        while (_gate == GateContoroller.Ticket_OK1) { yield return null; }
        while (_gate == GateContoroller.Ticket_OK2) { yield return null; }

        while (rect.localPosition.y > defPos.y) {
            yield return null;
            rect.localPosition = (Vector2)rect.localPosition - (new Vector2(0, 2000) * TimeManager.DeltaTime);
        }

        Destroy(ticket);
    }

    private IEnumerator LampPreview(bool check) {
        //Debug.Log("lamp");
        GameObject prefab = Resources.Load(check ? "Prefabs/Lamp_Green" : "Prefabs/Lamp_Red") as GameObject;
        GameObject lamp = Instantiate(prefab);
        lamp.transform.SetParent(canvas.transform);
        lamp.GetComponent<RectTransform>().localPosition = Vector2.zero;
        lamp.GetComponent<RectTransform>().localScale = Vector2.one;

        yield return new WaitForSeconds(0.1f);
        while (_gate == GateContoroller.NO || _gate == GateContoroller.Pasumo_OK) { yield return null; }
        Destroy(lamp);
    }


    /// <summary>
    /// 説明用画像変更
    /// 0,← 1,→ 2,↑ 3,↓ 4,space 5,null
    /// </summary>
    /// <param name="num">0,← 1,→ 2,↑ 3,↓ 4,space 5,null</param>
    private void SpriteChange(int num)
    {
        setumei_S.color = new Color(1, 1, 1, 1);
        switch (num)
        {
            case 0:
                setumei_S.sprite = setumei_image[0];
                break;
            case 1:
                setumei_S.sprite = setumei_image[1];
                break;
            case 2:
                setumei_S.sprite = setumei_image[2];
                break;
            case 3:
                setumei_S.sprite = setumei_image[3];
                break;
            case 4:
                setumei_S.sprite = setumei_image[4];
                break;
            case 5:
                setumei_S.sprite = null;
                setumei_S.color = new Color(1, 1, 1, 0);
                break;
        }
        if (num == 5) return;
        setumei.GetComponent<RectTransform>().sizeDelta = setumei_S.sprite.rect.size;
    }
}
