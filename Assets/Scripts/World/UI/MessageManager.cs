using System.Collections.Generic;
using UnityEngine;

namespace Vandraren.UI
{
    public class MessageManager
    {
        private static ChatBubble _ChatBubblePrefab;
        private static List<ChatBubble> ChatBubbles = new List<ChatBubble>();

        public static void OpenChatBubble(string pText, Transform pSpeaker)
        {
            if (_ChatBubblePrefab == null)
            {
                _ChatBubblePrefab = Resources.Load("Prefabs/ChatBubble", typeof(ChatBubble)) as ChatBubble;
            }

            var canvas = GameObject.FindGameObjectWithTag("MainCanvas");

            var bubble = GameObject.Instantiate(_ChatBubblePrefab, canvas.transform) as ChatBubble;
            bubble.Text = pText;
            bubble.Owner = pSpeaker;
            bubble.Position = Camera.main.WorldToScreenPoint(pSpeaker.position);

            ChatBubbles.Add(bubble);
        }

        public static void CloseChatBubble(Transform pSpeaker)
        {
            var bubble = ChatBubbles.Find(a => a.Owner = pSpeaker);
            bubble?.Close();

            //if (bubble != null)
            //{
            //    bubble.Close();
            //}
        }
    }
}
