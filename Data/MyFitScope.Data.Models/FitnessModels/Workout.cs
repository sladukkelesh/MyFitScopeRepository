namespace MyFitScope.Data.Models.FitnessModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MyFitScope.Data.Common.Models;
    using MyFitScope.Data.Models.FitnessModels.Enums;

    public class Workout : BaseDeletableModel<string>
    {
        public Workout()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.Users = new HashSet<ApplicationUser>();
            this.WorkoutDays = new HashSet<WorkoutDay>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string CreatorName { get; set; }

        public bool IsCustom { get; set; }

        [Required]
        public Difficulty Difficulty { get; set; }

        [Required]
        public WorkoutType WorkoutType { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public virtual ICollection<WorkoutDay> WorkoutDays { get; set; }
    }
}
