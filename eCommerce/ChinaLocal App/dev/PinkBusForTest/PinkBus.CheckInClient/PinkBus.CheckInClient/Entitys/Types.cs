using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinkBus.CheckInClient.Entitys
{
    public enum CustomerType
    {
        Old,
        New,
        VIP
    }

    public enum AgeRange
    {
        Blow25,
        Between25And35,
        Between35And45,
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
        TimeWise, //幻时／幻时佳
        WhiteningSystemFoaming, //美白
        Cleanser,//经典
        CalmingInfluence,//舒颜
        Other//其他系列
    }

    public enum InterestInCompany
    {
        BeautyConfidence, //美丽自信
        CompanyCulture, //公司文化
        BusinessOpportunity,//事业机会
        Other//其他
    }




    public enum Source
    {
        Wechat,
        MobileBrowser,
        IntouchClient,
        WinClient
    }


    /// <summary>
    /// 打开页面的用户身份
    /// </summary>

    public enum WechatUserType
    {
        Inviter,
        Customer,
        Other
    }



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
        Normal,
        Volunteer
    }


    public enum TicketStatus
    {
        Created,  //0
        Inviting,  //1
        Invited,   //2
        Bestowed,  //3
        Canceled,   //4
        Checkin,   //5
        UnCheckin,   //6
        Expired   //7
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
        Rebuild,
    }

    public enum SyncType
    {
        D, //Download,
        U  //Upload
    }

    public enum SyncStatus
    {
        Downloading,
        Downloaded,
        Uploading,
        Uploaded,
        None,

    }

    //public enum VolunteerStatus
    //{
    //    Normal,
    //    Canceled,
    //    Checkin
    //}

    public enum ConsultantStatus
    {
        NotApply,//for apply BC
        Applied, //for apply BC

        Canceled, //for volunteer
        CheckedIn //for volunteer
    }

}
