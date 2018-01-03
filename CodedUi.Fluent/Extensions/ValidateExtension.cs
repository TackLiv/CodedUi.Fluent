namespace TackLiv.CodedUi.Fluent.Extensions
{
    using System;

    using Microsoft.VisualStudio.TestTools.UITesting;

    public static class ValidateExtension
    {
        private static Action<bool> assertor;

        /// <summary>
        /// Validates control exists or not.
        /// </summary>
        /// <typeparam name="T">The this object <see cref="UITestControl" /></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="exists">Verify exists if set to <c>true</c> [exists], otherwise verify not exists.</param>
        /// <param name="millisecondsTimeout">The timeout in milliseconds, it will override the Playback setting.</param>
        /// <param name="searchAgain">Search the target again before waiting if set to <c>true</c> [always search].</param>
        /// <returns>
        /// The this object.
        /// </returns>
        public static T ValidateExistsOrNot<T>(
            this T target,
            bool exists,
            int millisecondsTimeout = 0,
            bool searchAgain = false)
            where T : UITestControl
        {
            if (searchAgain)
            {
                target.TryFind();
            }

            var result = millisecondsTimeout == 0
                             ? exists
                                   ? target.WaitForControlExist()
                                   : target.WaitForControlNotExist()
                             : exists
                                 ? target.WaitForControlExist(millisecondsTimeout)
                                 : target.WaitForControlNotExist(millisecondsTimeout);
            if (assertor == null)
            {
                throw new InvalidOperationException("No assertor initialized. Invoke Validat");
            }

            assertor.Invoke(result);
            return target;
        }

        public static void SetAssertor(Action<bool> assertResultTrue)
        {
            assertor = assertResultTrue;
        }
    }
}