﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : SingletonMonoBehaviour<EffectManager> {
    public enum EffectType {
        frustration,//不満表示
        gateClose,//ゲートを閉める
        gateHit,//ゲートにぶつかる
        cutIn,//カットイン表示
    }

    public List<GameObject> effectPrefabs = new List<GameObject>();

    public GameObject PlayEffect(EffectType value,Vector3 position) {
        GameObject effect = Instantiate(effectPrefabs[(int)value]);
        effect.transform.position = position;
        effect.AddComponent<EffectDest>();
        return effect;
    }
}

public class EffectDest : MonoBehaviour {
    Animation animator;

    private void Awake()
    {
        animator = GetComponent<Animation>();
    }

    private void Update()
    {
        if (animator && !animator.isPlaying) {
            Destroy(gameObject);
        }
    }

}
