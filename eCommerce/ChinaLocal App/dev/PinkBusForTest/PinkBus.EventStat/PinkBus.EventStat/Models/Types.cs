using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinkBus.EventStat.Models
{

    #region enum about Customer

    public enum CustomerType
    {
        Old,
        New,
        VIP
    }

    public enum OfflineCustomerType
    {
        Old,
        New,
        Student
    }



    public enum AgeRange
    {
        Blow25,
        Between25And35,
        Between35And45,
        Above45
    }

    public enum AgeRangeOffline
    {
        Bellow25,
        Between2535,
        Between3545,
        Above45
    }

    public enum Career
    {
        Clerk,
        PrivateOwner,
        Housewife,
        Freelancers
    }

    public enum InterestingTopic
    {
        SkinCare,
        MakeUp,
        DressUp,
        FamilyTies
    }


    public enum UsedSet
    {
        TimeWise,  //幻时／幻时佳
        WhiteningSystemFoaming,  //美白
        Cleanser,//经典
        CalmingInfluence,//舒颜
        Other//其他系列
    }


    public enum InterestInCompany
    {
        BeautyConfidence,  //美丽自信
        CompanyCulture,  //公司文化
        BusinessOpportunity,//事业机会
        Other//其他
    }

    #endregion


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
            Invited,  //2
            Bestowed,
            Canceled, //4
            Checkin,  //5
            UnCheckin,  //6
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