using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vandraren.Inputs;
using Vandraren.Instruments;
using Vandraren.Sound;
using Vandraren.World.Physics;

namespace Vandraren.World.Characters
{
    public class Player : MonoBehaviour
    {
        public float _MaxSpeed = 7f;

        private SpriteRenderer _SpriteRenderer;
        private Animator _Animator;
        private InputChecker _InputChecker;
        private Instrument _Instrument;

        private bool _IsActive { get; set; }

        private void Awake()
        {
            _SpriteRenderer = GetComponent<SpriteRenderer>();
            _Animator = GetComponent<Animator>();
            
            SetupInputChecker();
            SetupInstrument();

            SetActive(true);
        }

        private void Update()
        {
            if (_IsActive)
            {
                _InputChecker.Check();
            }
        }

        private void SetupInputChecker()
        {
            _InputChecker = new InputChecker();

            _InputChecker.AddInputCheck(ButtonName.Right, MovePlayer);
            _InputChecker.AddInputCheck(ButtonName.Right, StopPlayer, ButtonPressType.Up);
            _InputChecker.AddInputCheck(ButtonName.PlayInstrument, PlayInstrument);
        }

        private void SetupInstrument()
        {
            _Instrument = GetComponent<Instrument>();
            _Instrument.SetActive(false);

            if (_Animator != null)
            {
                _Instrument.SetAnimator(_Animator);
            }

            _Instrument.SetStopCallback(new Action(() => SetActive(true)));
        }

        //protected override void ComputeVelocity()
        //{
        //    Vector2 move = Vector2.zero;
        //    move.x = Input.GetAxis("Horizontal");

        //    bool flipSprite = (_SpriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
        //    if (flipSprite)
        //    {
        //        _SpriteRenderer.flipX = !_SpriteRenderer.flipX;
        //    }

        //    m_TargetVelocity = move * _MaxSpeed;
        //}

        private void MovePlayer()
        {
            _Animator.SetBool("Walking", true);
            Debug.Log("Walk");
        }

        private void StopPlayer()
        {
            _Animator.SetBool("Walking", false);
            Debug.Log("Stop");
        }

        private void PlayInstrument()
        {
            _Animator.SetTrigger("TakeInstrument");
            _Instrument.SetActive(true);
            SetActive(false);
        }

        public void SetActive(bool pActive)
        {
            _IsActive = pActive;
        }
    }
}