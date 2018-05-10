using Vandraren.Sound;

namespace Vandraren.Helpers
{
    public class ResourcePaths
    {
        /// <summary>
        /// Get the sound folder of a certain soundtype.
        /// </summary>
        /// <param name="pType">Sound type to look for</param>
        /// <returns>Name of the sound folder</returns>
        public static string GetSoundFolder(SoundType pType)
        {
            var soundFolder = "Sound/";
            var typeFolder = "";

            switch (pType)
            {
                case SoundType.Music:
                    typeFolder = "Music/";
                    break;

                case SoundType.Sfx:
                    typeFolder = "Sfx/";
                    break;

                default:
                    typeFolder = "";
                    break;
            }

            return $"{soundFolder}{typeFolder}";
        }
    }
}