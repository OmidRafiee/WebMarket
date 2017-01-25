﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models.EntityModels
{
    public class Setting
    {
        public int Id { get; set; }
        public string ContactUs { get; set; }
        public string AboutUs { get; set; }
        public string Help { get; set; }
        public string PrivacyPolicy { get; set; }
        public string Title { get; set; }
        public string KeyWord { get; set; }
        public string Description { get; set; }
        public string SmtpServer { get; set; }
        public string Email { get; set; }
        public string EmailPassword { get; set; }
        public string EmailSenderName { get; set; }
        public string Picture1 { get; set; }
        public string Picture2 { get; set; }
        public string Picture3 { get; set; }
        public string Picture4 { get; set; }
        public string Picture5 { get; set; }
        public string Picture6 { get; set; }
    }
}