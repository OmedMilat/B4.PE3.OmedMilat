using System.Collections.Generic;
using System.Threading.Tasks;
using B4.PE3.OmedM.Domain.Models;
using Plugin.Geolocator;

namespace B4.PE3.OmedM.Domain.Services
{
    public class LocationInMemoryService
    {
        static List<Location> InMemLocations = new List<Location>
        {
            
        };

        public async Task<IEnumerable<Location>> GetAll()
        {
            await Task.Delay(0);
            return InMemLocations;
        }

        public async Task AddNewLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;
            var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
            InMemLocations.Add(new Location { Latitude = position.Latitude.ToString(), Longitude = position.Longitude.ToString() });
        }
    }
}
