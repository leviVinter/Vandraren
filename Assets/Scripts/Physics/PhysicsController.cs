﻿using System.Collections.Generic;
using UnityEngine;

namespace Vandraren.World.Physics
{
    public class PhysicsController
    {
        private float _GravityModifier;
        private float _MinGroundNormalY;

        private Vector2 _Velocity;
        private ContactFilter2D _ContactFilter;
        private Rigidbody2D _Rigidbody;
        private RaycastHit2D[] _HitBuffer;
        private List<RaycastHit2D> _HitBufferList;
        private Vector2 _TargetVelocity;
        //private bool _Grounded = false;
        private Vector2 _GroundNormal;
        private float _MaxSpeed;

        private const float _MinMoveDistance = 0.001f;
        private const float _ShellRadius = 0.01f;

        public PhysicsController(Rigidbody2D pRigidbody, LayerMask pLayer, float pMaxSpeed)
        {
            _Rigidbody = pRigidbody;
            _ContactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(pLayer));
            _ContactFilter.useLayerMask = true;

            _GravityModifier = 3.0f;
            _MinGroundNormalY = 0.65f;
            _HitBuffer = new RaycastHit2D[16];
            _HitBufferList = new List<RaycastHit2D>(16);
            _Velocity = Vector2.zero;
            _GroundNormal = Vector2.zero;
            _MaxSpeed = pMaxSpeed;
        }

        public void ComputeVelocity(float pAxisValue)
        {
            var move = Vector2.zero;
            move.x = pAxisValue;

            _TargetVelocity = move * _MaxSpeed;
        }

        public void FixedUpdate()
        {
            _Velocity += _GravityModifier * Physics2D.gravity * Time.deltaTime;
            _Velocity.x = _TargetVelocity.x;

            //_Grounded = false;
            var deltaPosition = _Velocity * Time.deltaTime;
            var moveAlongGround = new Vector2(_GroundNormal.y, -_GroundNormal.x);
            var movement = moveAlongGround * deltaPosition.x;
            Move(movement, false);

            movement = Vector2.up * deltaPosition.y;
            Move(movement, true);
            _TargetVelocity = Vector2.zero;
        }

        private void Move(Vector2 pMovement, bool pYMovement)
        {
            var distance = pMovement.magnitude;
            if (distance > _MinMoveDistance)
            {
                var count = _Rigidbody.Cast(pMovement, _ContactFilter, _HitBuffer, distance + _ShellRadius);
                _HitBufferList.Clear();
                for (var i = 0; i < count; i++)
                {
                    _HitBufferList.Add(_HitBuffer[i]);
                }
                for (var i = 0; i < _HitBufferList.Count; i++)
                {
                    var tempCurrentNormal = _HitBufferList[i].normal;
                    if (tempCurrentNormal.y > _MinGroundNormalY)
                    {
                        //_Grounded = true;
                        if (pYMovement)
                        {
                            _GroundNormal = tempCurrentNormal;
                            tempCurrentNormal.x = 0;
                        }

                    }
                    var tempProjection = Vector2.Dot(_Velocity, tempCurrentNormal);
                    if (tempProjection < 0)
                    {
                        _Velocity = _Velocity - tempProjection * tempCurrentNormal;
                    }
                    var tempModifiedDistance = _HitBufferList[i].distance - _ShellRadius;
                    distance = tempModifiedDistance < distance ? tempModifiedDistance : distance;
                }
            }

            _Rigidbody.position = _Rigidbody.position + pMovement.normalized * distance;
        }
    }
}