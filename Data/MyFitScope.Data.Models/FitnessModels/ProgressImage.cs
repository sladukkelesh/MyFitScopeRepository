namespace MyFitScope.Data.Models.FitnessModels
{
    using System;

    using MyFitScope.Data.Common.Models;

    public class ProgressImage : BaseDeletableModel<string>
    {
        public ProgressImage()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string PublicId { get; set; }

        public string Url { get; set; }
    }
}
