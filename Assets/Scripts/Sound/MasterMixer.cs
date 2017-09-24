using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Vandraren.Sound
{
    public class MasterMixer : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer _Mixer;

        private void Awake()
        {
            SoundManager.Mixer = this;
        }

        public AudioMixerGroup GetMixerGroup(string pGroupName)
        {
            AudioMixerGroup[] groups = _Mixer.FindMatchingGroups(pGroupName);

            if (groups.Length == 0)
            {
                return null;
            }

            return groups[0];
        }
    }
}