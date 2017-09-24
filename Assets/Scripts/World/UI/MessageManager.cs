using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vandraren.UI
{
    public class MessageManager
    {
        private static ChatBubble _ChatBubblePrefab;

        public static void OpenChatBubble(string pText, Transform pSpeaker)
        {
            if (_ChatBubblePrefab == null)
            {
                _ChatBubblePrefab = Resources.Load("Prefabs/ChatBubble", typeof(ChatBubble)) as ChatBubble);
            }

            GameObject canvas = GameObject.FindGameObjectWithTag("MainCanvas");

            ChatBubble bubble = GameObject.Instantiate(_ChatBubblePrefab, canvas.transform) as ChatBubble;
            bubble.Text = pText;
            Vector2 position = pSpeaker.position;
            bubble.transform.position = position;
        }
    }
}
