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
        var spawnEmittor =
            PoolingObject.Instance.Spawn(_poolKey, soundEmitter.gameObject, audioContainer, null);
        var musicEmiitor = spawnEmittor.GetComponent<SoundEmitter>();
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