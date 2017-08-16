using Api.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Api.DIServiceRegister {
    public class ModelRepositories
    {
        //Dependency Injection to allow to achieve loose coupling between the objects below and their appropriate dependencies
        public static void Register(IServiceCollection services) {
            services.AddScoped<IAdministratorsRepository, AdministratorsRepository>();
            services.AddScoped<IBookmarksRepository, BookmarksRepository>();
            services.AddScoped<ICommentsRepository, CommentsRepository>();
            services.AddScoped<IFollowersRepository, FollowersRepository>();
            services.AddScoped<ILikesRepository, LikesRepository>();
            services.AddScoped<ILocationsRepository, LocationsRepository>();
            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddScoped<IReportsRepository, ReportsRepository>();
            services.AddScoped<ITagsRepository, TagsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
        }

    } 
}
