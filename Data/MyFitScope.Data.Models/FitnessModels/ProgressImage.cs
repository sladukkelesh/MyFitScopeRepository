namespace MyFitScope.Data.Models.FitnessModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyFitScope.Data.Common.Models;

    public class ProgressImage : BaseDeletableModel<string>
    {
        public ProgressImage()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public string PublicId { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
