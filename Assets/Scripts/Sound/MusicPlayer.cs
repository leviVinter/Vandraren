using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vandraren.Sound
{
    public class MusicPlayer : MonoBehaviour
    {
        public AudioSource[] Sources;

        /// <summary>
        /// Play an audioclip in a loop.
        /// </summary>
        /// <param name="pClip"></param>
        public AudioSource[] PlayMusic(AudioClip pClip)
        {
            StopMusic();

            AudioSource source = Sources[0];
            source.clip = pClip;
            source.loop = true;
            source.Play();

            return Sources;
        }

        /// <summary>
        /// Stop all music.
        /// </summary>
        private void StopMusic()
        {
            StopAllCoroutines();

            foreach (AudioSource source in Sources)
            {
                source.Stop();
                source.volume = 1f;
            }
        }
    }
}