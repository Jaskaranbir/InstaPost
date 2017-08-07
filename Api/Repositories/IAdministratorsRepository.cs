using Api.Models;

namespace Api.Repositories {
    public interface IAdministratorsRepository {
        void AddAdminUser(int userId);
        Users GetAdminUser(int adminId);
        bool IsUserAdmin(int userId);
        void RemoveAdminUser(int userId);
        void SaveChanges();
    }
}