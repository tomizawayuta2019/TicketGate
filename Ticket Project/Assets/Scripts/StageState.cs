using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 人間の出現頻度
/// </summary>
[System.Serializable]
public struct HumanLine {
    [SerializeField]
    string name;
    public string Name { get { return name; } }
    [SerializeField]
    [Range(0.1f, 5)]
    float addTime;//何秒ごとに人が来るか
    public float AddTime { get { return addTime; } }
    [SerializeField]
    [Range(0.1f, 5)]
    float targetTime;//目標時間
    public float TargetTime { get { return targetTime; } }
    [SerializeField]
    float time;//終了までの時間
    public float Time { get { return time; } }
    [SerializeField]
    float moveSpeed;//移動速度
    public float MoveSpeed { get { return moveSpeed; } }
    [SerializeField]
    [Range(0, 5f)]
    float randomRange;
    public float RandomRange { get { return randomRange; } }
}

[CreateAssetMenu]
public class StageState : ScriptableObject {
    [SerializeField]
    string stageName;
    public string StageName { get { return stageName; } }
    [SerializeField]
    int stageID;
    public int StageID { get { return stageID; } }
    [SerializeField]
    bool isClear;
    public bool IsClear { get { return isClear; } set { isClear = value; } }
    [SerializeField]
    float maxScore;
    public float MaxScore { get { return maxScore; } }


    [SerializeField]
    [Range(0, 1)]
    float suicaPer;//電子チケットの確立
    public float SuicaPer { get { return suicaPer; } }
    [SerializeField]
    float missPer;//間違ったチケットの可能性
    public float MissPer { get { return missPer; } }

    private int startHour = 6, endHour = 25;//始業、終業時刻
    public int StartHour { get { return startHour; } }
    public int EndHour { get { return endHour; } }

    public HumanSprite humanSprite;
    public List<HumanLine> humans = new List<HumanLine>();

    private string IsClearKey { get{ return "Stage" + StageID + "IsClear"; } }
    private string MaxScoreKey { get { return "Stage" + StageID + "MaxScore"; } }

    private void Awake()
    {
        //各種情報をロードする
        isClear = PlayerPrefs.GetInt(IsClearKey, 0) == 1;
        maxScore = PlayerPrefs.GetFloat(MaxScoreKey, 0);
    }

    public void SetMaxScore(float value) {
        if (value <= MaxScore) { return; }
        maxScore = value;
        PlayerPrefs.SetFloat(MaxScoreKey, MaxScore);
        PlayerPrefs.Save();
    }

    public void SetClear() {
        if (IsClear) { return; }
        isClear = true;
        PlayerPrefs.SetInt(IsClearKey, IsClear ? 1 : 0);
        PlayerPrefs.Save();
    }
}
