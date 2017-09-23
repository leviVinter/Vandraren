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
        private SpriteRenderer _SpriteRenderer { get; set; }
        private Animator _Animator { get; set; }
        private InputChecker _InputChecker { get; set; }
        private PhysicsController _Physics { get; set; }
	    private Instrument _Instrument { get; set; }

        private bool _IsActive { get; set; }

        private void Awake()
        {
            _SpriteRenderer = GetComponent<SpriteRenderer>();
            _Animator = GetComponent<Animator>();
            
	        SetupPhysics();
            SetupInstrument();
            SetupInputChecker();

            _IsActive = true;
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

            if (_Animator != null)
            {
                _Instrument.SetAnimator(_Animator);
            }
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
            _IsActive = false;
        }

        public void Activate()
        {
            _IsActive = true;
        }
    }
}
