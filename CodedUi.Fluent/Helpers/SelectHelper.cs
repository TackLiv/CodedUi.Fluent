namespace TackLiv.CodedUi.Fluent.Helpers
{
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;

    using TackLiv.CodedUi.Fluent.Extensions;

    public static class SelectHelper
    {
        /// <summary>
        /// Searches the popup window / control.
        /// </summary>
        /// <param name="className">Name of the class.</param>
        /// <param name="accessibleName">Name of the accessible.</param>
        /// <param name="name">The name.</param>
        /// <returns>The <see cref="UITestControl"/> object for expecting popup. </returns>
        public static UITestControl SearchPopup(string className, string accessibleName = null, string name = null)
        {
            var win = new UITestControl()
                .AddSearchProperty(UITestControl.PropertyNames.ClassName, className)
                .AddSearchProperty(WinWindow.PropertyNames.AccessibleName, accessibleName)
                .AddSearchProperty(UITestControl.PropertyNames.Name, name);
            return win;
        }

        /// <summary>
        /// Searches the window by title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="strictMatch">if set to <c>true</c> [strict match].</param>
        /// <returns>The <see cref="WinWindow"/> object.</returns>
        public static WinWindow SearchWindowByTitle(string title, string className, bool strictMatch = false)
        {
            var win = strictMatch
                          ? SearchWindowByExplicitTitle(title, className)
                          : SearchWindowContainsTitle(title, className);
            return win;
        }

        private static WinWindow SearchWindowByExplicitTitle(string title, string className)
        {
            // PropertyExpressionOperator.EqualTo by default.
            var win = new WinWindow().AddSearchProperties(
                UITestControl.PropertyNames.Name,
                title,
                UITestControl.PropertyNames.ClassName,
                className);
            win.WindowTitles.Add(title);
            return win;
        }

        private static WinWindow SearchWindowContainsTitle(string title, string className)
        {
            var win = new WinWindow()
                .AddSearchProperty(UITestControl.PropertyNames.Name, title, PropertyExpressionOperator.Contains)
                .AddSearchProperty(
                    UITestControl.PropertyNames.ClassName,
                    className,
                    PropertyExpressionOperator.Contains);
            win.WindowTitles.Add(title);
            return win;
        }
    }
}