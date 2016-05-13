using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinkBus.EventManagement.Models
{
   
        /// <summary>
        /// EventUserType , VolunteerBC must be the last one
        /// </summary>
        public enum EventUserType
        {
            NormalBC,
            BestowalBC,
            VIPBC,
            VolunteerBC
        }

        public enum TicketType
        {
            VIP,
            Normal
        }

        public enum TicketStatus
        {
            Created,
            Inviting,
            Invited,
            Bestowed,
            Canceled,
            Checkin,
            UnCheckin,
            Expired
        }

        public enum EventStage
        {
            OpenForCountDown,
            OpenForScrambleTicket,
            OpenForInvitation,
            OpenForDownloadData,
            OpenForOffline,
            OpenForHistory
        }

        public enum ApplyTicketResult
        {
            Unknown,
            Success,
            Fail
        }

        public enum TicketFrom
        {
            Import,
            Apply,
            Bestowal,
            Rebuild
        }
    
}