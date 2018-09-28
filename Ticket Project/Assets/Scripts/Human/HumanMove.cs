using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMove : MonoBehaviour {

    private Vector2 startPos, endPos;
    [SerializeField]
    private float moveSpeed;
    private const float outScreenDistance = 10;//改札から出てどれくらいまで歩くか
    private Coroutine move;

    //デバッグ用
    //private void Awake()
    //{
    //    Init(new Vector2(-6, 0), new Vector2(-3, 0), new Vector2(3, 0), StageController.instance.GetHumanMoveSpeed());
    //    GotoStartPoss(() => GotoEndPos(() => GotoOutScreen()));
    //}

    /// <summary>
    /// 各種初期化
    /// </summary>
    /// <param name="startPos">改札の入口（チケット入れるとこ）</param>
    /// <param name="endPos">改札の出口（チケット取るとこ）</param>
    /// <param name="moveSpeed">移動速度</param>
    public void Init(Vector2 initPos,Vector2 startPos,Vector2 endPos,float moveSpeed) {
        transform.position = initPos;
        this.startPos = startPos;
        this.endPos = endPos;
        this.moveSpeed = moveSpeed;
    }

    /// <summary>
    /// 改札まで移動開始
    /// </summary>
    /// <param name="comp">
    /// 移動が完了したときに呼びたい処理
    /// GotoStartPoss(() => 移動完了関数)　みたいな感じに呼べます。
    /// GotoStartPoss(() => { 何かの処理 })　でも呼べます。複数行も可。
    /// </param>
    public void GotoStartPoss(System.Action comp) {
        MoveStop();
        move = StartCoroutine(Move(startPos, comp));
    }

    /// <summary>
    /// 改札の出口まで移動開始
    /// </summary>
    /// <param name="comp">
    /// 移動完了時の処理
    /// GotoStartPoss(() => 移動完了関数)　みたいな感じに呼べます。
    /// GotoStartPoss(() => { 何かの処理 })　でも呼べます。複数行も可。
    /// </param>
    public void GotoEndPos(System.Action comp) {
        MoveStop();
        move = StartCoroutine(Move(endPos, comp));
    }

    /// <summary>
    /// 画面外まで移動開始 移動が終わると自動でDestroyされる
    /// </summary>
    public void GotoOutScreen() {
        MoveStop();
        move = StartCoroutine(Move(endPos + new Vector2(outScreenDistance, 0), () => Destroy(gameObject)));
    }

    /// <summary>
    /// 移動の停止
    /// </summary>
    public void MoveStop() {
        if (move != null) { StopCoroutine(move); }
        move = null;
    }

    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="targetPos">目標地点</param>
    /// <param name="comp">移動が完了した際に呼びたい処理</param>
    /// <returns></returns>
    private IEnumerator Move(Vector2 targetPos,System.Action comp) {

        do {
            yield return null;
            transform.position = (Vector2)transform.position + new Vector2(moveSpeed * Time.deltaTime, 0);
        } while (transform.position.x < targetPos.x);

        transform.position = targetPos;
        if (comp != null) { comp(); }
    }
}
