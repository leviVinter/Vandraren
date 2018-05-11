using System;
using System.Collections.Generic;
using UnityEngine;
using Vandraren.Inputs;
using Vandraren.Sound;

namespace Vandraren.Instruments
{
    public class Instrument : MonoBehaviour
    {
        [SerializeField]
        private List<string> _Tones = new List<string>();
        [SerializeField]
        private string _MixerGroup;

        private InputChecker _InputChecker;
        private Animator _Animator;
        private bool _IsActive;

        public Animator Animator
        {
            set { _Animator = value; }
        }

        public bool IsActive
        {
            set { _IsActive = value; }
        }

        private void Awake()
        {
            SetupInputChecker();

            _IsActive = false;
        }

        private void SetupInputChecker()
        {
            _InputChecker = new InputChecker();
            var playActions = new Action[]
            {
                () => PlayTone(0),
                () => PlayTone(1),
                () => PlayTone(2),
                () => PlayTone(3),
                () => PlayTone(4)
            };

            var buttons = new ButtonName[]
            {
                ButtonName.One,
                ButtonName.Two,
                ButtonName.Three,
                ButtonName.Four,
                ButtonName.Five
            };

            for (var i = 0; i < _Tones.Count; i++)
            {
                _InputChecker.AddButtonCheck(buttons[i], playActions[i]);
            }

            _InputChecker.AddButtonCheck(ButtonName.RemoveInstrument, StopPlaying);
        }

        private void PlayTone(int pToneIndex)
        {
            if (_Tones.Count < pToneIndex)
            {
                Debug.Log($"Tone index {pToneIndex} doesn't exist.");
                return;
            }

            SoundManager.PlaySfx(_Tones[pToneIndex], "Sfx");
        }

        private void StopPlaying()
        {
            _Animator.SetTrigger("RemoveInstrument");
            _IsActive = false;
        }

        private void Update()
        {
            if (_IsActive)
            {
                _InputChecker.Check();
            }
        }
    }
}