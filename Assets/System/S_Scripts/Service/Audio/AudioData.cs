using System.Collections.Generic;
using UnityEngine;

public abstract class AudioData : ScriptableObject
{
    [Range(0, 1)] public float audioVolume;
    public List<AudioClip> audioClips;
}

public enum PlayMusicMode
{
    RandomSingle
}

public enum PlaySoundFXMode
{
    Sequence,
    RandomSingle,
    RandomContinuous
}