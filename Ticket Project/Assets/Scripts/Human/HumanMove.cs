using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumanMove : MonoBehaviour {

    private Vector2 startPos, endPos;
    private Sprite walk, pass;
    Vector2? waitingPos = null;
    [SerializeField]
    private float moveSpeed;
    private const float outScreenDistance = 200;//改札から出てどれくらいまで歩くか
    private Coroutine move;
    RectTransform rect;
    RectTransform targetHumanRect;//前を歩いている人のRect

    public bool isGateStart = false;

    public static RectTransform defRect;//直前に生成されたHumanのRect

    public void Awake()
    {
        targetHumanRect = defRect;
        defRect = GetComponent<RectTransform>();
        if (!waitingPos.HasValue) {
            waitingPos = new Vector2(Mathf.Infinity, GetComponent<RectTransform>().localPosition.y);
        }
    }

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
        rect = GetComponent<RectTransform>();
        rect.localPosition = initPos;
        this.startPos = startPos;
        this.endPos = endPos;
        this.moveSpeed = moveSpeed;
    }

    public void SetSprite(Sprite walk,Sprite pass) {
        this.walk = walk;
        this.pass = pass;
        ChangeSprite(walk);
    }

    private void ChangeSprite(Sprite value) {
        GetComponent<Image>().sprite = value;
        rect.sizeDelta = value.rect.size;
    }

    public void SetWaitingPos(Vector2 waitingPos) {
        this.waitingPos = waitingPos;
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
        move = StartCoroutine(Move(startPos, ()=> { comp();isGateStart = true;ChangeSprite(pass); }));
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
        ChangeSprite(walk);
        move = StartCoroutine(Move(endPos, comp));
    }

    /// <summary>
    /// 画面外まで移動開始 移動が終わると自動でDestroyされる
    /// </summary>
    public void GotoOutScreen() {
        MoveStop();
        move = StartCoroutine(Move(endPos + new Vector2(outScreenDistance, 0), () => Destroy(gameObject)));
    }

    public void ReturnToGate() {
        //MoveStop();
        //move = StartCoroutine(Move(-endPos - new Vector2(outScreenDistance, 0), () => Destroy(gameObject)));
        Destroy(gameObject);
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
            rect.localPosition = (Vector2)rect.localPosition + new Vector2(moveSpeed * Time.deltaTime, 0);
            if (GetTargetHumanWaitingPoss().x <= rect.localPosition.x)
            {
                rect.localPosition = new Vector2(GetTargetHumanWaitingPoss().x, rect.localPosition.y);
                continue;
            }
            if (waitingPos.HasValue && waitingPos.Value.x <= rect.localPosition.x)
            {
                rect.localPosition = waitingPos.Value;
                continue;
            }
        } while (rect.localPosition.x < targetPos.x);

        rect.localPosition = new Vector2(targetPos.x, rect.localPosition.y);

        //transform.position = targetPos;
        if (comp != null) { comp(); }
    }

    /// <summary>
    /// 直前に生成された人の直前の位置を取得する
    /// </summary>
    /// <returns></returns>
    private Vector2 GetTargetHumanWaitingPoss() {
        if (!targetHumanRect) { return new Vector2(Mathf.Infinity, 0); }
        return (Vector2)targetHumanRect.localPosition - targetHumanRect.sizeDelta;
    }
}
