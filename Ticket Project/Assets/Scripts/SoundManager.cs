using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager> {
    public enum SoundType {
        ticketIn,//チケット入れる
        suicaIn,//
        Stop,//
        Train,//
        zawameki,//
        announce,//
        rushTime,//
    }

    public enum BGMType {
        main,
        title,
        stageSelect,
        main_normal,
        main_fever,
        stageClear,
    }

    public List<AudioClip> sounds = new List<AudioClip>();
    public List<AudioClip> bgm = new List<AudioClip>();
    
    private AudioSource bgmSource;
    private Coroutine bgm_fade;

    private Dictionary<SoundType, AudioSource> playSEList = new Dictionary<SoundType, AudioSource>();

    protected override void Awake()
    {
        if (instance) {
            Destroy(gameObject);
            return;
        }
        base.Awake();
    }

    private void Start()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        PlayBGM(BGMType.main,true,1.0f);
    }

    private bool IsSameSE(SoundType key) {
        //同じSEが鳴っているか確認する
        if (playSEList.ContainsKey(key)) {
            if (playSEList[key] == null) {
                playSEList.Remove(key);
                return false;
            }
            return true;
        }
        return false;
    }

    public AudioSource PlaySE(SoundType value,bool isDontPlaySameSE = false) {
        if (isDontPlaySameSE && IsSameSE(value)) {
            return null;
        }
        AudioSource audio = new GameObject().AddComponent<AudioSource>();
        audio.clip = sounds[(int)value];
        audio.Play();
        audio.gameObject.AddComponent<SEDest>();
        playSEList[value] = audio;
        return audio;
    }

    public AudioSource PlayBGM(BGMType value,bool isLoop = true,float fadeTime = 0) {
        AudioSource audio = bgmSource;
        if (fadeTime > 0)
        {
            if (audio.isPlaying)
            {
                StopCoroutine(bgm_fade);
                System.Func<bool> comp = () =>
                {
                    bgm_fade = StartCoroutine(BGM_Fade(bgmSource, 1, fadeTime / 2));
                    bgmSource.clip = bgm[(int)value];
                    bgmSource.loop = isLoop;
                    bgmSource.Play();
                    return true;
                };
                bgm_fade = StartCoroutine(BGM_Fade(bgmSource, 0, fadeTime / 2, comp));
            }
            else {
                audio.volume = 0;
                bgm_fade = StartCoroutine(BGM_Fade(bgmSource, 1, fadeTime));
                bgmSource.clip = bgm[(int)value];
                bgmSource.loop = isLoop;
                bgmSource.Play();
            }
        }
        else {
            audio.clip = bgm[(int)value];
            audio.loop = isLoop;
            audio.Play();
        }

        return audio;
    }

    public void StopBGM() {
        bgmSource.Stop();
    }

    public void ChangeBGMSpeed(float speed) {
        bgmSource.pitch = speed;
    }

    private IEnumerator BGM_Fade(AudioSource target,float targetVolume,float time,System.Func<bool> complete = null)
    {
        float defVolume = target.volume, deltaVolume = targetVolume - defVolume;
        float nowTime = 0;

        while ((nowTime += Time.deltaTime) < time) {
            target.volume = defVolume + (deltaVolume * (nowTime / time));
            yield return null;
        }

        target.volume = targetVolume;

        if (complete != null) { complete(); }
    }
}

public class SEDest : MonoBehaviour {
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (audioSource && !audioSource.isPlaying) {
            Destroy(gameObject);
        }
    }
}
