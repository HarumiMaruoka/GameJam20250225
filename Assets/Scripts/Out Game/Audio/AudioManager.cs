using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Confront.Audio
{
    public static class AudioManager
    {
        private static BGMPlayer _bgmPlayer = new BGMPlayer();
        private static SEPlayer _sePlayer = new SEPlayer();

        public static VolumeParameters VolumeParameters { get; } = new VolumeParameters();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            VolumeParameters.OnBgmVolumeChanged += _bgmPlayer.ChangeVolume;
            _sePlayer.UpdateAsync();
        }

        public static void PlayBGM(AudioClip clip, float duration = 1f)
        {
            _bgmPlayer.Play(clip, duration);
        }

        public static void StopBGM()
        {
            _bgmPlayer.Stop(1).Forget();
        }

        private static Camera _camera;

        public static void PlaySE(AudioClip clip)
        {
            if (!_camera) _camera = Camera.main;
            _sePlayer.Play(clip, _camera.transform.position);
        }

        public static void PlaySE(AudioClip clip, Vector3 position)
        {
            _sePlayer.Play(clip);
        }
    }
}