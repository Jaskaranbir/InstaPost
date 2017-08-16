using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class AdministratorsRepository : IAdministratorsRepository {
        private readonly InstaPostContext db;

        public AdministratorsRepository(InstaPostContext context) {
            this.db = context;
        }

		//Adds a new Administrator to Administrators Table
        public void AddAdminUser(int userId) {
            Administrators admin = new Administrators() {
                UserId = userId
            };
            db.Administrators.Add(admin);
        }

		//Returns a user basedon their adminID
        public Users GetAdminUser(int adminId) {
            Users user = db.Administrators.SingleOrDefault(e => e.AdministratorId == adminId).User;
            return user;
        }

		//Returns a boolean to check whether or not user is an admin
        public bool IsUserAdmin(int userId) {
            Administrators admin = db.Administrators.SingleOrDefault(e => e.UserId == userId);
            return admin != null;
        }

		//Remove an administrator from Administrators Table
        public void RemoveAdminUser(int userId) {
            Administrators admin = db.Administrators.SingleOrDefault(e => e.UserId == userId);
            db.Administrators.Remove(admin);
        }

        public void SaveChanges() {
            db.SaveChanges();
        }
    }
}
