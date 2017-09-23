using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vandraren.Sound
{
    public class SfxPlayer : MonoBehaviour
    {
        public AudioSource _SourcePrefab;

        private void Awake()
        {
            SoundManager.SetSfxPlayer(this);
        }

        public AudioSource Play(AudioClip pClip)
        {
            AudioSource source = Instantiate(_SourcePrefab, transform);
            source.clip = pClip;
            source.Play();

            //float duration = pClip.length + 0.1f;
            float duration = 1f;
            Destroy(source.gameObject, duration);

            return source;
        }
    }
}