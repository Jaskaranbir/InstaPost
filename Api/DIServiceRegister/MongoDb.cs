using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DIServiceRegister
{
    public class MongoDb {

        public static void Register(IServiceCollection services, IConfigurationRoot Configuration) {
            string mongoConnection = Configuration.GetValue<string>("ConnectionStrings:InstaPostDB_Mongo");
            IMongoDatabase mongoDB = new MongoClient(mongoConnection).GetDatabase("InstaPost");

            services.AddSingleton<IMongoDatabase>(mongoDB);
        }

    }
}
