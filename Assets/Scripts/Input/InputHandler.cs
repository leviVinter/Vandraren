using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vandraren.Inputs
{
    public enum ButtonName
    {
        PlayInstrument,
        RemoveInstrument,
        One,
        Two,
        Three,
        Four,
        Five
    }

    public enum AxisName
    {
        Horizontal
    }

    public static class InputHandler
    {
        private static Dictionary<ButtonName, KeyCode> _ButtonNames = new Dictionary<ButtonName, KeyCode>
        {
            { ButtonName.One, KeyCode.Alpha1 },
            { ButtonName.Two, KeyCode.Alpha2 },
            { ButtonName.Three, KeyCode.Alpha3 },
            { ButtonName.Four, KeyCode.Alpha4 },
            { ButtonName.Five, KeyCode.Alpha5 },
            { ButtonName.PlayInstrument, KeyCode.T },
            { ButtonName.RemoveInstrument, KeyCode.T }
        };

        private static Dictionary<AxisName, string> _AxisNames = new Dictionary<AxisName, string>
        {
            { AxisName.Horizontal, "Horizontal" }
        };

        public static float GetAxis(AxisName pAxis) => Input.GetAxis(_AxisNames[pAxis]);

        public static bool GetButtonDown(ButtonName pButton) => Input.GetKeyDown(_ButtonNames[pButton]);

        public static bool GetButton(ButtonName pButton) => Input.GetKey(_ButtonNames[pButton]);

        public static bool GetButtonUp(ButtonName pButton) => Input.GetKeyUp(_ButtonNames[pButton]);
    }
}
