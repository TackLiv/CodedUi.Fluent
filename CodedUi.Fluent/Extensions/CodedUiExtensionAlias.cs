namespace TackLiv.CodedUi.Fluent.Extensions
{
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;

    public static class CodedUiExtensionAlias
    {
        /// <summary>
        /// Alias function of GetChild. <seealso cref="CodedUiExtensionsSearch"/>
        /// </summary>
        /// <typeparam name="T">The this type <see cref="UITestControl"/></typeparam>
        /// <param name="parent">The parent container to be searched.</param>
        /// <param name="name">The name condition.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="controlId">The control identifier.</param>
        /// <param name="controlType">Type of the control.</param>
        /// <param name="instance">The instance.</param>
        /// <param name="disambiguate">
        ///     if set to <c>true</c> [disambiguate], apply the search configuration.
        ///     <seealso cref="SearchConfiguration.DisambiguateChild"/>
        /// </param>
        /// <param name="moreSearchProperties">The more search properties.</param>
        /// <returns>The this object.</returns>
        public static T Select<T>(
            this UITestControl parent,
            string name = null,
            string className = null,
            string controlId = null,
            string controlType = null,
            string instance = null,
            bool disambiguate = false,
            params string[] moreSearchProperties)
            where T : UITestControl, new()
        {
            return parent.GetChild<T>(
                name,
                className,
                controlId,
                controlType,
                instance,
                disambiguate,
                moreSearchProperties);
        }

        /// <summary>
        /// Alias function of GetByContainerId. <seealso cref="CodedUiExtensionsSearch"/>
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