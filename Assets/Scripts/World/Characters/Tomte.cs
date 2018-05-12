using UnityEngine;
using Vandraren.World.Physics;

namespace Vandraren.World.Characters
{
    public class Tomte : MonoBehaviour
    {
        private SpriteRenderer _SpriteRenderer;
        private Animator _Animator;
        private PhysicsController _Physics;
        private float _StartPositionX;
        private float _MoveDistance;
        private bool _IsMoving;
        private int _Direction;

        private bool _IsActive { get; set; }

        private void Awake()
        {
            _SpriteRenderer = GetComponent<SpriteRenderer>();
            _Animator = GetComponent<Animator>();

            SetupPhysics();

            Activate();

            _IsMoving = false;
            StartMovingBackAndForth(5f);
            Invoke("StopMoving", 10f);
        }

        private void Start()
        {
            var collider = GetComponent<Collider2D>();
            var player = GameObject.FindGameObjectWithTag("Player");
            var playerCollider = player.GetComponent<Collider2D>();

            Physics2D.IgnoreCollision(collider, playerCollider);
        }

        private void Update()
        {
            if (_IsMoving)
            {
                UpdateMovement();
            }
        }

        private void FixedUpdate()
        {
            _Physics.FixedUpdate();
        }

        private void SetupPhysics()
        {
            var rigidbody = GetComponent<Rigidbody2D>();
            var layer = gameObject.layer;
            var maxSpeed = 2.0f;
            _Physics = new PhysicsController(rigidbody, layer, maxSpeed);
        }

        private void StopMoving()
        {
            _Animator.SetBool("Walking", false);
            _Physics.ComputeVelocity(0f);
            _IsMoving = false;
        }

        private void StartMovingBackAndForth(float pMoveDistance)
        {
            _IsMoving = true;
            _MoveDistance = pMoveDistance;
            _StartPositionX = transform.position.x;

            _Animator.SetBool("Walking", true);
            _Direction = 1;
            _Physics.ComputeVelocity(1f);
        }

        private void UpdateMovement()
        {
            var shouldTurnRight = transform.position.x < (_StartPositionX - _MoveDistance) && _Direction != 1;
            var shouldTurnLeft = transform.position.x > (_StartPositionX + _MoveDistance) && _Direction != -1;

            if (shouldTurnLeft || shouldTurnRight)
            {
                _Direction *= -1;
                _SpriteRenderer.flipX = !_SpriteRenderer.flipX;
            }

            _Physics.ComputeVelocity(_Direction);
        }

        public void Activate()
        {
            _IsActive = true;
        }
    }
}
