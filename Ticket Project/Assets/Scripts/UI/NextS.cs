using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextS : MonoBehaviour {
    public void OnClick()
    {
        // nextstageへ移行
        SceneManager.LoadScene("Main");
    }
}
