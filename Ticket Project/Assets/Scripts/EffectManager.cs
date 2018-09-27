using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : SingletonMonoBehaviour<EffectManager> {
    public enum EffectType {
        frustration,//不満表示
        gateClose,//ゲートを閉める
        gateHit,//ゲートにぶつかる
        cutIn,//カットイン表示
    }

    public void PlayEffect(EffectType value) {

    }
}
