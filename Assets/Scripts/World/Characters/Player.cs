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
        private PhysicsController _Physics;

        private void Awake()
        {
            _SpriteRenderer = GetComponent<SpriteRenderer>();
            _InputChecker = new InputChecker();
            SetupInputChecker();
            _Animator = GetComponent<Animator>();
            SetupPhysics();
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

        private void SetupPhysics()
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            LayerMask layer = gameObject.layer;
            _Physics = new PhysicsController(rigidbody, layer);
        }

        private void MovePlayer()
        {
            _Animator.SetBool("Walking", true);
        }

        private void StopPlayer()
        {
            _Animator.SetBool("Walking", false);
        }
    }
}