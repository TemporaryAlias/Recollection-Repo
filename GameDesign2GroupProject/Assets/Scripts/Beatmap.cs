using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBeatmap")]
public class Beatmap : ScriptableObject {

    [Header("Settings")]
    public Texture2D beatMapTexture;
    public float beatSpeed;
    public float lineInterval;

}
