using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 人が持っている情報
/// </summary>
public class HumanInfo
{
    // チケットの種類
    private TicketType ticket { get; set; }

    // 通過目標時間
    private float targetTime { get; set; }
}

public class HumanScript : MonoBehaviour
{
    private Queue<HumanInfo> humanInfos = new Queue<HumanInfo>();
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 人生成
    /// </summary>
    /// <param name="type"></param>
    /// <param name="time"></param>
    public void CreateHuman()
    {
        HumanInfo info = new HumanInfo();
        
    }

    /// <summary>
    /// チケットを入れた時の処理
    /// </summary>
    public void TicketIn()
    {

    }

    /// <summary>
    /// 不満度の加減処理
    /// </summary>
    /// <param name="finishTime"></param>
    /// <param name="check"></param>
    public void ActionComplate(float finishTime,bool check)
    {

    }
}
