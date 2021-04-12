namespace MyFitScope.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MyFitScope.Data.Models.FitnessModels;

    public class ProgressImageConfiguration : IEntityTypeConfiguration<ProgressImage>
    {
        public void Configure(EntityTypeBuilder<ProgressImage> progressImage)
        {
            progressImage
                .HasOne(pi => pi.User)
                .WithMany(u => u.ProgressImages)
                .HasForeignKey(pi => pi.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
