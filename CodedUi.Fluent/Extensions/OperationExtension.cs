namespace TackLiv.CodedUi.Fluent.Extensions
{
    using System;
    using System.Drawing;

    using Microsoft.VisualStudio.TestTools.UITest.Common;
    using Microsoft.VisualStudio.TestTools.UITest.Input;
    using Microsoft.VisualStudio.TestTools.UITesting;

    public static class OperationExtension
    {
        /// <summary>
        /// Clicks the specified button.
        /// </summary>
        /// <param name="target">The target to be click.</param>
        /// <param name="button">The mouse button.</param>
        /// <param name="mouseAction">The mouse action.</param>
        /// <param name="modKey">The mod key.</param>
        /// <param name="relativeCoordinate">The relative coordinate to the target boundary.</param>
        /// <param name="timeout">The timeout to wait for the operating target.</param>
        /// <param name="mandatory">
        ///     if set to <c>true</c> then it's [mandatory] that target must exists and operation must be done.
        /// </param>
        /// <returns>The this object.</returns>
        /// <exception cref="ArgumentOutOfRangeException">mouseAction - null</exception>
        public static UITestControl Click(
            this UITestControl target,
            MouseButtons button = MouseButtons.Left,
            MouseActionType mouseAction = MouseActionType.Click,
            ModifierKeys modKey = ModifierKeys.None,
            Point? relativeCoordinate = null,
            int timeout = -1,
            bool mandatory = true)
        {
            if (timeout > 0)
            {
                target.WaitForControlReady(timeout);
            }
            else
            {
                target.WaitForControlReady();
            }

            if (!mandatory && !target.Exists)
            {
                return target;
            }

            switch (mouseAction)
            {
                case MouseActionType.Click:
                    DoClick(target, button, modKey, relativeCoordinate, Mouse.Click, Mouse.Click);

                    break;
                case MouseActionType.DoubleClick:
                    DoClick(target, button, modKey, relativeCoordinate, Mouse.DoubleClick, Mouse.DoubleClick);

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mouseAction), mouseAction, null);
            }

            return target;
        }

        /// <summary>
        /// Focuses the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>The this object.</returns>
        public static UITestControl Focus(this UITestControl target)
        {
            target.SetFocus();
            return target;
        }

        /// <summary>
        /// Sends the keys to a control
        /// </summary>
        /// <param name="target">The target to send.</param>
        /// <param name="text">The keys in text.</param>
        /// <param name="delay">The delay between key press.</param>
        /// <param name="modKey">The mod key when press.</param>
        /// <returns>
        /// The this object.
        /// </returns>
        public static UITestControl SendKeys(
            this UITestControl target,
            string text,
            int delay = 0,
            ModifierKeys modKey = ModifierKeys.None)
        {
            var stash = Keyboard.SendKeysDelay;
            if (delay > 0)
            {
                Keyboard.SendKeysDelay = delay;

                // Keyboard delay would apply after first key
                // Here add extra delay before first key.
                // This is for scenario where CodedUI Engine can not control.
                Playback.Wait(delay);
            }

            Keyboard.SendKeys(target, text, (System.Windows.Input.ModifierKeys)modKey);
            Keyboard.SendKeysDelay = stash;

            return target;
        }

        private static void DoClick(
            UITestControl target,
            MouseButtons button,
            ModifierKeys modKey,
            Point? relativeCoordinate,
            Action<System.Windows.Forms.MouseButtons, System.Windows.Input.ModifierKeys, Point> clickCoordinate,
            Action<UITestControl, System.Windows.Forms.MouseButtons> clickControl)
        {
            if (relativeCoordinate != null)
            {
                var point = new Point(
                    target.BoundingRectangle.X + relativeCoordinate.Value.X,
                    target.BoundingRectangle.Y + relativeCoordinate.Value.Y);
                clickCoordinate.Invoke(
                    (System.Windows.Forms.MouseButtons)button,
                    (System.Windows.Input.ModifierKeys)modKey,
                    point);
            }
            else
            {
                clickControl.Invoke(target, (System.Windows.Forms.MouseButtons)button);
            }
        }
    }
}