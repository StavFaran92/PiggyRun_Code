using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services
{
    class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        [SerializeField] private AudioClip[] audioClips;
        [SerializeField] private AudioSource mMusicAudioSource;
        [SerializeField] private AudioSource mSoundAudioSource;

        private Dictionary<string, AudioClip> mAudioDictionary = new Dictionary<string, AudioClip>();

        private void Awake()
        {
            instance = this;

            //--Load sounds into dictionary
            foreach (AudioClip audioClip in audioClips)
            {
                mAudioDictionary.Add(audioClip.name, audioClip);
            }
            GameEventManager.Instance.CallEvent(ApplicationConstants.AUDIO_MANAGER_IS_INITIALIZES, ActionParams.EmptyParams);
        }

        public void PlaySoundOverlap(string audioClipName)
        {
            bool shouldPlaySound = PlayerPrefs.GetInt("Sound", 1) == 1 ? true : false;
            if (shouldPlaySound)
            {
                mAudioDictionary.TryGetValue(audioClipName, out AudioClip audioClip);
                if (audioClip)
                {
                    mSoundAudioSource.PlayOneShot(audioClip);
                }
            }
        }

        public void PlayMusicInLoop(string audioClipName)
        {
            mAudioDictionary.TryGetValue(audioClipName, out AudioClip audioClip);
            if (audioClip)
            {
                //--Start playing BG music
                mMusicAudioSource.loop = true;
                mMusicAudioSource.clip = audioClip;
                mMusicAudioSource.Play();

                //--load music saving data and play decide if should mute
                bool shouldPlayMusic = PlayerPrefs.GetInt("Music", 1) == 1 ? true : false;
                mMusicAudioSource.mute = !shouldPlayMusic;
            }

        }

        internal void SetSound(bool v)
        {
            PlayerPrefs.SetInt("Sound", v == true ? 1 : 0);
        }

        internal void SetMusic(bool v)
        {
            PlayerPrefs.SetInt("Music", v == true ? 1 : 0);

            mMusicAudioSource.mute = !v;
        }
    }
}
