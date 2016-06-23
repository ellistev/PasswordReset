// ***********************************************************************
// Assembly         : PollGeographicCoordinates
// Author           : selliott
// Created          : 03-18-2016
//
// Last Modified By : selliott
// Last Modified On : 03-18-2016
// ***********************************************************************
// <copyright file="FindCoordinates.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using PasswordResetSelenium.Pages;

/// <summary>
/// The PollGeographicCoordinates namespace.
/// </summary>
namespace PasswordResetSelenium
{
    /// <summary>
    /// Class FindCoordinates.
    /// </summary>
    class AccountUnlocker
    {
        /// <summary>
        /// Gets the driver.
        /// </summary>
        /// <value>The driver.</value>
        public static IWebDriver Driver { get; private set; }

        /// <summary>
        /// The selenium options
        /// </summary>
        private static string SeleniumOptions = "--disable-extensions";

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                //InputFileLocation = args[0].Trim('"');
            }

            if (args.Length > 1)
            {
                //SeleniumOptions = args[1].Trim('"');
            }

            Setup();

            ResetPassword();

            TearDown();

        }

        private static void ResetPassword()
        {
            //List<string> lines = new List<string>();

            //lines = ReadLinesFromFile(lines, InputFileLocation);

            PasswordPage myPasswordPage = new PasswordPage(Driver);
            myPasswordPage.OpenPage();


            //Tuple<List<string>, List<string>> processedLocations = ProcessMissingLocations(lines, latlongpage);
            ResetAccount(myPasswordPage);

            //File.WriteAllLines("good.txt", processedLocations.Item1.ToArray());
            //File.WriteAllLines("bad.txt", processedLocations.Item2.ToArray());
            //Process.Start("notepad.exe", "good.txt");
            Driver.Quit();
        }


        /// <summary>
        /// Setups this instance.
        /// </summary>
        public static void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument(SeleniumOptions);
            Driver = new ChromeDriver(options);

        }


        /// <summary>
        /// Tears down.
        /// </summary>
        public static void TearDown()
        {
            Driver.Quit();
        }

        /// <summary>
        /// Processes the missing locations.
        /// </summary>
        /// <param name="myPasswordPage"></param>
        /// <returns>Tuple&lt;List&lt;System.String&gt;, List&lt;System.String&gt;&gt;.</returns>
        private static void ResetAccount(PasswordPage myPasswordPage)
        {


            myPasswordPage.UnlockMyAccountLink.Click();
            Thread.Sleep(5000);

            UserNamePage usernamePage = new UserNamePage(Driver);
            //usernamePage.ClickItem(usernamePage.UsernameField);
            //usernamePage.SetFieldValue(usernamePage.UsernameField, "selliott");
            Actions action = new Actions(Driver);

            action.SendKeys("selliott").Build().Perform();
            Thread.Sleep(1500);

            action.SendKeys(Keys.Tab).Build().Perform();
            Thread.Sleep(1500);

            action.SendKeys("BASPINT").Build().Perform();
            Thread.Sleep(1500);

            action.SendKeys(Keys.Tab).Build().Perform();
            Thread.Sleep(2000);

            action.SendKeys(Keys.Enter).Build().Perform();
            Thread.Sleep(5000);

            action.SendKeys("nissan").Build().Perform();
            Thread.Sleep(1500);

            action.SendKeys(Keys.Tab).Build().Perform();
            Thread.Sleep(1500);

            action.SendKeys(Keys.Enter).Build().Perform();
            Thread.Sleep(1500);

            action.SendKeys("blue").Build().Perform();
            Thread.Sleep(1500);

            action.SendKeys(Keys.Tab).Build().Perform();
            Thread.Sleep(1500);

            action.SendKeys(Keys.Enter).Build().Perform();
            Thread.Sleep(1500);

            action.SendKeys("sierra").Build().Perform();
            Thread.Sleep(1500);

            action.SendKeys(Keys.Tab).Build().Perform();
            Thread.Sleep(1500);

            action.SendKeys(Keys.Enter).Build().Perform();
            Thread.Sleep(1500);

            action.SendKeys(Keys.Tab).Build().Perform();
            Thread.Sleep(1500);

            action.SendKeys(Keys.Enter).Build().Perform();
            Thread.Sleep(10000);





        }

    }
}
