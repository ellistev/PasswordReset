using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Twilio;

namespace PasswordResetWeb.Controllers
{
    public class UnlockAccountController : Controller
    {

        public ActionResult Reset()
        {

            var watch = System.Diagnostics.Stopwatch.StartNew();

            var proc = Process.Start("c:/reset/PollGeographicCoordinates.exe");
            proc.WaitForExit();
            var exitCode = proc.ExitCode;


            // the code that you want to measure comes here
            watch.Stop();
            var elapsedSeconds = watch.ElapsedMilliseconds/1000;

            string AccountSid = "ACe438f581edceafffdb27daacc6da1b87";
            string AuthToken = "412cd77649e095da11c2b0ff6ac2305b";

            // instantiate a new Twilio Rest Client
            var client = new TwilioRestClient(AccountSid, AuthToken);


            // Send a new outgoing SMS by POSTing to the Messages resource */
            client.SendMessage(
                "587-600-0677", // From number, must be an SMS-enabled Twilio number
                "+14039217113",     // To number, if using Sandbox see note above
                                    // message content
                string.Format("Hey Steve, Account Unlocked, in {0} seconds!", elapsedSeconds)
            );

            Response.Write(string.Format("Sent message to Steve"));

            return View("Reset");
        }
    }
}