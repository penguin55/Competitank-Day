using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Create Library", fileName = "Audio Library")]
public class AudioLibrary : ScriptableObject
{
    public List<AudioData> audios;
}

[System.Serializable]
public class AudioData
{
    public string audioName;
    [Range(0,1)] public float offsetVolume;
    public AudioClip clip;
}
