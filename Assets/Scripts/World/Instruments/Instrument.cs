using System;
using System.Collections;
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
            Action[] playActions = new Action[]
            {
                PlayFirst,
                PlaySecond,
                PlayThird,
                PlayFourth,
                PlayFifth
            };

            ButtonName[] buttons = new ButtonName[]
            {
                ButtonName.One,
                ButtonName.Two,
                ButtonName.Three,
                ButtonName.Four,
                ButtonName.Five
            };

            for (int i = 0; i < _Tones.Count; i++)
            {
                _InputChecker.AddButtonCheck(buttons[i], playActions[i]);
            }

            _InputChecker.AddButtonCheck(ButtonName.RemoveInstrument, StopPlaying);
        }

        private void PlayTone(string pName)
        {
            SoundManager.PlaySfx(pName, "Sfx");
        }

        private void PlayFirst()
        {
            PlayTone(_Tones[0]);
        }

        private void PlaySecond()
        {
            PlayTone(_Tones[1]);
        }

        private void PlayThird()
        {
            PlayTone(_Tones[2]);
        }

        private void PlayFourth()
        {
            PlayTone(_Tones[3]);
        }

        private void PlayFifth()
        {
            PlayTone(_Tones[4]);
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