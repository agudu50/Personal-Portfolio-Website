using Personal_Portfolio_Website.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace Personal_Portfolio_Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() => View();

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View(); // ✅ Make sure this only returns the View
        }

        // ✅ MOVE THIS OUTSIDE of the Contact() method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendEmail(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fromAddress = new MailAddress("devtony2020@gmail.com", "TonyTech Portfolio");
                    var toAddress = new MailAddress("devtony2020@gmail.com", "Anthony Gudu");

                    string subject = model.Subject;
                    string body = $"Name: {model.Name}\nEmail: {model.Email}\n\nMessage:\n{model.Message}";

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential("devtony2020@gmail.com", "egpxykpwcxwwbvad") // ✅ use a valid app password
                    };

                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }

                    TempData["Success"] = "Your message has been sent successfully!";
                    return RedirectToAction("Contact");
                }
                catch (Exception ex)
                {
                    // ❌ FIX: use TempData instead of ViewBag
                    TempData["Error"] = "Failed to send email: " + ex.Message;
                    return RedirectToAction("Contact");
                }
            }

            ModelState.AddModelError("", "Please complete all required fields correctly.");
            return View("Contact", model); // Pass model with errors back to the view

        }




        public ActionResult Projects() => View();
        public ActionResult Resume() => View();
        public ActionResult Skills() => View();
        public ActionResult Services() => View();
    }
}
