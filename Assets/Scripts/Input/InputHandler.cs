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
        PlayInstrument,
        One,
        Two,
        Three,
        Four,
        Five
    }

    public static class InputHandler
    {
        private static Dictionary<ButtonName, KeyCode> _KeyNames = new Dictionary<ButtonName, KeyCode>()
        {
            { ButtonName.Left, KeyCode.A },
            { ButtonName.Right, KeyCode.D },
            { ButtonName.One, KeyCode.Alpha1 },
            { ButtonName.Two, KeyCode.Alpha2 },
            { ButtonName.Three, KeyCode.Alpha3 },
            { ButtonName.Four, KeyCode.Alpha4 },
            { ButtonName.Five, KeyCode.Alpha5 },
            { ButtonName.PlayInstrument, KeyCode.T },
        };

        public static bool GetButtonDown(ButtonName pButton)
        {
            return Input.GetKeyDown(_KeyNames[pButton]);
        }

        public static bool GetButton(ButtonName pButton)
        {
            return Input.GetKey(_KeyNames[pButton]);
        }

        public static bool GetButtonUp(ButtonName pButton)
        {
            return Input.GetKeyUp(_KeyNames[pButton]);
        }
    }
}
