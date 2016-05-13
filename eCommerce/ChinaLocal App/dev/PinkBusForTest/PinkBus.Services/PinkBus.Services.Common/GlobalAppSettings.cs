using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkBus.Services.Common
{
    public class GlobalAppSettings
    {
        public static string ContactsLite = ConfigurationManager.AppSettings["ContactsLite"];
        public static string Community = ConfigurationManager.AppSettings["Community"];
        public static string Contacts = ConfigurationManager.AppSettings["Contacts"];
        public static string EnableOAuthATValidation = ConfigurationManager.AppSettings["EnableOAuthATValidation"];
        public static string OAuthServiceURL = ConfigurationManager.AppSettings["OAuthServiceURL"];
        public static string ValidateCommandServiceURL = ConfigurationManager.AppSettings["ValidateCommandServiceURL"];
        public static string EnableCommandContextValidation = ConfigurationManager.AppSettings["EnableCommandContextValidation"];

        public static string Municipalities = ConfigurationManager.AppSettings["Municipalities"];

        public static string SharedUrl = ConfigurationManager.AppSettings["SharedUrl"];
        public static string PhoneNumberType = ConfigurationManager.AppSettings["PhoneNumberType"];

        public static string TicketUrlSchema = ConfigurationManager.AppSettings["TicketUrlSchema"];

        public static string ConsultantLevel_ErrorMessage = ConfigurationManager.AppSettings["ConsultantLevelErrorMessage"];
        public static string DuplicatePhoneNumer_ErrorMessage = ConfigurationManager.AppSettings["DuplicatePhoneNumerErrorMessage"];

        public static string DirectSellerIdFailed_BestowalTicket_ErrorMessage = ConfigurationManager.AppSettings["DirectSellerIdFailed_BestowalTicket"];
        public static string ConsnultantLevelFailed_BestowalTicket_ErrorMessage = ConfigurationManager.AppSettings["CosnultantLevelFailed_BestowalTicket"];

        public static string InvitatedCustomer_Intouch_SMSTemplate = ConfigurationManager.AppSettings["InvitatedCustomer_Intouch_SMSTemplate"];
        public static string CancelTicket_Intouch_SMSTemplate = ConfigurationManager.AppSettings["CancelTicket_Intouch_SMSTemplate"];


    }
}
