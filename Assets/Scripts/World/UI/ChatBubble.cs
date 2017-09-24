using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vandraren.UI
{
    public class ChatBubble : MonoBehaviour
    {
        private Text _Text;
        
        public string Text
        {
            set { _Text.text = value; }
        }

        private void Awake()
        {
            _Text = GetComponentInChildren<Text>();
        }

        public void Close()
        {
            Destroy(gameObject);
        }
    }
}
