using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour {
    [SerializeField] float speed; //フェードするスピード
    Image fade_back; //フェードの背景      
    float fadeA;    //α値
    [SerializeField] public bool outflag = false;
    [SerializeField] public bool inflag = false;
    public static Fade instance;
    private string sceneName = "";

    private void Awake()
    {
        if (instance) {
            Destroy(gameObject);
            return;
            }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {

        fade_back = GameObject.Find("Fade_Back").GetComponent<Image>();
        fade_back.enabled = false;
        fadeA = fade_back.color.a;

        inflag = true;

    }
	
	// Update is called once per frame
	void Update () {
        if(inflag == true){
            fadein();
        }
        if(outflag == true){
            fadeout();
        }
    }

    public void FadeStart(string sceneName) {
        this.sceneName = sceneName;
        outflag = true;
    }

    void fadeout()
    {
        fade_back.enabled = true;
        fadeA += speed;
        fade_back.color = new Color(0, 0, 0, fadeA);
        if (fadeA >= 1)
        {
            outflag = false;
            if (sceneName != "")
            {
                StageChange(sceneName);
                sceneName = "";
            }
            inflag = true;  
        }
    }

    void fadein()
    {
        fade_back.enabled = true;
        fadeA -= speed;
        fade_back.color = new Color(0, 0, 0, fadeA);
        if (fadeA <= 0)
        {
            inflag = false;
            fade_back.enabled = false;
        }
    }

    private void StageChange(string SceneName)   //ステージ移行
    {
        SceneManager.LoadScene(SceneName);
    }

}
