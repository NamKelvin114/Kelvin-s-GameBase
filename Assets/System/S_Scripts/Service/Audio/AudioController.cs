using System;
using Cysharp.Threading.Tasks;
using Kelvin.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

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
        AudioClip getMusic;
        switch (audioData.playMode)
        {
            case PlayMusicMode.RandomSingle:
                getMusic = audioData.audioClips[Random.Range(0, audioData.audioClips.Count)];
                _currentMusicPlaying = getMusic;
                var spawnEmittor =
                    PoolingObject.Instance.Spawn(_poolKey, soundEmitter.gameObject, audioContainer, null);
                var musicEmiitor = spawnEmittor.GetComponent<SoundEmitter>();
                musicEmiitor.PlayMusic(_currentMusicPlaying, audioData.audioVolume);
                break;
        }
    }

    public void PlaySoundFx(SoundFXData audioData, bool loop, float volume, Action actionWhenEndClip)
    {
    }
}