using UnityEngine;
using Vandraren.World.Physics;

namespace Vandraren.World.Characters
{
    public class Tomte : MonoBehaviour
    {
        private SpriteRenderer _SpriteRenderer;
        private Animator _Animator;
        private PhysicsController _Physics;

        private bool _IsActive { get; set; }

        private void Awake()
        {
            _SpriteRenderer = GetComponent<SpriteRenderer>();
            _Animator = GetComponent<Animator>();

            SetupPhysics();

            Activate();
        }

        private void SetupPhysics()
        {
            var rigidbody = GetComponent<Rigidbody2D>();
            var layer = gameObject.layer;
            _Physics = new PhysicsController(rigidbody, layer, 2.0f);
        }

        private void StopMoving()
        {
            _Animator.SetBool("Idle", true);
        }

        public void Activate()
        {
            _IsActive = true;
        }
    }
}
