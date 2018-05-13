using UnityEngine;
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
                UpdatePosition();
            }
        }

        public Vector2 Position
        {
            set { _RectTransform.position = value; }
        }

        private void Awake()
        {
            _Text = GetComponentInChildren<Text>();
            _RectTransform = GetComponent<RectTransform>();

            _OffsetY = 200;
            _OffsetX = 0f;
        }

        private void Update()
        {
            UpdatePosition();
        }

        public void Close()
        {
            Destroy(gameObject);
        }


        private void UpdatePosition()
        {
            var relativePosition = new Vector3(_OffsetX, _OffsetY, 0);

            Position = Camera.main.WorldToScreenPoint(_Owner.position) + relativePosition;
        }
    }
}
