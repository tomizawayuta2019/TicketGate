using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TicketType {
    paper,//切符
    paper_miss,//間違い切符
    suica,//電子カード
    suica_miss,//間違い電子カード
}
/// <summary>
/// 人が持っている情報
/// </summary>
public class HumanInfo : MonoBehaviour {
    // チケットの種類------------------------------------------------------------*
    private TicketType ticket;

    /// <summary>
    /// チケット種類獲得
    /// </summary>
    /// <returns></returns>
    public TicketType GetTicket() { return ticket; }

    /// <summary>
    /// チケット種類格納
    /// </summary>
    /// <param name="type"></param>
    public void SetTicket(TicketType type) { ticket = type; }
    // --------------------------------------------------------------------------*

    // 通過目標時間--------------------------------------------------------------*
    private float targetTime;

    /// <summary>
    /// 経過目標時間取得
    /// </summary>
    /// <returns></returns>
    public float GetTargetTime() { return targetTime; }

    /// <summary>
    /// 経過目標時間格納
    /// </summary>
    /// <param name="time"></param>
    public void SetTargetTime(float time) { targetTime = time; }
    // --------------------------------------------------------------------------*
}

/// <summary>
/// 人マネージャー
/// </summary>
public class HumanManager : MonoBehaviour {
    public Queue<HumanInfo> humanLines = new Queue<HumanInfo>();
    private GameObject _humanPrefab;

    // 移動に必要な変数--------*
    private Vector2 createPos;
    private Vector2 startPos;
    private Vector2 endPos;
    private float moveSpeed;
    // ------------------------*

    /// <summary>
    /// 人生成
    /// </summary>
    public void CreateHuman() {
        _humanPrefab = Resources.Load("Prefabs/HumanPrefab") as GameObject;
        createPos = GameObject.Find("Canvas/CreatePos").GetComponent<RectTransform>().localPosition;
        startPos = GameObject.Find("Canvas/StartPos").GetComponent<RectTransform>().localPosition;
        endPos = GameObject.Find("Canvas/EndPos").GetComponent<RectTransform>().localPosition;

        moveSpeed = StageController.instance.GetHumanMoveSpeed();

        // 生成--------------------------------*
        GameObject human = Instantiate(_humanPrefab);
        human.transform.SetParent(GameObject.Find("Canvas").transform);

        //human.transform.position = new Vector2(createPos.x - humanLines.Count * human.GetComponent<RectTransform>().sizeDelta.x, createPos.y);

        startPos = new Vector2(startPos.x - humanLines.Count * human.GetComponent<RectTransform>().sizeDelta.x, startPos.y);
        Debug.Log("startPos" + startPos);
        createPos = new Vector2(createPos.x - humanLines.Count * human.GetComponent<RectTransform>().sizeDelta.x, createPos.y);

        HumanMove hMove = human.AddComponent<HumanMove>();
        hMove.Init(createPos, startPos, endPos, moveSpeed);
        // ------------------------------------*

        // StartPosに移動
        hMove.GotoStartPoss(() => { Debug.Log("Complate");});
        
        // 人情報を配列に入れる---------------------*
        HumanInfo info = human.AddComponent<HumanInfo>();

        info.SetTicket(StageController.instance.GetRundTicketType());
        info.SetTargetTime(StageController.instance.GetHumanTargetTime());
        Debug.Log("TotalBefore:" + humanLines.Count);
        humanLines.Enqueue(info);
        Debug.Log("TotalAfter:" + humanLines.Count);
        // -----------------------------------------*
    }
}

public class StageController : MonoBehaviour {
    public static StageController instance;
    public StageState nowStage;
    private List<HumanLine> line = new List<HumanLine>();
    private int nowLineNumber = 0;
    private float nowLineTime;
    private int nowLineAddCount = 0;
    private float frustration;
    public float Frustration { get { return frustration; } }
    public string NowTimeName { get{ return nowLineNumber <= line.Count ? line[nowLineNumber].Name : "終業"; } }
    private int passHumanCount = 0;
    public int PassHumanCount { get { return passHumanCount; } }
    private float score;
    public float Score { get { return score; } }

    [HideInInspector]
    public HumanManager hManager;

    private static StageState NextStage = null;
    public static void SetStage(StageState value) {
        NextStage = value;
    }

    private void GetStage() {
        nowStage = NextStage;
        NextStage = null;
    }

    /// <summary>
    /// 合計就業時間の取得
    /// </summary>
    public float MaxTime { get {
            float sum = 0;
            foreach (HumanLine item in line) { sum += item.Time; }
            return sum; } }

    private void Awake()
    {
        instance = this;
        line = new List<HumanLine>(nowStage.humans);
        hManager = new HumanManager();
        GetStage();
    }

    private void Update()
    {
        HumanLineUpdate();
    }

    /// <summary>
    /// 人の流れの処理
    /// </summary>
    private void HumanLineUpdate() {
        //流れきったらreturn
        if (nowLineNumber >= line.Count) {
            return;
        }
        HumanLine nowLine = line[nowLineNumber];
        nowLineTime += Time.deltaTime;
        //時間が終了していたら次の時間帯へ
        if (nowLine.Time < nowLineTime)
        {
            NextHumanLine();
        }
        //一定時間ごとに人を追加する
        else if (nowLine.AddTime * (nowLineAddCount + 1) < nowLineTime)
        {
            AddHuman();
        }
    }

    /// <summary>
    /// 人の流れ方が変わった時に呼び出される処理
    /// </summary>
    private void UpdateHumanLine() {

    }

    /// <summary>
    /// 次の人の流れに切り替える処理
    /// </summary>
    private void NextHumanLine() {
        nowLineTime -= line[nowLineNumber].Time;
        nowLineNumber++;
        nowLineAddCount = 0;
        if (nowLineNumber == line.Count) {
            HumanLineEnd();
        }
    }

    /// <summary>
    /// 人の流れの終了
    /// </summary>
    private void HumanLineEnd() {

    }

    /// <summary>
    /// 人を追加する処理
    /// </summary>
    private void AddHuman() {
        hManager.CreateHuman();
        nowLineAddCount++;
    }

    /// <summary>
    /// 現在通過している人の目標時間を確認する処理
    /// </summary>
    /// <returns></returns>
    public float GetHumanTargetTime() {
        return line[nowLineNumber].TargetTime;
    }

    /// <summary>
    /// チケットのタイプを取得する処理
    /// </summary>
    /// <returns></returns>
    public TicketType GetRundTicketType() {
        return Random.Range(0f, 1f) < nowStage.SuicaPer ?
            Random.Range(0f, 1f) < nowStage.MissPer ? TicketType.suica_miss : TicketType.suica :
            Random.Range(0f, 1f) < nowStage.MissPer ? TicketType.paper_miss : TicketType.paper;
    }

    /// <summary>
    /// 人の動く速度を取得する処理（時間ごとにある程度ランダム）
    /// </summary>
    /// <returns></returns>
    public float GetHumanMoveSpeed() {
        return line[nowLineNumber].MoveSpeed + Random.Range(-line[nowLineNumber].RandomRange, line[nowLineNumber].RandomRange);
    }

    /// <summary>
    /// 不満度の加算
    /// </summary>
    /// <param name="value"></param>
    public void AddFrustration(float value) {
        frustration += value;
    }

    /// <summary>
    /// スコアの加算
    /// </summary>
    /// <param name="value"></param>
    public void AddScore(float value) {
        score += value;
    }

    /// <summary>
    /// 人の通過
    /// </summary>
    public void PassHuman() {
        passHumanCount++;
    }
}
