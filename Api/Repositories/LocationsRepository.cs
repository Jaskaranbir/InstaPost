using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public class LocationsRepository : ILocationsRepository {
        private readonly InstaPostContext db;

        public LocationsRepository(InstaPostContext context) {
            this.db = context;
        }

        public Locations AddLocation(Locations location) {
            db.Locations.Add(location);
            db.SaveChanges();
            return location;
        }

        public Locations UpdateLocation(int locationId, string address, string city, string country) {
            Locations locations = db.Locations.SingleOrDefault(e => e.LocationId == locationId);

            locations.Address = address;
            locations.City = city;
            locations.Country = country;
            db.Update(locations);
            db.SaveChanges();
            return locations;
        }
        
        public Locations GetLocation(int postId) {
            return db.Locations.SingleOrDefault(e => e.PostId == postId);
        }

        public Locations RemoveLocation(int postId) {
            Locations location = new Locations() {
                PostId = postId,
            };

            db.Locations.Attach(location);
            db.SaveChanges();
            return location;
        }

        public Posts GetPostsByLocation(int locationId) {
            return db.Locations.SingleOrDefault(e => e.LocationId == locationId).Post;
        }

        public IEnumerable<Locations> LocationSearch(string searchText, int skip = 0, int count = 20) {
            IEnumerable<Locations> locations = db.Locations.Where(e => 
                e.City.Contains(searchText)
                || e.Country.Contains(searchText)
            )
            .Skip(skip)
            .Take(count);

            return locations;
        }

    }
}
