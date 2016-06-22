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
using PollGeographicCoordinates.Pages;

/// <summary>
/// The PollGeographicCoordinates namespace.
/// </summary>
namespace PollGeographicCoordinates
{
    /// <summary>
    /// Class FindCoordinates.
    /// </summary>
    class FindCoordinates
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
        /// The input file location
        /// </summary>
        private static string InputFileLocation = "";


        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                InputFileLocation = args[0].Trim('"');
            }

            if (args.Length > 1)
            {
                SeleniumOptions = args[1].Trim('"');
            }

            Setup();

            FindLocations();

            TearDown();

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
        /// Finds the locations.
        /// </summary>
        public static void FindLocations()
        {
            List<string> lines = new List<string>();

            lines = ReadLinesFromFile(lines, InputFileLocation);

            LatLongPage latlongpage = new LatLongPage(Driver);
            latlongpage.OpenPage();


            Tuple<List<string>, List<string>> processedLocations = ProcessMissingLocations(lines, latlongpage);

            File.WriteAllLines("good.txt", processedLocations.Item1.ToArray());
            File.WriteAllLines("bad.txt", processedLocations.Item2.ToArray());
            Process.Start("notepad.exe", "good.txt");
            Driver.Quit();
        }

        /// <summary>
        /// Processes the missing locations.
        /// </summary>
        /// <param name="lines">The lines.</param>
        /// <param name="latlongpage">The latlongpage.</param>
        /// <returns>Tuple&lt;List&lt;System.String&gt;, List&lt;System.String&gt;&gt;.</returns>
        private static Tuple<List<string>, List<string>> ProcessMissingLocations(List<string> lines, LatLongPage latlongpage)
        {
            List<string> newGoodLines = new List<string>();
            List<string> newBadLines = new List<string>();

            foreach (string s in lines)
            {
                
                latlongpage.ClickItem(latlongpage.PlaceNameField);

                string searchText = ScrubText(s);

                latlongpage.SetFieldValue(latlongpage.PlaceNameField, searchText);

                latlongpage.ClickItem(latlongpage.FindButton);

                Thread.Sleep(500);
                string latitude = "";
                string longitude = "";
                try
                {
                    IAlert alert = Driver.SwitchTo().Alert();
                    alert.Accept();
                    newBadLines.Add(s);
                }
                catch (NoAlertPresentException)
                {
                    latitude = Driver.FindElement(latlongpage.LatitudeField).GetAttribute("value");
                    longitude = Driver.FindElement(latlongpage.LongitudeField).GetAttribute("value");


                    string newString = s + "," + latitude + "," + longitude;
                    newGoodLines.Add(newString);

                }

            }

            return Tuple.Create(newGoodLines, newBadLines);

        }

        /// <summary>
        /// Scrubs the text.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>System.String.</returns>
        private static string ScrubText(string s)
        {
            if (s.Contains("Ste-"))
            {
                return s.Replace("Ste-", "Sainte-");
            }

            if (s.Contains("St-"))
            {
                return s.Replace("St-", "Saint-");
            }

            if (s.Contains("St. "))
            {
                return s.Replace("St. ", "Saint-");
            }

            return s;
        }

        /// <summary>
        /// Reads the lines from file.
        /// </summary>
        /// <param name="lines">The lines.</param>
        /// <param name="inputFileLocation">The input file location.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        private static List<string> ReadLinesFromFile(List<string> lines, string inputFileLocation)
        {
            using (StreamReader r = new StreamReader(inputFileLocation))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines;
        }
    }
}
