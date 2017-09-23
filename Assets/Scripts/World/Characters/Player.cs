using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vandraren.World.Physics;

namespace Vandraren.World.Characters
{
    public class Player : PhysicsObject
    {
        public float m_MaxSpeed = 7f;

        private SpriteRenderer m_SpriteRenderer;
        private Animator m_Animator;

        private void Awake()
        {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void ComputeVelocity()
        {
            Vector2 move = Vector2.zero;
            move.x = Input.GetAxis("Horizontal");

            bool flipSprite = (m_SpriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
            if (flipSprite)
            {
                m_SpriteRenderer.flipX = !m_SpriteRenderer.flipX;
            }

            m_TargetVelocity = move * m_MaxSpeed;
        }
    }
}