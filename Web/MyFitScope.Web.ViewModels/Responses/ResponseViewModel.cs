namespace MyFitScope.Web.ViewModels
{
    using System;

    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Services.Mapping;

    public class ResponseViewModel : IMapFrom<Response>
    {
        public string UserUserName { get; set; }

        public string Content { get; set; }

        public int PositiveVotes { get; set; }

        public int NegativeVotes { get; set; }

        public DateTime CreatedOn { get; set; }

        public string OutputDate
            => this.CreatedOn.Year + "-" + this.CreatedOn.Month + "-" + this.CreatedOn.Day;
    }
}
