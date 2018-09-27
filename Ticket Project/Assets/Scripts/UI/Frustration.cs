using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frustration : MonoBehaviour {
    [SerializeField]
    private Image fru_img;

    public int fru_num; //客の不満度の値
    private float fru_total;

	void Start () {
        //ゲーム開始時に不満度を0に
        fru_img.fillAmount = 0.0f;		
	}
	
	void Update () {
        fru_img.fillAmount = fru_total;
	}
    /// <summary>
    /// 不満度加算関数
    /// </summary>
    /// <param name="num">客の不満度</param>
    /// <returns>不満度合計</returns>
    public float PlusFru(int num)
    {
        fru_total += num / 100; 

        if (fru_total >= 1.0f)
        {
            fru_total = 1.0f;
        }
        return fru_total;
    }
}
