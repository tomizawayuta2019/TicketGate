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
    float time;
    public float Time { get { return time; } }
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

    public int firstHour = 6, endHour = 25;//始業、終業時刻

    public List<HumanLine> humans = new List<HumanLine>();


    public void SetMaxScore(float value) {
        if (value > maxScore) { maxScore = value; }
    }

    public void SetClear() {
        if (!isClear) { isClear = true; }
    }
}
