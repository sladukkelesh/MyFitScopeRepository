﻿// ReSharper disable VirtualMemberCallInConstructor
namespace MyFitScope.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    using MyFitScope.Data.Common.Models;
    using MyFitScope.Data.Models.BlogModels;
    using MyFitScope.Data.Models.FitnessModels;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Progresses = new HashSet<Progress>();
            this.ProgressImages = new HashSet<ProgressImage>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string AvatarImageUrl { get; set; }

        public string AvatarImagePublicId { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string WorkoutId { get; set; }

        public virtual Workout Workout { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Progress> Progresses { get; set; }

        public virtual ICollection<ProgressImage> ProgressImages { get; set; }
    }
}
