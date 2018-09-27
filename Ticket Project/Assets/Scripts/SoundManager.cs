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

    public List<AudioClip> sounds = new List<AudioClip>();
    public List<AudioClip> bgm = new List<AudioClip>();

    private AudioSource bgmSource;

    private void Start()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
    }

    public AudioSource PlaySE(SoundType value) {
        AudioSource audio = new GameObject().AddComponent<AudioSource>();
        audio.clip = sounds[(int)value];
        return audio;
    }

    public AudioSource PlayBGM(BGMType value) {
        AudioSource audio = bgmSource;
        audio.clip = bgm[(int)value];
        return audio;
    }

    public void StopBGM() {
        bgmSource.Stop();
    }
}
