using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static TicketGate instance;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
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
        if (_gate == GateContoroller.Start) return;
        if (timeOn) { timer += Time.deltaTime; }

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
        Debug.Log(_ticket);
        switch (_ticket)
        {
            case TicketType.paper:
            case TicketType.paper_miss:
                _gate = GateContoroller.Ticket;
                StartCoroutine(TicketPreview(true));
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


    /// <summary>
    /// 切符が間違っているかどうかを取得(間違ってたらNO,あってたらTicket_OK1)
    /// </summary>
    private void TicketSort()
    {
        //切符が間違っているかどうかを取得(間違ってたらNO,あってたらTicket_OK1)
        if (_ticket == TicketType.paper)
        {
            _gate = GateContoroller.Ticket_OK1;
            Debug.Log("GOOD");
        }
        if (_ticket == TicketType.paper_miss)
        {
            _gate = GateContoroller.NO;
            Debug.Log("NO_GOOD");
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
            Debug.Log("??");
            return;
        }
        switch (gate)
        {
            case GateContoroller.Pasumo_OK:
                staffAnim.SetTrigger("pasumo");
                Completed();
                Debug.Log("↑");
                break;
            case GateContoroller.Ticket:
                staffAnim.SetTrigger("toGet");
                TicketSort();
                Debug.Log("←");
                break;
            case GateContoroller.Ticket_OK1:
                staffAnim.SetTrigger("toPush");
                _gate = GateContoroller.Ticket_OK2;
                Debug.Log("↓");
                break;
            case GateContoroller.Ticket_OK2:
                staffAnim.SetTrigger("toOut");
                Completed();
                Debug.Log("→");
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
        if (_gate == GateContoroller.NO)
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
        //HumanManager.instance.EndPosComplate();
    }


    private void Completed()
    {
        timeOn = false;
        Debug.Log(timer);
        /*人クラスにタイムと終了したかを渡す関数呼び出し*/
        HumanManager.instance.ActionComplete(timer);
        timer = 0;
        _gate = GateContoroller.Start;
        HumanManager.instance.EndPosComplate();
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

    private Vector2 defPos = new Vector2(0, -900);
    public IEnumerator TicketPreview(bool check) {
        GameObject prefab = Resources.Load("Prefabs/BigTicket") as GameObject;
        GameObject ticket = Instantiate(prefab)as GameObject;
        ticket.transform.SetParent(canvas.transform);
        RectTransform rect = ticket.GetComponent<RectTransform>();
        Vector2 targetPos = prefab.GetComponent<RectTransform>().localPosition;
        rect.localPosition = new Vector2(targetPos.x, defPos.y);


        while ((_gate == GateContoroller.Ticket || _gate == GateContoroller.Ticket_OK1 || _gate == GateContoroller.Ticket_OK2) && rect.localPosition.y < targetPos.y) {
            yield return null;
            rect.localPosition = (Vector2)rect.localPosition + (new Vector2(0, 2000) * Time.deltaTime);
        }

        //rect.localPosition = targetPos;
        while (_gate == GateContoroller.Ticket) { yield return null; }
        while (_gate == GateContoroller.Ticket_OK1) { yield return null; }
        while (_gate == GateContoroller.Ticket_OK2) { yield return null; }

        while (rect.localPosition.y > defPos.y) {
            yield return null;
            rect.localPosition = (Vector2)rect.localPosition - (new Vector2(0, 2000) * Time.deltaTime);
        }

        Destroy(ticket);
    }
}
