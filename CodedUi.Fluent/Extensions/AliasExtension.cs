namespace TackLiv.CodedUi.Fluent.Extensions
{
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;

    public static class AliasExtension
    {
        /// <summary>
        /// Alias function of GetByContainerId. <seealso cref="SearchExtensions"/>
        /// </summary>
        /// <typeparam name="T">The this type <see cref="UITestControl"/></typeparam>
        /// <param name="parent">The parent.</param>
        /// <param name="controlId">The control identifier.</param>
        /// <param name="name">The name.</param>
        /// <returns>The this object.</returns>
        public static T Select<T>(this UITestControl parent, int controlId, string name = null)
            where T : UITestControl, new()
        {
            return parent.GetByContainerId<T>(controlId, name);
        }

        /// <summary>
        /// Specific additonal search conditions to a CodedUI object.
        /// </summary>
        /// <typeparam name="T">The this type <see cref="UITestControl"/></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="nameValuePairs">The name value pairs.</param>
        /// <returns>The this object.</returns>
        public static T Where<T>(this T target, params string[] nameValuePairs)
            where T : UITestControl
        {
            return target.AddSearchProperties(nameValuePairs);
        }

        /// <summary>
        /// Specific a search with additional configurations.
        /// </summary>
        /// <typeparam name="T">The this type <see cref="UITestControl"/></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="configurationNameStrings">The configuration names.</param>
        /// <returns>The this object.</returns>
        public static T WithConfiguration<T>(this T target, params string[] configurationNameStrings)
            where T : UITestControl
        {
            return target.AddSearchConfigurations(configurationNameStrings);
        }
    }
}