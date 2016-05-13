using ServiceStack;

namespace iParty.Services.Contract
{
    public class Meta
    {
        public long? Total { get; set; }
        public int? Limit { get; set; }
        public long? Offset { get; set; }
    }

    public abstract class BaseRequestDto
    {
        public string _SubsidiaryCode { get; set; }
        public string _ClientKey { get; set; }
        public string _ClientIdentity { get; set; }
        public string _UserName { get; set; }
        public string _Culture { get; set; }
        public string _UICulture { get; set; }
        public string _Timezone { get; set; }
    }

    public abstract class BaseResponseDto
    {
        public ResponseStatus ResponseStatus { get; set; }
    }

    public abstract class CommandRequestDto : BaseRequestDto
    {
        public CommandRequestDto() : base() { }
    }

    public abstract class CommandResponseDto : BaseResponseDto
    {
        public CommandResponseDto() : base() { }
    }

    public abstract class QueryRequestDto : BaseRequestDto
    {
        [ApiMember(Name = "Fields", ParameterType = "query", Description = "indicate a partial return or association, e.g. /customers?fields=Name,PhoneNumber will return a objects of only fileds of Name and PhoneNumber", DataType = "string", IsRequired = false)]
        public string Fields { get; set; }

        [ApiMember(Name = "Sort", ParameterType = "query", Description = "indicate sorting order, e.g. ?sort=-Name, -Name means order by Name descendingly", DataType = "string", IsRequired = false)]
        public string Sort { get; set; }

        [ApiMember(Name = "Q", ParameterType = "query", Description = "indicate full text search, e.g. ?q=mary+13899998888 will apply full text search and return only records containing string mary and 13899998888", DataType = "string", IsRequired = false)]
        public string Q { get; set; }

        [ApiMember(Name = "Limit", ParameterType = "query", Description = "indicate pagination, e.g. ?limit=20&offset=100", DataType = "int", IsRequired = false)]
        public int Limit { get; set; }

        [ApiMember(Name = "Offset", ParameterType = "query", Description = "indicate pagination, e.g. ?limit=20&offset=100", DataType = "long", IsRequired = false)]
        public long Offset { get; set; }

        public QueryRequestDto()
            : base()
        {
            Offset = 0;
            Limit = 20;
        }
    }

    public abstract class QueryResponseDto : BaseResponseDto
    {
        public QueryResponseDto() : base() { }

        public Meta _Meta { get; set; }
    }

    public interface IPublicRequestDto { }
}
