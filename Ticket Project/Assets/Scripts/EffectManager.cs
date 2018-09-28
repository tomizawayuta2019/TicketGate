using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : SingletonMonoBehaviour<EffectManager> {
    public enum EffectType {
        Peep,//「ピッ」の電子音
        cutIn,//カットイン表示
    }

    public List<GameObject> effectPrefabs = new List<GameObject>();
    [SerializeField]
    GameObject canvas;


    protected override void Awake()
    {
        if (instance) {
            Destroy(gameObject);
            return;
        }
        base.Awake();
        if (canvas == null) { canvas = GameObject.Find("UICanvas"); }
    }

    public GameObject PlayEffect(EffectType value,Vector3 position) {
        GameObject effect = Instantiate(effectPrefabs[(int)value]);
        effect.transform.SetParent(canvas.transform);
        effect.GetComponent<RectTransform>().localPosition = position;
        effect.GetComponent<RectTransform>().localScale = effectPrefabs[(int)value].GetComponent<RectTransform>().localScale;
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
