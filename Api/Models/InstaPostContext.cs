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
                entity.HasKey(e => e.Commentid)
                    .HasName("PK__Comments__CDA84BC5DDA91D39");

                entity.Property(e => e.Commentid)
                    .HasColumnName("commentid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Likeid).HasColumnName("likeid");

                entity.Property(e => e.Postid).HasColumnName("postid");

                entity.Property(e => e.Text)
                    .HasColumnName("text")
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Postid)
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
                entity.HasKey(e => e.Locationid)
                    .HasName("PK__Location__306F78A672D744BA");

                entity.Property(e => e.Locationid)
                    .HasColumnName("locationid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address).HasColumnType("varchar(50)");

                entity.Property(e => e.City).HasColumnType("varchar(50)");

                entity.Property(e => e.Country).HasColumnType("varchar(50)");

                entity.Property(e => e.Postid).HasColumnName("postid");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.Postid)
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
                entity.HasKey(e => e.Postid)
                    .HasName("PK__Posts__DD017FD239BBFC94");

                entity.Property(e => e.Postid)
                    .HasColumnName("postid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("date");

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Tags)
                    .HasColumnName("tags")
                    .HasColumnType("text");

                entity.Property(e => e.Text)
                    .HasColumnName("text")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserPost");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__1788CCACED8C3C12");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Emails)
                    .HasColumnName("emails")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.FirstName).HasColumnType("varchar(50)");

                entity.Property(e => e.LastName).HasColumnType("varchar(50)");

                entity.Property(e => e.Passwords)
                    .HasColumnName("passwords")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.ProfilePicture).HasColumnType("varchar(100)");

                entity.Property(e => e.Suspended)
                    .HasColumnName("suspended")
                    .HasColumnType("varchar(50)");
            });
        }
    }
}