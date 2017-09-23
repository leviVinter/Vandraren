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
        private static MusicPlayer _MusicPlayer { get; set; }
        private static MasterMixer _Mixer { get; set; }

        private static Dictionary<string, AudioClip> _MusicClips = new Dictionary<string, AudioClip>();
        private static Dictionary<string, AudioClip> _SfxClips = new Dictionary<string, AudioClip>();

        public static void SetMusicPlayer(MusicPlayer pPlayer)
        {
            _MusicPlayer = pPlayer;
        }

        public static void SetMixer(MasterMixer pMixer)
        {
            _Mixer = pMixer;
        }

        /// <summary>
        /// Play a music clip and set the mixer group.
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pMixerGroup"></param>
        public static void PlayMusic(string pName, string pMixerGroup = "")
        {
            AudioClip clip = GetAudioClip(pName, SoundType.MUSIC);

            AudioSource[] sources = _MusicPlayer.PlayMusic(clip);

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
        private static AudioClip GetAudioClip(string pName, SoundType pType)
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
            AudioMixerGroup group = _Mixer.GetMixerGroup(pName);
            pSource.outputAudioMixerGroup = group;
        }
    }
}