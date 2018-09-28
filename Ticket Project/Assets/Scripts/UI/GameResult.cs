using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResult : SingletonMonoBehaviour<GameResult> {

    [SerializeField]
    GameObject finishgame; // ボタンを子要素にもつ空のオブジェクト
    [SerializeField]
    GameObject header;　// スコアや不満度などを子要素にもつ電光掲示板header

    bool IsFinish = false;

    private void Update()
    {
        if (IsFinish) {
            Vector3 pos = header.GetComponent<RectTransform>().localPosition;
            if (pos.y > -350)
            {
                header.GetComponent<RectTransform>().localPosition -= new Vector3(0, 10, 0);
            }
            else
            {
                finishgame.SetActive(true);
            }
        }
    }

    public void FinishGame() // ゲーム終了時に呼ぶ
    {
        IsFinish = true;
    }
}
