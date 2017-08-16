using Api.Models;
using Api.MongoWrappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DIServiceRegister
{
    public class MongoDbSets
    {
		//Creating Singletons of each DB to allow for only one object to be instantiated throughout entire system
        public static void Register(IServiceCollection services) {
            services.AddSingleton<IMongoDbSet<Bookmarks>, MongoDbSet<Bookmarks>>();
            services.AddSingleton<IMongoDbSet<Followers>, MongoDbSet<Followers>>();
            services.AddSingleton<IMongoDbSet<Likes>, MongoDbSet<Likes>>();
            services.AddSingleton<IMongoDbSet<Reports>, MongoDbSet<Reports>>();
            services.AddSingleton<IMongoDbSet<Tags>, MongoDbSet<Tags>>();
        }

    }
}
