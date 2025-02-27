using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Confront.Audio
{
    public class SEPlayer
    {
        private GameObject _sePlayerObject;
        private AudioSource _seAudioSource;

        private const int MaxAudioSourceCount = 5;

        private HashSet<AudioSource> _activeAudioSources = new HashSet<AudioSource>();
        private Stack<AudioSource> _availableAudioSources = new Stack<AudioSource>();

        public SEPlayer()
        {
            _sePlayerObject = new GameObject("SEPlayer");
            GameObject.DontDestroyOnLoad(_sePlayerObject);
            _seAudioSource = Resources.Load<AudioSource>("Audio/SEAudioSource");
        }

        private Camera _camera;

        public void Play(AudioClip clip)
        {
            if (_activeAudioSources.Count >= MaxAudioSourceCount) return;

            if (!_camera) _camera = Camera.main;
            var volume = AudioManager.VolumeParameters.SeVolume;

            AudioSource audioSource = GetAvailableAudioSource();
            audioSource.transform.position = _camera.transform.position;
            audioSource.volume = volume;
            audioSource.PlayOneShot(clip);
        }

        public void Play(AudioClip clip, Vector3 position)
        {
            if (_activeAudioSources.Count >= MaxAudioSourceCount) return;

            var volume = AudioManager.VolumeParameters.SeVolume;

            AudioSource audioSource = GetAvailableAudioSource();
            audioSource.transform.position = position;
            audioSource.volume = volume;
            audioSource.PlayOneShot(clip);
        }

        private AudioSource GetAvailableAudioSource()
        {
            if (_availableAudioSources.Count > 0)
            {
                return _availableAudioSources.Pop();
            }
            AudioSource audioSource = GameObject.Instantiate(_seAudioSource, _sePlayerObject.transform);
            audioSource.transform.SetParent(_sePlayerObject.transform);
            _activeAudioSources.Add(audioSource);
            return audioSource;
        }

        private void ReturnAudioSource(AudioSource audioSource)
        {
            audioSource.Stop();
            audioSource.clip = null;
            _availableAudioSources.Push(audioSource);
            _removeAudioSources.Add(audioSource);
        }

        private HashSet<AudioSource> _removeAudioSources = new HashSet<AudioSource>();

        public async void UpdateAsync()
        {
            while (Application.isPlaying)
            {
                foreach (AudioSource audioSource in _activeAudioSources)
                {
                    if (!audioSource.isPlaying)
                    {
                        ReturnAudioSource(audioSource);
                    }
                }

                foreach (AudioSource audioSource in _removeAudioSources)
                {
                    _activeAudioSources.Remove(audioSource);
                }
                _removeAudioSources.Clear();

                await UniTask.Yield();
            }
        }
    }
}
