using System;
using System.Collections;
using Kelvin;
using Kelvin.Pool;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEmitter : CacheGameComponent<AudioSource>
{
    private Coroutine emitterRoutine;
    public string KeyPool { private get; set; }

    protected override void Reset()
    {
        base.Reset();
    }

    private void Play(AudioClip audioClip, bool loop, float volume, Action actionWhenEndClip = null)
    {
        component.loop = loop;
        component.volume = volume;
        component.clip = audioClip;
        component.Play();
        if (!loop) emitterRoutine = StartCoroutine(WaitForEndClip(audioClip.length, actionWhenEndClip));
    }

    private IEnumerator WaitForEndClip(float lengthClip, Action actionEndClip)
    {
        yield return new WaitForSeconds(lengthClip);
        actionEndClip?.Invoke();
        StopCoroutine(emitterRoutine);
        PoolingObject.Instance.DeSpawn(KeyPool, gameObject);
    }


    public void PlaySoundFx(AudioClip audioClip, bool loop, float volume, Action actionWhenEndClip)
    {
        Play(audioClip, loop, volume);
    }

    public void PlayMusic(AudioClip audioClip, float volume)
    {
        Play(audioClip, true, volume);
    }
}