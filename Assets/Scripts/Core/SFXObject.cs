using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SFXObject", menuName = "Data/SFXObject")]
public class SFXObject : ScriptableObject
{
    public AudioClip clip;
    public bool pitchModulation = true;
    public bool volumeModulation = true;
    public float volume = 1f;
}
