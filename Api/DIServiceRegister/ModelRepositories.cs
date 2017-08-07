using Api.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Api.DIServiceRegister {
    public class Repositories
    {

        public static void Register(IServiceCollection services) {
            services.AddSingleton<IAdministratorsRepository, AdministratorsRepository>();
            services.AddSingleton<IBookmarksRepository, BookmarksRepository>();
            services.AddSingleton<ICommentsRepository, CommentsRepository>();
            services.AddSingleton<IFollowersRepository, FollowersRepository>();
            services.AddSingleton<ILikesRepository, LikesRepository>();
            services.AddSingleton<ILocationsRepository, LocationsRepository>();
            services.AddSingleton<IPostsRepository, IPostsRepository>();
            services.AddSingleton<IReportsRepository, ReportsRepository>();
            services.AddSingleton<ITagsRepository, TagsRepository>();
        }

    }
}
