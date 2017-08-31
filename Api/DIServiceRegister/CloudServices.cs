using Api.Models.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.DIServiceRegister {
    public class CloudServices {

        public static void Register(IServiceCollection services, IConfigurationRoot config) {
            services.Configure<Auth0Config>(config.GetSection("Auth0"));
            services.Configure<CloudinaryConfig>(config.GetSection("Cloudinary"));
        }
    }
}
