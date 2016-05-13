
namespace iParty.Services.Entity
{
    public enum PartyCategory
    {
        Love = 1,
        HighendVIP,
    }

    public enum PartyType
    {
        Party,
        ThanksGiving,
    }

    public enum WorkshopType
    {
        Award,
        Normal,
    }

    public enum PartyStage
    {
        OpenForApplication,     //开放
        Created,                //创建
        OpenForInvitation,      //邀约
        OpenForInviteeSignIn,   //签到
        Finished,               //历史
        FeedbackForInvitees,    //反馈
    }

    public enum MaritalStatusType
    {
        Single,
        Married
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

    /// <summary>
    /// 承办方式
    /// </summary>
    public enum OrganizationType
    {
        Standalone,
        Joint
    }

    public struct InvitationStatus
    {
        /// <summary>
        /// Accepted & no status
        /// </summary>
        public const string Accepted = "00";    //已报名
        public const string CheckedIn = "01";   //已签到
        public const string Cancelled = "03";   //已取消
        public const string OnSite = "04";      //现场报名
        public const string OffLine = "05";     //线下补录
    }

    public enum WeCardStatus
    {
        Created,
        CheckedIn,
        Cancelled
    }

    public struct CheckInType
    {
        public const string WechartWithCard = "00";     //卡券 
        public const string Browser = "01";             //浏览器
        public const string Album = "02";               //保存到相册
        public const string Letter = "03";              //intouch发送邀请函
        public const string Intouch = "04";             //intouch
        public const string WechartNoCard = "05";       //微网页
    }
}
