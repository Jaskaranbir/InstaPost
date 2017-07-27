using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Api.Models
{
    public partial class InstaPostContext : DbContext
    {
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public InstaPostContext(DbContextOptions<InstaPostContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PK__Comments__CDDE919D4FF52FF3");

                entity.Property(e => e.CommentId)
                    .HasColumnName("commentId")
                    .ValueGeneratedNever();

                entity.Property(e => e.CommentDate)
                    .HasColumnName("commentDate")
                    .HasColumnType("date");

                entity.Property(e => e.CommentText)
                    .HasColumnName("commentText")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PostComment");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserComment");
            });

            modelBuilder.Entity<Locations>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__Location__30646B6E8887D181");

                entity.Property(e => e.LocationId)
                    .HasColumnName("locationId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.PostId).HasColumnName("postId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_PostLocation");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserLocation");
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.HasKey(e => e.PostId)
                    .HasName("PK__Posts__DD0C739AB70D7A96");

                entity.Property(e => e.PostId)
                    .HasColumnName("postId")
                    .ValueGeneratedNever();

                entity.Property(e => e.PostDate)
                    .HasColumnName("postDate")
                    .HasColumnType("date");

                entity.Property(e => e.PostImage)
                    .HasColumnName("postImage")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.PostText)
                    .HasColumnName("postText")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Tags)
                    .HasColumnName("tags")
                    .HasColumnType("varchar(max)");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserPost");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__CB9A1CFFCD2A278B");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.FirstName)
                    .HasColumnName("firstName")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IsSuspended).HasColumnName("isSuspended");

                entity.Property(e => e.LastName)
                    .HasColumnName("lastName")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.ProfilePicture)
                    .HasColumnName("profilePicture")
                    .HasColumnType("varchar(100)");
            });
        }
    }
}