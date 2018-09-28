using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HumanSprite : ScriptableObject {
    [System.Serializable]
    public struct SpriteSet {
        public Sprite walk, pass;
    }

    public List<SpriteSet> list;

    public SpriteSet GetRndHuman() {
        return list[Random.Range(0, list.Count)];
    }
}
