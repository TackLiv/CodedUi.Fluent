namespace TackLiv.CodedUi.Fluent.Examples.Models
{
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;

    using TackLiv.CodedUi.Fluent.Extensions;
    using TackLiv.CodedUi.Fluent.Helpers;

    public class PopupMessageModel : ModelBase
    {
        public PopupMessageModel()
        {
            this.Window = SelectHelper.SearchWindowByTitle("Notepad", "#32770");
        }

        public void DontSave()
        {
            this.Window.Select<WinButton>("Don't Save").Click();
        }
    }
}