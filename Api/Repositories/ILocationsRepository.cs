using System.Linq;
using Api.Models;
using System.Collections.Generic;

namespace Api.Repositories {
    public interface ILocationsRepository {
        Locations AddLocation(Locations location);
        Locations GetLocation(int postId);
        Posts GetPostsByLocation(int locationId);
        IEnumerable<Locations> LocationSearch(string searchText, int skip = 0, int count = 20);
        Locations RemoveLocation(int postId);
        Locations UpdateLocation(int locationId, string address, string city, string country);
    }
}