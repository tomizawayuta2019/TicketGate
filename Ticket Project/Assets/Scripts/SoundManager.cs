using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager> {
    public enum SoundType {
        gateClose,//ゲートを閉める
        gateEnter,//ゲート通過
        ticketIn,//チケット入れる
        ticketOut,//チケット出る
        suicaEnter,//スイカタッチ
        suicaMiss,//スイカ間違い
    }

    public enum BGMType {
        title,
        stageSelect,
        main_normal,
        main_fever,
        stageClear,
    }

    public List<SoundType> sounds = new List<SoundType>();

    public void PlaySound(SoundType value) {

    }

    public void PlayBGM(BGMType value) {

    }
}
