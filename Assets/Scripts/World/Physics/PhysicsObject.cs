using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

    public float m_GravityModifier = 3f;
    public float m_MinGroundNormalY = 0.65f;

    protected Rigidbody2D m_Rb2d;
    protected Vector2 m_Velocity;
    protected ContactFilter2D m_ContactFilter;
    protected RaycastHit2D[] m_HitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> m_HitBufferList = new List<RaycastHit2D>(16);
    protected Vector2 m_TargetVelocity;
    protected bool m_Grounded;
    protected Vector2 m_GroundNormal;

    protected const float m_MinMoveDistance = 0.001f;
    protected const float m_ShellRadius = 0.01f;

    private void OnEnable()
    {
        m_Rb2d = GetComponent<Rigidbody2D>();
    }

	private void Start()
    {
        m_ContactFilter.useTriggers = false;
        m_ContactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        m_ContactFilter.useLayerMask = true;
	}

	private void Update()
    {
        m_TargetVelocity = Vector2.zero;
        ComputeVelocity();
	}

    protected virtual void ComputeVelocity()
    {

    }

    private void FixedUpdate()
    {
        m_Velocity += m_GravityModifier * Physics2D.gravity * Time.deltaTime;
        m_Grounded = false;
        Vector2 deltaPosition = m_Velocity * Time.deltaTime;
        Vector2 movement = Vector2.up * deltaPosition.y;

        Move(movement, true);
    }

    private void Move(Vector2 pMovement, bool pYMovement)
    {
        float distance = pMovement.magnitude;
        if (distance > m_MinMoveDistance)
        {
            int count = m_Rb2d.Cast(pMovement, m_ContactFilter, m_HitBuffer, distance + m_ShellRadius);
            m_HitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                m_HitBufferList.Add(m_HitBuffer[i]);
            }
            for (int i = 0; i < m_HitBufferList.Count; i++)
            {
                Vector2 tempCurrentNormal = m_HitBufferList[i].normal;
                if (tempCurrentNormal.y > m_MinGroundNormalY)
                {
                    m_Grounded = true;
                    if (pYMovement)
                    {
                        //m_GroundNormal = tempCurrentNormal;
                        tempCurrentNormal.x = 0;
                    }
                    
                }
                float tempProjection = Vector2.Dot(m_Velocity, tempCurrentNormal);
                if (tempProjection < 0)
                {
                    m_Velocity = m_Velocity - tempProjection * tempCurrentNormal;
                }
                float tempModifiedDistance = m_HitBufferList[i].distance - m_ShellRadius;
                distance = tempModifiedDistance < distance ? tempModifiedDistance : distance;
            }
        }

        m_Rb2d.position = m_Rb2d.position + pMovement.normalized * distance;
    }
}
