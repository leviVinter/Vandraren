using UnityEngine;
using Vandraren.Inputs;
using Vandraren.Instruments;
using Vandraren.UI;
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

            _IsActive = true;
        }

        private void Start()
        {
            //Invoke("OpenChatBubble", 2f);
        }

        private void OpenChatBubble()
        {
            MessageManager.OpenChatBubble("This is the first chat bubble", transform);
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
                _Instrument.Animator = _Animator;
            }
        }

        private void SetupPhysics()
        {
            var rigidbody = GetComponent<Rigidbody2D>();
            var layer = gameObject.layer;
            _Physics = new PhysicsController(rigidbody, layer, 3.0f);
        }

        private void OnMovement(float pAxisValue)
        {
            var flipSprite = (_SpriteRenderer.flipX ? (pAxisValue > 0.01f) : (pAxisValue < -0.01f));

            if (flipSprite)
            {
                _SpriteRenderer.flipX = !_SpriteRenderer.flipX;
            }

            _Physics.ComputeVelocity(pAxisValue);

            var isMoving = pAxisValue < -0.01f || pAxisValue > 0.01f;

            var animatorIsWalking = _Animator.GetBool("Walking");

            if (isMoving)
            {
                if (!animatorIsWalking)
                {
                    _Animator.SetBool("Walking", true);
                }
            }
            else if (animatorIsWalking)
            {
                _Animator.SetBool("Walking", false);
            }
        }

         private void PlayInstrument()
        {
            _Animator.SetTrigger("TakeInstrument");
            _Instrument.IsActive = true;
            _IsActive = false;
        }

        public void Activate()
        {
            _IsActive = true;
        }
    }
}
