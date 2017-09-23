using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vandraren.Inputs;
using Vandraren.Instruments;
using Vandraren.World.Physics;

namespace Vandraren.World.Characters
{
    public class Player : MonoBehaviour
    {
        private SpriteRenderer _SpriteRenderer;
        private Animator _Animator;
        private InputChecker _InputChecker;
        private PhysicsController _Physics;
	    private Instrument _Instrument;

        private bool _IsActive { get; set; }

        private void Awake()
        {
            _SpriteRenderer = GetComponent<SpriteRenderer>();
            _Animator = GetComponent<Animator>();
            
	        SetupPhysics();
            SetupInstrument();
            SetupInputChecker();

            SetActive(true);
        }

        private void Update()
        {
            if (_IsActive)
            {
                _InputChecker.Check();
            }
        }

        private void FixedUpdate()
        {
            _Physics.FixedUpdate();
        }

        private void SetupInputChecker()
        {
            _InputChecker = new InputChecker();
            _InputChecker.AddAxisCheck(AxisName.Horizontal, OnMovement);
	        _InputChecker.AddButtonCheck(ButtonName.PlayInstrument, PlayInstrument);
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

        private void SetupPhysics()
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            LayerMask layer = gameObject.layer;
            _Physics = new PhysicsController(rigidbody, layer);
        }

        private void OnMovement(float pAxisValue)
        {
            _Physics.ComputeVelocity(pAxisValue);
            if (pAxisValue < -0.01f || pAxisValue > 0.01f)
            {
                if (!_Animator.GetBool("Walking"))
                {
                    _Animator.SetBool("Walking", true);
                }
            }
            else
            {
                if (_Animator.GetBool("Walking"))
                {
                    _Animator.SetBool("Walking", false);
                }
            }
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
