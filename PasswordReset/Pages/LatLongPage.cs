// ***********************************************************************
// Assembly         : PollGeographicCoordinates
// Author           : selliott
// Created          : 03-18-2016
//
// Last Modified By : selliott
// Last Modified On : 03-18-2016
// ***********************************************************************
// <copyright file="LatLongPage.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

/// <summary>
/// The Pages namespace.
/// </summary>
namespace PollGeographicCoordinates.Pages
{
    /// <summary>
    /// Class LatLongPage.
    /// </summary>
    class LatLongPage
    {

        /// <summary>
        /// The driver
        /// </summary>
        protected IWebDriver Driver;
        /// <summary>
        /// The time out
        /// </summary>
        public const int TimeOut = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="LatLongPage"/> class.
        /// </summary>
        /// <param name="driver">The driver.</param>
        public LatLongPage(IWebDriver driver)
        {
            this.Driver = driver;
            PageFactory.InitElements(Driver, this);
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            Driver.Quit();
        }

        /// <summary>
        /// Opens the page.
        /// </summary>
        public void OpenPage()
        {
            Driver.Navigate().GoToUrl("http://www.latlong.net/");
            Driver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Gets the place name field.
        /// </summary>
        /// <value>The place name field.</value>
        public By PlaceNameField
        {
            get
            {
                return By.CssSelector("#gadres");
            }
        }

        /// <summary>
        /// Gets the latitude field.
        /// </summary>
        /// <value>The latitude field.</value>
        public By LatitudeField
        {
            get
            {
                return By.Id("lat");
            }
        }

        /// <summary>
        /// Gets the longitude field.
        /// </summary>
        /// <value>The longitude field.</value>
        public By LongitudeField
        {
            get
            {
                return By.Id("lng");
            }
        }

        /// <summary>
        /// Gets the find button.
        /// </summary>
        /// <value>The find button.</value>
        public By FindButton
        {
            get
            {
                return By.CssSelector(".button");
            }
        }



        /// <summary>
        /// Clicks the item.
        /// </summary>
        /// <param name="byItem">The by item.</param>
        public void ClickItem(By byItem)
        {
            Driver.FindElement(byItem).Click();
        }

        /// <summary>
        /// Sets the field value.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="valueToSet">The value to set.</param>
        public void SetFieldValue(By field, string valueToSet)
        {
            IWebElement element = Driver.FindElement(field);
            element.Clear();
            element.SendKeys(valueToSet);
        }

        /// <summary>
        /// Submits the item.
        /// </summary>
        /// <param name="byItem">The by item.</param>
        public void SubmitItem(By byItem)
        {
            Driver.FindElement(byItem).Submit();
        }

        /// <summary>
        /// Waits until element text has changed.
        /// </summary>
        /// <param name="elementToWaitFor">The element to wait for</param>
        /// <param name="originalValue">The original value</param>
        public void WaitUntilElementTextHasChanged(By elementToWaitFor, string originalValue)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TimeOut));
            wait.Until<IWebElement>((d) =>
            {
                IWebElement element = Driver.FindElement(elementToWaitFor);
                if (element.Text != originalValue)
                {
                    return element;
                }

                return null;
            });
        }

        /// <summary>
        /// Waits until url has changed.
        /// </summary>
        public void WaitUntilUrlHasChanged()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TimeOut));
            var originalUrl = this.Driver.Url;

            wait.Until<string>((d) =>
            {
                var url = this.Driver.Url;
                if (url != originalUrl)
                {
                    return url;
                }

                return null;
            });
        }
    }
}
