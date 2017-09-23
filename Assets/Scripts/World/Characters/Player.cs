using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vandraren.Inputs;
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

        private void Awake()
        {
            _SpriteRenderer = GetComponent<SpriteRenderer>();
            _InputChecker = new InputChecker();
            SetupInputChecker();
            _Animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _InputChecker.Check();
        }

        private void SetupInputChecker()
        {
            _InputChecker.AddInputCheck(ButtonName.Right, MovePlayer);
            _InputChecker.AddInputCheck(ButtonName.Right, StopPlayer, ButtonPressType.Up);
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
    }
}