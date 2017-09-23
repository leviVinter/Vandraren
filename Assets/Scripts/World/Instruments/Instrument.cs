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
        public List<string> _Tones = new List<string>();
        public string _MixerGroup;

        private InputChecker _InputChecker { get; set; }
        private Animator _Animator { get; set; }
        private bool _IsActive { get; set; }
        private GameObject _Owner { get; set; }
        private Action _StopCallback { get; set; }

        private void Awake()
        {
            SetupInputChecker();
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

        public void SetStopCallback(Action pAction)
        {
            _StopCallback = pAction;
        }

        private void StopPlaying()
        {
            _Animator.SetTrigger("RemoveInstrument");
            SetActive(false);

            if (_StopCallback != null)
            {
                _StopCallback();
            }
        }

        public void SetOwner(GameObject pOwner)
        {
            _Owner = pOwner;
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