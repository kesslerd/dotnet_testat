using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;

namespace AutoReservation.UI.Testing
{
    [TestClass]
    public class KundeTest : UITestBase
    {
        // Patrick: Bitte testen
        [TestMethod]
        public void AddButtonOpensDialog()
        {
            var app = Application.Launch(SutPath);
            var window = app.GetWindow("MainWindow", InitializeOption.NoCache);
            Assert.IsNull(app.GetWindow("Kunde", InitializeOption.NoCache));
            var button = window.Get<Button>("AddKundeButton");
            button.Click();
            Assert.IsNotNull(app.GetWindow("Kunde", InitializeOption.NoCache));
            app.Close();
        }

        //[TestMethod]
        //public void ReloadButtonRefreshesPage()
        //{
        //    var app = Application.Launch(SutPath);
        //    var window = app.GetWindow("MainWindow", InitializeOption.NoCache);
        //    Assert.IsNull(app.GetWindow("Kunde", InitializeOption.NoCache));
        //    var button = window.Get<Button>("ReloadKundeButton");
        //    button.Click();
        //    Assert.IsNotNull(app.GetWindow("Kunde", InitializeOption.NoCache));
        //    app.Close();
        //}

        //[TestMethod]
        //public void EditButtonOpensDialog()
        //{
        //    var app = Application.Launch(SutPath);
        //    var window = app.GetWindow("MainWindow", InitializeOption.NoCache);
        //    Assert.IsNull(app.GetWindow("Kunde", InitializeOption.NoCache));
        //    var button = window.Get<Button>("EditKundeButton");
        //    button.Click();
        //    Assert.IsNotNull(app.GetWindow("Kunde", InitializeOption.NoCache));
        //    app.Close();
        //}

        //[TestMethod]
        //public void RemoveButtonRemovesElement()
        //{
        //    var app = Application.Launch(SutPath);
        //    var window = app.GetWindow("MainWindow", InitializeOption.NoCache);
        //    Assert.IsNull(app.GetWindow("Kunde", InitializeOption.NoCache));
        //    var button = window.Get<Button>("DeleteKundeButton");
        //    button.Click();
        //    Assert.IsNotNull(app.GetWindow("Kunde", InitializeOption.NoCache));
        //    app.Close();
        //}
    }
}
