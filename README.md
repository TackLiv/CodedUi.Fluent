CodedUi.Fluent
===
A set of extensions to create human-readable Codded UI tests.

It can help to reduce the code amount, keep the code clean and improve the productivity. The library are mainly built on C# extension method for Fluent API, so it quite easy to extend it for your future needs. 

Read more [here](https://www.pluralsight.com/blog/software-development/about-fluent-api) for advantage of Fluent API.

Avaiable at NuGet: https://www.nuget.org/packages/TackLiv.CodedUi.Fluent

Get Start
---
The usage of this library is quite simple, kindly add it from NuGet, then enjoy!

It will help you get rid of code like
```C#
            WinClient cltEverestlogin = new WinClient(wndEverestlogin);
            cltEverestlogin.SearchProperties[WinControl.PropertyNames.Name] = "Everest login";
            cltEverestlogin.WindowTitles.Add("Everest login");

            WinButton btnOK = new WinButton(cltEverestlogin);
            btnOK.SearchProperties[WinButton.PropertyNames.Name] = "OK";
            btnOK.WindowTitles.Add("Everest login");
            Mouse.Click(btnOK);
```
and make more human-readable code like
```C#
   SelectHelper
     .SearchWindowByTitle("Everest login", "TfrmEverestLogin")
     .Select<WinButton>("OK").Click()
```
Full examples can be found under [here](https://github.com/TackLiv/CodedUi.Fluent/tree/develop/CodedUi.Fluent.Example)

_Please note that though the helper extensions could make creating code easier, you would still need knowledge about Coded UI and better to have a clear picture how the Coded UI engine find a control._

Work with Page-Object Pattern
---
The library could give maximum power when be used together with Page-Object pattern. The key of the pattern is **modeling** and **model the UI element rather than steps**. 

For a typical scenario, the steps could be:
* Go through Test steps and model encountered Window
* Translate steps into Model operations

Here the `FluentCodedUi` library mostly be involved in the modeling step.

Take one class modeling Font option dialog as example, it's written with `FluentCodedUi`
```
    public class FontOptionModel : ModelBase
    {
        public FontOptionModel()
        {
            this.Window = SelectHelper.SearchWindowByTitle("Font", "#32770");
        }
        public WinList FontList => this.Window.Select<WinList>("Font:");
        public WinList FontSizeList => this.Window.Select<WinList>("Size:");
        public WinList FontStyleList => this.Window.Select<WinList>("Font style:");
        public void Ok()
        {
            this.Window.Select<WinButton>("OK").Click();
        }
    }
```
If writing with plain Coded UI, to get the dialog object the code would be:
```
var win = new WinWindow();
win.SearchProperties.Add(new PropertyExpression(UITestControl.PropertyNames.Name, "Font", PropertyExpressionOperator.Contains));
win.SearchProperties.Add(new PropertyExpression(UITestControl.PropertyNames.ClassName, "#32770", PropertyExpressionOperator.Contains));
win.WindowTitles.Add("Font");
``` 
And then to get the Font List, the code could be:
```
var list = new WinList(win);
list.SearchProperties.Add(UITestControl.PropertyNames.Name, "Font:");
list.WindowTitles.Add("Font");
```
This is just the idealize case, if the target control is under a deep hierarchy, then there might be lots of repeated code as above to find for each level.

Now with the help of this library, we can simply achieve same with simply one line like did in above class.
```
this.Window = SelectHelper.SearchWindowByTitle("Font", "#32770");
```
and to get one control of that dialog we only need another line:
```
public WinList FontList => this.Window.Select<WinList>("Font:");
```
### Extra Notes for Page-Object Patter

In nature developer may implement one UI test in a sequence step by step literally based on the test cases definition. And then since **D.R.Y** principle is there, common used steps are extracted out. 

The code may look like:
```
    var obj = new Class();
    obj.DoLoginAndFillLotsOfFields();
    AnotherClass.DoSomeKindOfProcedure(arg1, arg2)
```

This is fine but this is not all. The smallest item of test is the UI element rather than procedures. No matter how the procedure was modeled, there would be still repeat part when same dialog involved in different procedure. It may be find and operated on repeatedly in code for different procedure.

Another drawback of modeling procedure is that procedure is not always stable. There might be lots of procedure which have minor difference or parameter. In this case, the code would soon become complex and hard to be reused and even hard to be read and understand. 

Take the following code snippet as example:
 ```
enterLoginDetailsForm.EnterLoginDetails(userName, password, appServer, dbName, companyCode);
defaultJurisdictionAndLocationSubLocationForm.DefaultJurisdictionAndLocationSubLocationTest(jurisdiction, location, timeZone);
everestTipoftheDayForm.EverestTipoftheDay_ClickBtnClose();
 ```
Pretty hard to figure out what exactly it do, right? Now check the following code for same procedure:
```
            // Detail comment would be added after confirmed EVTCM-764
            var loginDetail = new LoginDetailModel();
            loginDetail.Window.WaitForControlExist();
            loginDetail.UserCode.Text = UserName;
            loginDetail.Password.SendKeys(Password);
            loginDetail.OptionsExpandButton.Click();
            loginDetail.ApplicationServerName.Text = AppServer;
            loginDetail.DatabaseServerName.Text = DbName;
            loginDetail.Ok.Click();
```
It isn't this bit more clear than before? And there is also a good benefit that anywhere when you encounter the modeled dialog, you have it there ready to be reused.

An code example with Fluent API and Page-Object pattern can be found [here](https://github.com/TackLiv/CodedUi.Fluent/tree/develop/CodedUi.Fluent.Example)

## Build & Contribute

Though the library itself is targeting .Net framework 4.5 and can be used by any 4.5 or above project, building or contributing to it will require Visual Studio 2015 / 2017 installed.

> Coded UI requires Enterprise version of Visual Studio

When contributing, the solution come with latest C# compiler which support latest feature of C#. Achieved this through the package: [Microsoft.Net.Compilers](https://www.nuget.org/packages/Microsoft.Net.Compilers/)


## Fire Issues

Please feel free to fire [Issue Ticket](https://github.com/TackLiv/CodedUi.Fluent/issues) if any issue found.

## References

* [Fluent API](https://en.wikipedia.org/wiki/Fluent_interface#C#)
* [.Net Compiler](https://github.com/dotnet/roslyn)
* [How does Coded UI Finds A Control](https://blogs.msdn.microsoft.com/balagans/2009/12/28/how-does-coded-ui-test-finds-a-control/)
