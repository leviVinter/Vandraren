using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Vandraren.Sound
{
    public class MasterMixer : MonoBehaviour
    {
        public AudioMixer _Mixer;

        private void Awake()
        {
            SoundManager.SetMixer(this);
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