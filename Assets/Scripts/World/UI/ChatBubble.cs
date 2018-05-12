﻿using UnityEngine;
using UnityEngine.UI;

namespace Vandraren.UI
{
    public class ChatBubble : MonoBehaviour
    {
        private Text _Text;
        private Transform _Owner;
        private RectTransform _RectTransform;
        private float _OffsetY;
        private float _OffsetX;
        
        public string Text
        {
            set { _Text.text = value; }
        }

        public Transform Owner
        {
            set
            {
                _Owner = value;
                _OffsetY = 100;
                _OffsetX = 0f;
                UpdatePosition();
            }
        }

        private void Awake()
        {
            _Text = GetComponentInChildren<Text>();
            _RectTransform = GetComponent<RectTransform>();
        }

        public void Close()
        {
            Destroy(gameObject);
        }

        public Vector2 Position
        {
            set
            {
                _RectTransform.position = value;
            }
        }

        private void Update()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            var relativePosition = new Vector3(_OffsetX, _OffsetY, 0);

            Position = Camera.main.WorldToScreenPoint(_Owner.position) + relativePosition;
        }
    }
}
