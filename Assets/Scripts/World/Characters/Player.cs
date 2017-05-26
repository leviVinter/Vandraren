using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.World.Characters
{
    public class Player : MonoBehaviour
    {
        public float m_Speed = 10;
        private Rigidbody2D rb2d;
        private SpriteRenderer m_Sprite;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            m_Sprite = GetComponent<SpriteRenderer>();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            if (horizontal != 0)
            {
                Move();
            }
        }

        public void Move()
        {
            Debug.Log("Moving");
            rb2d.velocity = new Vector2(m_Speed, 0);
        }
    }
}