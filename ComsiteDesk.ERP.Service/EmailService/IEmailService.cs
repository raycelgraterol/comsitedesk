using System;
using System.Collections.Generic;
using System.Text;

namespace ComsiteDesk.ERP.Service
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html, string from = "helpdesk@comsite.com.ve");
    }
}
