using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using System;
using System.Linq;
using Xunit;

namespace Stammdatenverwaltung.UI.WPF.Tests
{
    public class MainWindowTests:IDisposable
    {
        FlaUI.Core.Application app;
        public MainWindowTests()
        {
            var appPath = "Stammdatenverwaltung.UI.WPF.exe";
            app = FlaUI.Core.Application.Launch(appPath);
        }

        public void Dispose()
        {
            app.Close();
        }

        [Fact]
        public void Load_Kunden()
        {
            using var auto = new UIA3Automation();
            var win = app.GetMainWindow(auto);
            var addButton = win.FindFirstDescendant(c => c.ByText("Neu")).AsButton();
            addButton.Invoke();

            var loadButton = win.FindFirstDescendant(c => c.ByText("Laden")).AsButton();
            loadButton.Invoke();

            var dataGrid = win.FindFirstDescendant(c => c.ByAutomationId("myGrid")).AsDataGridView();
            dataGrid.Rows.First().AsGridRow().Select();

            var nameTb = win.FindFirstDescendant(c => c.ByAutomationId("nameTb")).AsTextBox();
            Assert.Equal("Fred99", nameTb.Text);
        }
    }
}
