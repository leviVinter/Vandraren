using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Vandraren.Helpers;

namespace Vandraren.Sound
{
    public enum SoundType
    {
        MUSIC,
        SFX
    }
    public class SoundManager
    {
        private MusicPlayer _MusicPlayer;
        private MasterMixer _Mixer;

        private Dictionary<string, AudioClip> _MusicClips;
        private Dictionary<string, AudioClip> _SfxClips;

        public SoundManager()
        {
            _MusicPlayer = GameObject.FindObjectOfType<MusicPlayer>();
            _Mixer = GameObject.FindObjectOfType<MasterMixer>();

            _MusicClips = new Dictionary<string, AudioClip>();
            _SfxClips = new Dictionary<string, AudioClip>();

        }

        /// <summary>
        /// Play a music clip and set the mixer group.
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pMixerGroup"></param>
        public void PlayMusic(string pName, string pMixerGroup = "")
        {
            AudioClip clip = GetAudioClip(pName, SoundType.MUSIC);

            AudioSource[] sources =_MusicPlayer.PlayMusic(clip);

            if (!string.IsNullOrEmpty(pMixerGroup))
            {
                foreach (AudioSource source in sources)
                {
                    SetMixerGroup(pMixerGroup, source);
                }
            }
        }

        /// <summary>
        /// Get an audioclip.
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pType"></param>
        /// <returns></returns>
        private AudioClip GetAudioClip(string pName, SoundType pType)
        {
            AudioClip clip;
            Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();

            switch (pType)
            {
                case SoundType.MUSIC:
                    clips = _MusicClips;
                    break;

                case SoundType.SFX:
                    clips = _SfxClips;
                    break;

                default:
                    clips = _SfxClips;
                    break;
            }

            if (!clips.ContainsKey(pName))
            {
                string folderPath = ResourcesPaths.SoundFolder(pType);
                string fullPath = string.Format("{0}{1}", folderPath, pName);

                clip = Resources.Load(fullPath) as AudioClip;
                clips.Add(pName, clip);
            }

            return clips[pName];
        }

        /// <summary>
        /// Set the audiomixer group of an audiosource.
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pSource"></param>
        private void SetMixerGroup(string pName, AudioSource pSource)
        {
            AudioMixerGroup group = _Mixer.GetMixerGroup(pName);
            pSource.outputAudioMixerGroup = group;
        }
    }
}