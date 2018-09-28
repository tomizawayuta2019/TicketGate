using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_StageSelectButton : MonoBehaviour {

    public void OnClick(string sceneName)
    {
        //SceneManager.LoadScene(sceneName); //ステージセレクトへ移動
        Fade.instance.FadeStart(sceneName);
    }
}
