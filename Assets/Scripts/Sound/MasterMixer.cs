using System.Linq;
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
            var groups = _Mixer.FindMatchingGroups(pGroupName);
 
            if (!groups.Any())
            {
                return null;
            }

            return groups[0];
        }
    }
}