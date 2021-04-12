namespace MyFitScope.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using MyFitScope.Data.Models.BlogModels;

    public class ResponseConfiguration : IEntityTypeConfiguration<Response>
    {
        public void Configure(EntityTypeBuilder<Response> response)
        {
            response
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            response
                .HasOne(r => r.Article)
                .WithMany()
                .HasForeignKey(r => r.ArticleId)
                .OnDelete(DeleteBehavior.Restrict);

            response
                .HasOne(r => r.Comment)
                .WithMany(c => c.Responses)
                .HasForeignKey(r => r.CommentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
