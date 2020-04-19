namespace MyFitScope.Data.Models.FitnessModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyFitScope.Data.Common.Models;

    public class Progress : BaseDeletableModel<string>
    {
        public Progress()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public double Weight { get; set; }

        public double? Biceps { get; set; }

        public double? Chest { get; set; }

        public double? Stomach { get; set; }

        public double? Hips { get; set; }

        public double? Thigh { get; set; }

        public double? Calf { get; set; }
    }
}
