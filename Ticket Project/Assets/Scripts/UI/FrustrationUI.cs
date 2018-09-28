using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrustrationUI : MonoBehaviour
{

    [SerializeField]
    private Image fru_img;

    StageController stagecont;

    private float fru_total;    //合計不満度

    void Start()
    {
        //ゲーム開始時に不満度を0に
        fru_img.fillAmount = 0.0f;
        stagecont = StageController.instance;
    }

    void Update()
    {
        fru_total = stagecont.Frustration / StageController.maxFrustration;
        fru_img.fillAmount = fru_total;
    }
}
