using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 人が持っている情報
/// </summary>
public class HumanInfo : MonoBehaviour
{
    // チケットの種類-----------------------------*
    private TicketType ticket;

    /// <summary>
    /// チケット種類獲得
    /// </summary>
    /// <returns></returns>
    public TicketType GetTicket() {return ticket;}

    /// <summary>
    /// チケット種類格納
    /// </summary>
    /// <param name="type"></param>
    public void SetTicket(TicketType type) {ticket = type;}
    // -----------------------------------------*

    // 通過目標時間------------------------------*
    private float targetTime;

    /// <summary>
    /// 経過目標時間取得
    /// </summary>
    /// <returns></returns>
    public float GetTargetTime() {return targetTime;}

    /// <summary>
    /// 経過目標時間格納
    /// </summary>
    /// <param name="time"></param>
    public void SetTargetTime(float time) {targetTime = time;}
    // -----------------------------------------*
}

/// <summary>
/// 全体処理
/// </summary>
public class HumanScript : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    // 待機列
    private Queue<HumanInfo> humanLines = new Queue<HumanInfo>();

    [SerializeField,Header("人間Prefab")]
    private GameObject _humanPrefab;

    void Awake() {
        
    }

    void Start() {
        
    }

    /// <summary>
    /// 人生成
    /// </summary>
    /// <param name="type"></param>
    /// <param name="time"></param>
    public void CreateHuman()
    {
        // 人情報を配列に入れる---------------------*
        HumanInfo info = new HumanInfo();
        info.SetTicket(StageController.instance.GetRundTicketType());
        info.SetTargetTime(StageController.instance.GetHumanTargetTime());
        humanLines.Enqueue(info);
        // ---------------------------------------*

        // 生成
        GameObject human = Instantiate(_humanPrefab);
        human.transform.SetParent(canvas.transform);

    }

    /// <summary>
    /// チケットを入れた時の処理
    /// </summary>
    public void TicketIn()
    {

    }

    /// <summary>
    /// 人通行後の不満度加減処理
    /// </summary>
    /// <param name="finishTime"></param>
    /// <param name="check"></param>
    public void ActionComplate(float finishTime,bool check)
    {

    }
}
