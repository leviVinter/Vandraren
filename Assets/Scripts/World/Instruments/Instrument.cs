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
        public List<string> Tones = new List<string>();
        public string MixerGroup;

        private InputChecker _InputChecker { get; set; }
        private Animator _Animator { get; set; }
        private bool _IsActive { get; set; }
        private Action _StopCallback { get; set; }

        private void Awake()
        {
            SetupInputChecker();

            _IsActive = false;
        }

        public void SetAnimator(Animator pAnimator)
        {
            _Animator = pAnimator;
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

            for (int i = 0; i < Tones.Count; i++)
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
            PlayTone(Tones[0]);
        }

        private void PlaySecond()
        {
            PlayTone(Tones[1]);
        }

        private void PlayThird()
        {
            PlayTone(Tones[2]);
        }

        private void PlayFourth()
        {
            PlayTone(Tones[3]);
        }

        private void PlayFifth()
        {
            PlayTone(Tones[4]);
        }

        private void StopPlaying()
        {
            _Animator.SetTrigger("RemoveInstrument");
            SetActive(false);
        }

        public void SetActive(bool pActive = true)
        {
            _IsActive = pActive;
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