using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReTimer : MonoBehaviour {

    private Text time_txt;
    void Start () {
        time_txt = GetComponentInChildren<Text>();
    }
	
	void Update () {
		
	}
    public int Timer()
    {
        return 0;
    }
}
