namespace TackLiv.CodedUi.Fluent.Helpers
{
    using Microsoft.VisualStudio.TestTools.UITesting;

    public static class OperationHelper
    {
        /// <summary>
        /// For global key press or shortcuts with delay
        /// </summary>
        /// <param name="keyString">The key String to be sent.</param>
        /// <param name="delay">The delay in milliseconds, this will override the Playback setting.</param>
        public static void PressKey(string keyString, int delay = 1000)
        {
            var stash = Keyboard.SendKeysDelay;
            Keyboard.SendKeysDelay = delay;
            Keyboard.SendKeys(keyString);
            Keyboard.SendKeysDelay = stash;
        }
    }
}