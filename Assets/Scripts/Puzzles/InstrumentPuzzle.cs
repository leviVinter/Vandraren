using System;
using System.Collections.Generic;
using UnityEngine;
using Vandraren.Inputs;
using Vandraren.UI;

namespace Assets.Scripts.Puzzles
{
    public class InstrumentPuzzle : MonoBehaviour
    {
        private ButtonName[] _ButtonOrder;
        private float _MaximumDistance;
        private bool _IsActive;
        private Transform _Player;
        private InputChecker _InputChecker;
        private List<ButtonName> _CorrectButtonsPlayed = new List<ButtonName>();
        private SpriteRenderer[] _Boxes;

        private void Awake()
        {
            _Boxes = GetComponentsInChildren<SpriteRenderer>();

            _ButtonOrder = new[]
            {
                ButtonName.One,
                ButtonName.Two,
                ButtonName.Three,
                ButtonName.Four
            };

            Activate(false);

            _MaximumDistance = 5f;
            _Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

            SetupInputChecker();
        }

        private void Update()
        {
            var puzzlePosition = transform.position;
            var playerPosition = _Player.transform.position;
            var distance = Vector3.Distance(playerPosition, puzzlePosition);

            if (!_IsActive && distance < _MaximumDistance)
            {
                Activate();
                MessageManager.OpenChatBubble("Play a tune to remove these boxes", transform);
            }
            else if (_IsActive && distance > _MaximumDistance)
            {
                Activate(false);
                MessageManager.CloseChatBubble(transform);
            }

            if (_IsActive)
            {
                _InputChecker.Check();
            }
        }

        private void SetupInputChecker()
        {
            _InputChecker = new InputChecker();

            var buttons = new ButtonName[]
            {
                ButtonName.One,
                ButtonName.Two,
                ButtonName.Three,
                ButtonName.Four,
                ButtonName.Five
            };

            var actions = new Action[]
            {
                () => CheckIfCorrectOrder(ButtonName.One),
                () => CheckIfCorrectOrder(ButtonName.Two),
                () => CheckIfCorrectOrder(ButtonName.Three),
                () => CheckIfCorrectOrder(ButtonName.Four),
                () => CheckIfCorrectOrder(ButtonName.Five)
            };

            for (var i = 0; i < buttons.Length; i++)
            {
                _InputChecker.AddButtonCheck(buttons[i], actions[i]);
            }
        }

        private void CheckIfCorrectOrder(ButtonName pButton)
        {
            var correctCount = _CorrectButtonsPlayed.Count;
            var nextButton = _ButtonOrder[correctCount];

            if (pButton == nextButton)
            {
                _CorrectButtonsPlayed.Add(pButton);
                ChangeSprite(correctCount, true);
                Debug.Log("Correct button");
            }
            else
            {
                for (var i = 0; i < _CorrectButtonsPlayed.Count; i++)
                {
                    ChangeSprite(i, false);
                }

                _CorrectButtonsPlayed.Clear();
                Debug.Log("Wrong button");
            }

            if (_CorrectButtonsPlayed.Count == _ButtonOrder.Length)
            {
                Activate(false);
                Debug.Log("You're done");
                DestroyAll();
            }
        }

        public void Activate(bool pActivate = true)
        {
            _IsActive = pActivate;
        }

        private void DestroyAll()
        {
            MessageManager.CloseChatBubble(transform);
            Destroy(gameObject);
        }

        private void ChangeSprite(int pIndex, bool pCorrect)
        {
            var spriteName = pCorrect ? "green_box" : "blue_box";

            _Boxes[pIndex].sprite = Resources.Load($"Sprites/Obstacles/{spriteName}", typeof(Sprite)) as Sprite;
        }
    }
}
