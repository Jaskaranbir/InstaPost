using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MongoDB.Driver;
using Api.MongoWrappers;

namespace Api.Models {
	//Instantiates the context of the application and loads all relevant data
    public partial class InstaPostContext : DbContext {

        public virtual DbSet<Administrators> Administrators { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public IMongoDbSet<Bookmarks> Bookmarks { get; set; }
        public IMongoDbSet<Followers> Followers { get; set; }
        public IMongoDbSet<Likes> Likes { get; set; }
        public IMongoDbSet<Reports> Reports { get; set; }
        public IMongoDbSet<Tags> Tags { get; set; }


        public InstaPostContext(
            DbContextOptions<InstaPostContext> options,
            IMongoDbSet<Bookmarks> Bookmarks,
            IMongoDbSet<Followers> Followers,
            IMongoDbSet<Likes> Likes,
            IMongoDbSet<Reports> Reports,
            IMongoDbSet<Tags> Tags
        ) : base(options) {
            this.Bookmarks = Bookmarks;
            this.Followers = Followers;
            this.Likes = Likes;
            this.Reports = Reports;
            this.Tags = Tags;
        }
		
		//Makes sure administrator has appropriate unique adminID, userID, and authority over users
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Administrators>(entity => {
                entity.HasKey(e => e.AdministratorId)
                    .HasName("PK_Administrators_administratorId");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_Administrators_userId");

                entity.Property(e => e.AdministratorId).HasColumnName("administratorId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Administrators)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Administrators_userId");
            });
			
			//Makes sure each comment has appropriate unique commentID, comment time and date and comment text
			//Handles whether comment is parent or child. 
			//Comment must have postID(associated post) and userID(associated user)
            modelBuilder.Entity<Comments>(entity => {
                entity.HasKey(e => e.CommentId)
                    .HasName("PK_Comments_commentId");

                entity.HasIndex(e => new { e.CommentDate, e.CommentTime })
                    .HasName("IX_Comments_commentDate_commentTime");

                entity.HasIndex(e => new { e.PostId, e.CommentDate, e.CommentTime })
                    .HasName("IX_Comments_postId_commentDate_commentTime");

                entity.Property(e => e.CommentId).HasColumnName("commentId");

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

                entity.HasOne(d => d.ParentComment)
                    .WithMany(p => p.InverseParentComment)
                    .HasForeignKey(d => d.ParentCommentId)
                    .HasConstraintName("FK_Comments_parentCommentId");

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

			//Location is made up of locationID, name, address, city, country. 
			//Location is associated with a user(ID) and post(ID) 
            modelBuilder.Entity<Locations>(entity => {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK_Locations_locationId");

                entity.HasIndex(e => e.PostId)
                    .HasName("UQ__Location__DD0C739BF8DF4822")
                    .IsUnique();

                entity.Property(e => e.LocationId).HasColumnName("locationId");

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

			//Post must have postID, date and time. 
			//Post starts with 0 comments, likes by default 
            modelBuilder.Entity<Posts>(entity => {
                entity.HasKey(e => e.PostId)
                    .HasName("PK_Post_postId");

                entity.HasIndex(e => new { e.PostDate, e.PostTime })
                    .HasName("IX_Posts_postDate_postTime");

                entity.Property(e => e.PostId).HasColumnName("postId");

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

			//Users must have a unique primary key, being that 2 users with the same name may occur
			//Must have unique email, usertag
            modelBuilder.Entity<Users>(entity => {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_User_userId");

                entity.HasIndex(e => e.Usertag)
                    .HasName("UQ__Users__79DFBA16EF2068A6")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("userId");

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