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

    /// <summary>
    /// Collect all the buttons to check input for
    /// </summary>
    public class InputChecker
    {
        private Dictionary<ButtonName, Action> _ButtonsDown;
        private Dictionary<ButtonName, Action> _ButtonsPressed;
        private Dictionary<ButtonName, Action> _ButtonsUp;
        private Dictionary<AxisName, Action<float>> _Axis;

        public InputChecker()
        {
            _ButtonsDown = new Dictionary<ButtonName, Action>();
            _ButtonsPressed = new Dictionary<ButtonName, Action>();
            _ButtonsUp = new Dictionary<ButtonName, Action>();
            _Axis = new Dictionary<AxisName, Action<float>>();
        }

        /// <summary>
        /// Check if any buttons are pressed
        /// </summary>
        public void Check()
        {
            CheckButtonsDown();
            CheckButtonsPressed();
            CheckButtonsUp();
            CheckAxis();
        }

        /// <summary>
        /// Check if a button is down
        /// </summary>
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

        /// <summary>
        /// Check if a button is pressed and held down
        /// </summary>
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

        /// <summary>
        /// Check if a button is released
        /// </summary>
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

        private void CheckAxis()
        {
            foreach (KeyValuePair<AxisName, Action<float>> axis in _Axis)
            {
                axis.Value(InputHandler.GetAxis(axis.Key));
            }
        }

        /// <summary>
        /// Add a button to check input for
        /// </summary>
        /// <param name="pButton">Name of the button</param>
        /// <param name="pCallback">The function to call if the button is used</param>
        /// <param name="pPressType">Which kind of button press to check for</param>
        public void AddButtonCheck(ButtonName pButton, Action pCallback, ButtonPressType pPressType = ButtonPressType.Down)
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

        public void AddAxisCheck(AxisName pAxis, Action<float> pCallback)
        {
            _Axis[pAxis] = pCallback;
        }
    }
}
