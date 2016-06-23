using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace PasswordResetWeb.Controllers
{
    public class UnlockAccountController : Controller
    {

        public ActionResult Reset()
        {

            var proc = Process.Start("../../PasswordReset/bin/debug/PollGeographicCoordinates.exe");
            proc.WaitForExit();
            var exitCode = proc.ExitCode;

            return View("index");
        }
    }
}