using GlobusMailLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Api.Myfashionmarketer.Helper
{
    public class ClsMailHelper
    {
        public string usernameSend = string.Empty;
        public string pass = string.Empty;
        public string SendMail(string name, string company,string lname, string email, string Subject, string profile)
        {

            MailSenderFactory objMailSenderFactory = null;
            string ret = string.Empty;
            string tomail = ConfigurationManager.AppSettings["tomail"];

            objMailSenderFactory = MailHelper();
            string subject = Subject;
            string Body = "FirstName: " + name + "</br>" + "LastName:" + lname + "</br>" + "Email: " + email + "</br>" + "Subject:" + Subject + "</br>" + "Message: " + profile + "</br>";



            ret = objMailSenderFactory.GetMailSenderInstance().SendMail(tomail, "", tomail, "", "", subject, Body, usernameSend, pass);

            return ret;

        }


        public MailSenderFactory MailHelper()
        {
            MailSenderFactory objMailSenderFactory = null;
            string mailtype = ConfigurationManager.AppSettings["MailType"];

            if (mailtype == "Gmail")
            {
                usernameSend = ConfigurationManager.AppSettings["GoogleUserName"];
                pass = ConfigurationManager.AppSettings["GooglePassword"];
                objMailSenderFactory = new MailSenderFactory(MailSendingType.Gmail);
            }
            else if (mailtype == "Mandrill")
            {
                usernameSend = ConfigurationManager.AppSettings["Mandrillusername"];
                pass = ConfigurationManager.AppSettings["Mandrillpassword"];
                objMailSenderFactory = new MailSenderFactory(MailSendingType.Mandrill);
            }
            else if (mailtype == "Sendgrid")
            {
                usernameSend = ConfigurationManager.AppSettings["GendgridUserName"];
                pass = ConfigurationManager.AppSettings["GendgridPassword"];
                objMailSenderFactory = new MailSenderFactory(MailSendingType.Sendgrid);
            }
            return objMailSenderFactory;
        }
    }
}