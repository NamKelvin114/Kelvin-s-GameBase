using System;
using System.Collections;
using Kelvin;
using Kelvin.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class SoundEmitter : CacheGameComponent<AudioSource>
{
    private Coroutine emitterRoutine;
    public string KeyPool { private get; set; }

    protected override void Reset()
    {
        base.Reset();
    }

    private void OnDisable()
    {
        if (emitterRoutine != null) StopCoroutine(emitterRoutine);
    }

    private void Play(AudioClip audioClip, bool loop, float volume, Action actionWhenEndClip = null,
        bool isDeSpawnWhenDone = true)
    {
        component.loop = loop;
        component.volume = volume;
        component.clip = audioClip;
        component.Play();
        if (!loop)
            emitterRoutine = StartCoroutine(WaitForEndClip(audioClip.length, actionWhenEndClip, isDeSpawnWhenDone));
    }

    private IEnumerator WaitForEndClip(float lengthClip, Action actionEndClip, bool isDeSpawn = true)
    {
        yield return new WaitForSeconds(lengthClip);
        actionEndClip?.Invoke();
        if (isDeSpawn) PoolingObject.Instance.DeSpawn(KeyPool, gameObject);
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
                        Play(getSoundFX, false, audioData.audioVolume, () => { actionWhenEndClip?.Invoke(); });
                    else
                        Play(getSoundFX, false, audioData.audioVolume, () => { HandleSequence(); }, false);
                }

                break;
            case PlaySoundFXMode.RandomContinuous:
                HandleRandonContinuous();

                void HandleRandonContinuous()
                {
                    getSoundFX = audioData.audioClips[Random.Range(0, audioData.audioClips.Count)];
                    Play(getSoundFX, false, audioData.audioVolume, () => { HandleRandonContinuous(); }, false);
                }

                break;
            case PlaySoundFXMode.RandomSingle:
                getSoundFX = audioData.audioClips[Random.Range(0, audioData.audioClips.Count)];
                Play(getSoundFX, false, audioData.audioVolume, () => { actionWhenEndClip?.Invoke(); });
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

        Play(getMusic, true, audioData.audioVolume);
    }
}