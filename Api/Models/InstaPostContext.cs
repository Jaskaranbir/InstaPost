using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Api.Models
{
    public partial class InstaPostContext : DbContext
    {
        public virtual DbSet<Administrators> Administrators { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public InstaPostContext(DbContextOptions<InstaPostContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrators>(entity =>
            {
                entity.HasKey(e => e.AdministratorId)
                    .HasName("PK_Administrators_administratorId");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_Administrators_userId");

                entity.Property(e => e.AdministratorId)
                    .HasColumnName("administratorId")
                    .ValueGeneratedNever();

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Administrators)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Administrators_userId");
            });

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PK_Comments_commentId");

                entity.HasIndex(e => new { e.CommentDate, e.CommentTime })
                    .HasName("IX_Comments_commentDate_commentTime");

                entity.HasIndex(e => new { e.PostId, e.CommentDate, e.CommentTime })
                    .HasName("IX_Comments_postId_commentDate_commentTime");

                entity.Property(e => e.CommentId)
                    .HasColumnName("commentId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CommentDate)
                    .HasColumnName("commentDate")
                    .HasColumnType("date");

                entity.Property(e => e.CommentText)
                    .IsRequired()
                    .HasColumnName("commentText")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.CommentTime).HasColumnName("commentTime");

                entity.Property(e => e.HasChildComments)
                    .HasColumnName("hasChildComments")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.ParentCommentId).HasColumnName("parentCommentId");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Comments_postId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Comments_userId");
            });

            modelBuilder.Entity<Locations>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK_Locations_locationId");

                entity.HasIndex(e => e.PostId)
                    .HasName("UQ__Location__DD0C739BE2078527")
                    .IsUnique();

                entity.Property(e => e.LocationId)
                    .HasColumnName("locationId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Post)
                    .WithOne(p => p.Locations)
                    .HasForeignKey<Locations>(d => d.PostId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Locations_postId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Locations_userId");
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.HasKey(e => e.PostId)
                    .HasName("PK_Post_postId");

                entity.HasIndex(e => new { e.PostDate, e.PostTime })
                    .HasName("IX_Posts_postDate_postTime");

                entity.Property(e => e.PostId)
                    .HasColumnName("postId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CommentsCount)
                    .HasColumnName("commentsCount")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.LikesCount)
                    .HasColumnName("likesCount")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.PostDate)
                    .HasColumnName("postDate")
                    .HasColumnType("date");

                entity.Property(e => e.PostImage)
                    .HasColumnName("postImage")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.PostText)
                    .HasColumnName("postText")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.PostTime).HasColumnName("postTime");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Post_userId");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_User_userId");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Users__AB6E61649A1FC5CE")
                    .IsUnique();

                entity.HasIndex(e => e.Usertag)
                    .HasName("UQ__Users__79DFBA16B56A9F80")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IsSuspended)
                    .HasColumnName("isSuspended")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.ProfilePicture)
                    .HasColumnName("profilePicture")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Usertag)
                    .IsRequired()
                    .HasColumnName("usertag")
                    .HasColumnType("varchar(20)");
            });
        }
    }
}