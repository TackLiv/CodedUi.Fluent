namespace TackLiv.CodedUi.Fluent.Examples.Models
{
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;

    using TackLiv.CodedUi.Fluent.Extensions;
    using TackLiv.CodedUi.Fluent.Helpers;

    public class NotepadMainModel : ModelBase
    {
        public NotepadMainModel()
        {
            this.Window = SelectHelper.SearchWindowByTitle("Untitled - Notepad", "Notepad");
        }

        public WinEdit TextAreaEdit => this.Window.Select<WinEdit>(15);

        public void Close()
        {
            this.Window.Select<WinTitleBar>().Select<WinButton>("Close").Click();
        }

        public void SelectMenu(params string[] commands)
        {
            this.Window.Select<WinMenuBar>("Application").GetMenuItem(commands).Click();
        }
    }
}