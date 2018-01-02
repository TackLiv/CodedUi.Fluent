namespace TackLiv.CodedUi.Fluent.Extensions
{
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;

    public static class CodedUiExtensionsSearch
    {
        /// <summary>
        /// Adds the search configurations in group.
        /// </summary>
        /// <typeparam name="T">The this type <see cref="UITestControl"/></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="configuratioNames">The configuratio names.</param>
        /// <returns>The this object.</returns>
        public static T AddSearchConfigurations<T>(this T target, params string[] configuratioNames)
            where T : UITestControl
        {
            foreach (var cfgName in configuratioNames)
            {
                target.SearchConfigurations.Add(cfgName);
            }

            return target;
        }

        /// <summary>
        /// Adds the search properties in group.
        /// </summary>
        /// <typeparam name="T">The this type <see cref="UITestControl"/></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="nameValuePairs">The name value pairs.</param>
        /// <returns>The this object.</returns>
        public static T AddSearchProperties<T>(this T target, params string[] nameValuePairs)
            where T : UITestControl
        {
            target.SearchProperties.Add(nameValuePairs);
            return target;
        }

        /// <summary>
        /// Adds single search property with operator condition.
        /// </summary>
        /// <typeparam name="T">The this type <see cref="UITestControl"/></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyOperator">The property operator.</param>
        /// <returns>The this object.</returns>
        public static T AddSearchProperty<T>(
            this T target,
            string name,
            string value,
            PropertyExpressionOperator propertyOperator = PropertyExpressionOperator.EqualTo)
            where T : UITestControl
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
            {
                target.SearchProperties.Add(new PropertyExpression(name, value, propertyOperator));
            }

            return target;
        }

        /// <summary>
        /// Adds the window titles.
        /// </summary>
        /// <typeparam name="T">The this type <see cref="UITestControl"/></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="title">The title.</param>
        /// <returns>The this object.</returns>
        public static T AddWindowTitles<T>(this T target, string title)
            where T : UITestControl
        {
            target.WindowTitles.Add(title);
            return target;
        }

        /// <summary>
        /// Convert to the specified type.
        /// </summary>
        /// <typeparam name="T">The this type <see cref="UITestControl"/></typeparam>
        /// <param name="target">The target.</param>
        /// <returns>The this object.</returns>
        public static T As<T>(this UITestControl target)
            where T : UITestControl
        {
            return (T)target;
        }

        /// <summary>
        /// Gets the CodedUI object by it'scontainer identifier.
        /// </summary>
        /// <typeparam name="T">The this type <see cref="UITestControl"/></typeparam>
        /// <param name="parent">The parent.</param>
        /// <param name="containerId">The container identifier.</param>
        /// <param name="name">The name.</param>
        /// <returns>The this object.</returns>
        public static T GetByContainerId<T>(this UITestControl parent, int containerId, string name = null)
            where T : UITestControl, new()
        {
            return parent.GetChild<WinWindow>(controlId: containerId.ToString()).GetChild<T>(name);
        }

        /// <summary>
        /// Gets object represent a specific child of the given parent.
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
        public static T GetChild<T>(
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
            var control = new T { Container = parent };

            if (disambiguate)
            {
                control.AddSearchConfigurations(SearchConfiguration.DisambiguateChild);
            }

            control.AddSearchProperty(UITestControl.PropertyNames.Name, name)
                .AddSearchProperty(UITestControl.PropertyNames.ClassName, className)
                .AddSearchProperty(WinControl.PropertyNames.ControlId, controlId)
                .AddSearchProperty(UITestControl.PropertyNames.ControlType, controlType).AddSearchProperty(
                    UITestControl.PropertyNames.Instance,
                    instance);

            if (moreSearchProperties.Length > 0)
            {
                control.AddSearchProperties(moreSearchProperties);
            }

            foreach (var title in parent.WindowTitles)
            {
                control.WindowTitles.Add(title);
            }

            return control;
        }

        /// <summary>
        /// Gets the menu item from <see cref="WinMenuBar"/>.
        /// </summary>
        /// <param name="menu">The menu bar object.</param>
        /// <param name="commands">The command list.</param>
        /// <returns>The this object.</returns>
        public static WinMenuItem GetMenuItem(this WinMenuBar menu, params string[] commands)
        {
            UITestControl target = menu;
            foreach (var command in commands)
            {
                var menuItem = target.Select<WinMenuItem>(command)
                    .WithConfiguration(SearchConfiguration.ExpandWhileSearching);
                target = menuItem;
            }

            return (WinMenuItem)target;
        }

        /// <summary>
        /// Gets the tree item according provided path in string arrary.
        /// </summary>
        /// <param name="tree">The tree object.</param>
        /// <param name="nodes">The node list.</param>
        /// <returns>The this object.</returns>
        public static WinTreeItem GetTreeItem(this WinTree tree, params string[] nodes)
        {
            UITestControl target = tree;
            foreach (var node in nodes)
            {
                target = target.Select<WinTreeItem>(node).AddSearchConfigurations(
                    SearchConfiguration.ExpandWhileSearching,
                    SearchConfiguration.NextSibling);
            }

            return (WinTreeItem)target;
        }
    }
}