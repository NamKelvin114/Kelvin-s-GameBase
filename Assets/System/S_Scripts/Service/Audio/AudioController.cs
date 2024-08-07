using System;
using Cysharp.Threading.Tasks;
using Kelvin.Pool;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    [SerializeField] private Transform audioContainer;
    [SerializeField] private SoundEmitter soundEmitter;
    [SerializeField] private ScriptableBoolVariable isLoadingCompleted;
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
        musicEmiitor.PlayMusic(audioData, out var getCurrentMusicPlaying);
        _currentMusicPlaying = getCurrentMusicPlaying;
    }

    public void PlaySoundFx(SoundFXData audioData, Action actionWhenEndClip)
    {
        var spawnEmittor =
            PoolingObject.Instance.Spawn(_poolKey, soundEmitter.gameObject, audioContainer, null);
        var soundFXEmiitor = spawnEmittor.GetComponent<SoundEmitter>();
        soundFXEmiitor.KeyPool = _poolKey;
        soundFXEmiitor.PlaySoundFx(audioData, actionWhenEndClip, out var currentSoundFxPlaying);
        _currentSoundFxPlaying = currentSoundFxPlaying;
    }
}