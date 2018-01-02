namespace TackLiv.CodedUi.Fluent.Examples
{
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TackLiv.CodedUi.Fluent.Examples.Models;
    using TackLiv.CodedUi.Fluent.Extensions;

    /// <summary>
    /// Test class for notepad example
    /// </summary>
    [CodedUITest]
    public class TestClassNotepadExample
    {
        private static ApplicationUnderTest app;

        /// <summary>
        /// Codeds the UI test method.
        /// http://testrai.devfactory.com/dummypath/1234567
        /// </summary>
        [TestMethod]
        public void C1234567()
        {
            // 1 Launch notepad
            // -> Verify notepad launched
            var notepadMain = new NotepadMainModel();
            notepadMain.Window.ValidateExistsOrNot(true);

            // 2 Type BlaBlaBla in text area
            // -> Verify text exists
            notepadMain.TextAreaEdit.Text = "BlaBlaBla";
            Assert.IsTrue(notepadMain.TextAreaEdit.Text.Contains("BlaBlaBla"));

            // 5 Close notepad by click Close button.
            // Click don't save on message box
            // -> Verify notepad closed.
            notepadMain.Close();
            var messageWin = new PopupMessageModel();
            messageWin.Window.ValidateExistsOrNot(true);
            messageWin.DontSave();
            notepadMain.Window.ValidateExistsOrNot(false);
        }

        [TestCleanup]
        public void DoTestCleanup()
        {
            app?.Close();
        }

        [TestInitialize]
        public void DoTestInit()
        {
            app = ApplicationUnderTest.Launch(@"C:\Windows\notepad.exe");
        }
    }
}