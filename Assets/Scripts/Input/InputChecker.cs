using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Vandraren.Inputs
{
    public enum ButtonPressType
    {
        Down,
        Pressed,
        Up
    }

    public class InputChecker
    {
        private Dictionary<ButtonName, Action> _ButtonsDown { get; set; }
        private Dictionary<ButtonName, Action> _ButtonsPressed { get; set; }
        private Dictionary<ButtonName, Action> _ButtonsUp { get; set; }

        public InputChecker()
        {
            _ButtonsDown = new Dictionary<ButtonName, Action>();
            _ButtonsPressed = new Dictionary<ButtonName, Action>();
            _ButtonsUp = new Dictionary<ButtonName, Action>();
        }

        public void Check()
        {
            CheckButtonsDown();
            CheckButtonsPressed();
            CheckButtonsUp();
        }

        private void CheckButtonsDown()
        {
            foreach (KeyValuePair<ButtonName, Action> button in _ButtonsDown)
            {
                if (InputHandler.GetButtonDown(button.Key))
                {
                    button.Value();
                }
            }
        }

        private void CheckButtonsPressed()
        {
            foreach (KeyValuePair<ButtonName, Action> button in _ButtonsPressed)
            {
                if (InputHandler.GetButton(button.Key))
                {
                    button.Value();
                }
            }
        }

        private void CheckButtonsUp()
        {
            foreach (KeyValuePair<ButtonName, Action> button in _ButtonsUp)
            {
                if (InputHandler.GetButtonUp(button.Key))
                {
                    button.Value();
                }
            }
        }

        public void AddInputCheck(ButtonName pButton, Action pCallback, ButtonPressType pPressType = ButtonPressType.Down)
        {
            switch(pPressType)
            {
                case ButtonPressType.Down:
                    _ButtonsDown[pButton] = pCallback;
                    break;
                case ButtonPressType.Pressed:
                    _ButtonsPressed[pButton] = pCallback;
                    break;
                case ButtonPressType.Up:
                    _ButtonsUp[pButton] = pCallback;
                    break;
                default:
                    new ArgumentException("The button press type is not accounted for");
                    break;
            }
        }
    }
}
