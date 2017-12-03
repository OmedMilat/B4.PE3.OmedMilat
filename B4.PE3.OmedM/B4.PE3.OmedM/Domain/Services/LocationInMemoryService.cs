using System.Collections.Generic;
using System.Threading.Tasks;
using B4.PE3.OmedM.Domain.Models;
using Plugin.Geolocator;
using System;
using System.Linq;

namespace B4.PE3.OmedM.Domain.Services
{
    public class LocationInMemoryService
    {
        static List<Location> InMemLocations = new List<Location>
        {
            new Location{Name="hohoh"}
        };
        static List<ListLocation> listLocations = new List<ListLocation>
        {

        };

        public async Task<IEnumerable<Location>> GetAll()
        {
            await Task.Delay(0);
            return InMemLocations;
        }

        public async Task<Location> GetById(Guid id)
        {
            await Task.Delay(0);
            return InMemLocations.FirstOrDefault(lo => lo.Id == id);
        }

        public async Task AddNewLocation(string name)
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;

            var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
            InMemLocations.Add(
                new Location
                {
                    Latitude = position.Latitude.ToString(),
                    Longitude = position.Longitude.ToString(),
                    Name = name,
                    GpsTime = DateTime.UtcNow
                });

        }

        public async Task AddNewLocationList()
        {
            await Task.Delay(0);
             listLocations.Add(new ListLocation { Listlocations = InMemLocations });
        }
    }
}
