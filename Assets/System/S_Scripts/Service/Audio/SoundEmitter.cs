using System;
using System.Collections;
using Kelvin;
using Kelvin.Pool;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class SoundEmitter : CacheGameComponent<AudioSource>
{
    [Header("Volume")]
    [ReadOnly] public ScriptableFloatVariable volume;
    public string KeyPool { private get; set; }
    private Coroutine _emitterRoutine;
    protected override void Reset()
    {
        base.Reset();
    }
    public void Init(string keyPool, ScriptableFloatVariable getVolume)
    {
        KeyPool = keyPool;
        volume = getVolume;
        volume.OnRaise += VolumeChange;
    }
    void VolumeChange(float value)
    {
        component.volume = value;
    }

    private void OnDisable()
    {
        if (_emitterRoutine != null) StopCoroutine(_emitterRoutine);
        if (volume!=null)
        {
            volume.OnRaise -= VolumeChange;
        }
    }

    private void Play(AudioClip audioClip, bool loop, float volume, Action actionWhenEndClip = null,
        bool isDeSpawnWhenDone = true)
    {
        component.loop = loop;
        component.volume = volume;
        component.clip = audioClip;
        component.Play();
        if (!loop)
            _emitterRoutine = StartCoroutine(WaitForEndClip(audioClip.length, actionWhenEndClip, isDeSpawnWhenDone));
    }

    private IEnumerator WaitForEndClip(float lengthClip, Action actionEndClip, bool isDeSpawn = true)
    {
        yield return new WaitForSeconds(lengthClip);
        actionEndClip?.Invoke();
        if (isDeSpawn) PoolingObject.Instance.DeSpawn(KeyPool, gameObject);
    }
    public void StopPLay()
    {
        component.Stop();
    }


    public void PlaySoundFx(SoundFXData audioData, Action actionWhenEndClip, out AudioClip currentSoundFxPlaying)
    {
        AudioClip getSoundFX = null;
        currentSoundFxPlaying = null;
        switch (audioData.playMode)
        {
            case PlaySoundFXMode.Sequence:
                var index = -1;
                HandleSequence();

                void HandleSequence()
                {
                    index++;
                    getSoundFX = audioData.audioClips[index];
                    if (index >= audioData.audioClips.Count - 1)
                        Play(getSoundFX, false, audioData.audioVolume.Value, () => { actionWhenEndClip?.Invoke(); });
                    else
                        Play(getSoundFX, false, audioData.audioVolume.Value, () => { HandleSequence(); }, false);
                }

                break;
            case PlaySoundFXMode.RandomContinuous:
                HandleRandonContinuous();

                void HandleRandonContinuous()
                {
                    getSoundFX = audioData.audioClips[Random.Range(0, audioData.audioClips.Count)];
                    Play(getSoundFX, false, audioData.audioVolume.Value, () => { HandleRandonContinuous(); }, false);
                }

                break;
            case PlaySoundFXMode.RandomSingle:
                getSoundFX = audioData.audioClips[Random.Range(0, audioData.audioClips.Count)];
                Play(getSoundFX, false, audioData.audioVolume.Value, () => { actionWhenEndClip?.Invoke(); });
                break;
        }

        currentSoundFxPlaying = getSoundFX;
    }

    public void PlayMusic(MusicData audioData, out AudioClip currentMusicPlaying)
    {
        AudioClip getMusic = null;
        currentMusicPlaying = null;
        switch (audioData.playMode)
        {
            case PlayMusicMode.RandomSingle:
                getMusic = audioData.audioClips[Random.Range(0, audioData.audioClips.Count)];
                currentMusicPlaying = getMusic;
                break;
        }

        Play(getMusic, true, audioData.audioVolume.Value);
    }
}