using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vandraren.World.Physics
{
    public class PhysicsController
    {
        public float _GravityModifier = 3f;
        public float _MinGroundNormalY = 0.65f;

        private Rigidbody2D _Rigidbody;
        private Vector2 _Velocity = Vector2.zero;
        private ContactFilter2D _ContactFilter;
        private RaycastHit2D[] _HitBuffer = new RaycastHit2D[16];
        private List<RaycastHit2D> _HitBufferList = new List<RaycastHit2D>(16);
        private Vector2 _TargetVelocity;
        private bool _Grounded = false;
        private Vector2 _GroundNormal = Vector2.zero;
        private float _MaxSpeed = 7.0f;

        private const float _MinMoveDistance = 0.001f;
        private const float _ShellRadius = 0.01f;

        public PhysicsController(Rigidbody2D pRigidbody, LayerMask pLayer)
        {
            _Rigidbody = pRigidbody;
            _ContactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(pLayer));
            _ContactFilter.useLayerMask = true;
        }

        public void ComputeVelocity(float pAxisValue)
        {
            Vector2 move = Vector2.zero;
            move.x = pAxisValue;

            //bool flipSprite = (_SpriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
            //if (flipSprite)
            //{
            //    _SpriteRenderer.flipX = !_SpriteRenderer.flipX;
            //}

            _TargetVelocity = move * _MaxSpeed;
        }

        public void FixedUpdate()
        {
            _Velocity += _GravityModifier * Physics2D.gravity * Time.deltaTime;
            _Velocity.x = _TargetVelocity.x;

            _Grounded = false;
            Vector2 deltaPosition = _Velocity * Time.deltaTime;
            Vector2 moveAlongGround = new Vector2(_GroundNormal.y, -_GroundNormal.x);
            Vector2 movement = moveAlongGround * deltaPosition.x;
            Move(movement, false);

            movement = Vector2.up * deltaPosition.y;
            Move(movement, true);
            _TargetVelocity = Vector2.zero;
        }

        private void Move(Vector2 pMovement, bool pYMovement)
        {
            float distance = pMovement.magnitude;
            if (distance > _MinMoveDistance)
            {
                int count = _Rigidbody.Cast(pMovement, _ContactFilter, _HitBuffer, distance + _ShellRadius);
                _HitBufferList.Clear();
                for (int i = 0; i < count; i++)
                {
                    _HitBufferList.Add(_HitBuffer[i]);
                }
                for (int i = 0; i < _HitBufferList.Count; i++)
                {
                    Vector2 tempCurrentNormal = _HitBufferList[i].normal;
                    if (tempCurrentNormal.y > _MinGroundNormalY)
                    {
                        _Grounded = true;
                        if (pYMovement)
                        {
                            _GroundNormal = tempCurrentNormal;
                            tempCurrentNormal.x = 0;
                        }

                    }
                    float tempProjection = Vector2.Dot(_Velocity, tempCurrentNormal);
                    if (tempProjection < 0)
                    {
                        _Velocity = _Velocity - tempProjection * tempCurrentNormal;
                    }
                    float tempModifiedDistance = _HitBufferList[i].distance - _ShellRadius;
                    distance = tempModifiedDistance < distance ? tempModifiedDistance : distance;
                }
            }

            _Rigidbody.position = _Rigidbody.position + pMovement.normalized * distance;
        }
    }
}