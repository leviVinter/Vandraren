using System.Collections.Generic;
using UnityEngine;
using Vandraren.Helpers;

namespace Vandraren.Sound
{
    public enum SoundType
    {
        Music,
        Sfx
    }

    public class SoundManager
    {
        private static MusicPlayer _MusicPlayer;
        private static SfxPlayer _SfxPlayer;
        private static MasterMixer _Mixer;

        private static Dictionary<string, AudioClip> _MusicClips = new Dictionary<string, AudioClip>();
        private static Dictionary<string, AudioClip> _SfxClips = new Dictionary<string, AudioClip>();

        public static MusicPlayer MusicPlayer
        {
            set { _MusicPlayer = value; }
        }

        public static SfxPlayer SfxPlayer
        {
            set { _SfxPlayer = value; }
        }

        public static MasterMixer Mixer
        {
            set { _Mixer = value; }
        }

        /// <summary>
        /// Play an sfx clip and set the mixer group.
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pMixerGroup"></param>
        public static void PlaySfx(string pName, string pMixerGroup = "")
        {
            var clip = GetAudioClip(pName, SoundType.Sfx);

            var source = _SfxPlayer.Play(clip);

            if (!string.IsNullOrEmpty(pMixerGroup))
            {
                SetMixerGroup(pMixerGroup, source);
            }
        }

        /// <summary>
        /// Play a music clip and set the mixer group.
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pMixerGroup"></param>
        public static void PlayMusic(string pName, string pMixerGroup = "")
        {
            var clip = GetAudioClip(pName, SoundType.Music);

            var sources = _MusicPlayer.PlayMusic(clip);

            if (!string.IsNullOrEmpty(pMixerGroup))
            {
                foreach (var source in sources)
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
        private static AudioClip GetAudioClip(string pName, SoundType pType)
        {
            AudioClip clip;
            Dictionary<string, AudioClip> clips;

            switch (pType)
            {
                case SoundType.Music:
                    clips = _MusicClips;
                    break;

                case SoundType.Sfx:
                    clips = _SfxClips;
                    break;

                default:
                    clips = _SfxClips;
                    break;
            }

            if (!clips.ContainsKey(pName))
            {
                var folderPath = ResourcePaths.GetSoundFolder(pType);
                var fullPath = $"{folderPath}{pName}";

                clip = Resources.Load(fullPath) as AudioClip;

                if (clip == null)
                {
                    Debug.Log("Couldn't find audiofile: " + fullPath);
                }

                clips.Add(pName, clip);
            }

            return clips[pName];
        }

        /// <summary>
        /// Set the audiomixer group of an audiosource.
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pSource"></param>
        private static void SetMixerGroup(string pName, AudioSource pSource)
        {
            var group = _Mixer.GetMixerGroup(pName);
            pSource.outputAudioMixerGroup = group;
        }
    }
}