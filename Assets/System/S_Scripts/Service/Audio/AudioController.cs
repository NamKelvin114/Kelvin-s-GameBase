using System;
using Cysharp.Threading.Tasks;
using Kelvin.Pool;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    [Header("Properties")]
    [SerializeField] private Transform audioContainer;
    [SerializeField] private SoundEmitter soundEmitter;
    [SerializeField] private ScriptableBoolVariable isLoadingCompleted;
    [Header("Volume")]
    [SerializeField]
    private ScriptableFloatVariable musicVolume;
    [SerializeField] private ScriptableFloatVariable soundFXVolume;
    private AudioClip _currentMusicPlaying;
    private AudioClip _currentSoundFxPlaying;
    private SoundEmitter _musicEmittor;
    private string _poolKey;

    protected override void Awake()
    {
        base.Awake();
        UniTask.WaitUntil(() => isLoadingCompleted.Value);
        _poolKey = PoolingObject.Instance.newKey;
        PoolingObject.Instance.PreSpawn(_poolKey, soundEmitter.gameObject, 10, audioContainer);
    }


    public void PlayMusic(MusicData audioData)
    {
        if (_musicEmittor != null) PoolingObject.Instance.DeSpawn(_poolKey, _musicEmittor.gameObject);
        var spawnEmittor =
            PoolingObject.Instance.Spawn(_poolKey, soundEmitter.gameObject, audioContainer, null);
        var musicEmiitor = spawnEmittor.GetComponent<SoundEmitter>();
        _musicEmittor = musicEmiitor;
        _musicEmittor.Init(_poolKey,musicVolume);
        musicEmiitor.PlayMusic(audioData, out var getCurrentMusicPlaying);
        _currentMusicPlaying = getCurrentMusicPlaying;
    }
    public void StopMusic()
    {
        _musicEmittor.StopPLay();
        PoolingObject.Instance.DeSpawn(_poolKey, _musicEmittor.gameObject);
        _musicEmittor = null;
    }

    public void PlaySoundFx(SoundFXData audioData, Action actionWhenEndClip)
    {
        var spawnEmittor =
            PoolingObject.Instance.Spawn(_poolKey, soundEmitter.gameObject, audioContainer, null);
        var soundFXEmiitor = spawnEmittor.GetComponent<SoundEmitter>();
        soundFXEmiitor.Init(_poolKey,soundFXVolume);
        soundFXEmiitor.PlaySoundFx(audioData, actionWhenEndClip, out var currentSoundFxPlaying);
        _currentSoundFxPlaying = currentSoundFxPlaying;
    }
}