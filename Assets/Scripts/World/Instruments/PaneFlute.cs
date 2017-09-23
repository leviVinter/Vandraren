using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vandraren.Inputs;

namespace Vandraren.Instruments
{
    public class PaneFlute : MonoBehaviour
    {
        private InputChecker _InputChecker;

        private void Awake()
        {
            _InputChecker = new InputChecker();
            SetupInputChecker();
        }

        private void SetupInputChecker()
        {
            //_InputChecker.AddInputCheck(ButtonName.Right, MovePlayer);
            //_InputChecker.AddInputCheck(ButtonName.Right, StopPlayer, ButtonPressType.Up);
        }
    }
}