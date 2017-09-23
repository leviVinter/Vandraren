using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vandraren.Sound;

namespace Vandraren.Helpers
{
    public class ResourcesPaths
    {
        /// <summary>
        /// Get the sound folder of a certain soundtype.
        /// </summary>
        /// <param name="pType"></param>
        /// <returns></returns>
        public static string SoundFolder(SoundType pType)
        {
            string soundFolder = "Sound/";
            string typeFolder = "";

            switch (pType)
            {
                case SoundType.MUSIC:
                    typeFolder = "Music/";
                    break;

                case SoundType.SFX:
                    typeFolder = "Sfx/";
                    break;
            }

            return string.Format("{0}{1}", soundFolder, typeFolder);
        }
    }
}