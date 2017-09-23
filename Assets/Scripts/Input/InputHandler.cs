using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vandraren.Inputs
{
    public enum ButtonName
    {
        None,
        Left,
        Right,
        One,
        Two,
        Three,
        Four,
        Five
    }

    public class InputHandler
    {
        private Dictionary<ButtonName, KeyCode> KeyNames;

        public InputHandler()
        {
            KeyNames = new Dictionary<ButtonName, KeyCode>()
            {
                { ButtonName.Left, KeyCode.A },
                { ButtonName.Right, KeyCode.D },
                { ButtonName.One, KeyCode.Alpha1 },
                { ButtonName.Two, KeyCode.Alpha2 },
                { ButtonName.Three, KeyCode.Alpha3 },
                { ButtonName.Four, KeyCode.Alpha4 },
                { ButtonName.Five, KeyCode.Alpha5 }
            };
        }

        public bool GetButtonDown(ButtonName pButton)
        {
            return Input.GetKeyDown(KeyNames[pButton]);
        }

        public bool GetButton(ButtonName pButton)
        {
            return Input.GetKey(KeyNames[pButton]);
        }

        public bool GetButtonUp(ButtonName pButton)
        {
            return Input.GetKeyUp(KeyNames[pButton]);
        }
    }
}
