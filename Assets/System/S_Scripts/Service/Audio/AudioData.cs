using System.Collections.Generic;
using UnityEngine;

public abstract class AudioData : ScriptableObject
{
    public ScriptableFloatVariable audioVolume;
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