using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.DIServiceRegister {
    public class SqlServerDb
    {

        public static void Register(IServiceCollection services, IConfigurationRoot Configuration) {
            string sqlConnection = Configuration.GetValue<string>("ConnectionStrings:InstaPostDB_SQL");
            services.AddDbContext<InstaPostContext>(options => options.UseSqlServer(sqlConnection));
        }
    }
}
