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
#region FirstPage

        static List<ListLocation> listLocations = new List<ListLocation>
        {
           new ListLocation{Locations=InMemLocations,NameList="hoho"}
        };

        public async Task<IEnumerable<ListLocation>> GetAllList()
        {
            await Task.Delay(0);
            return listLocations;
        }
        public async Task AddNewLocationList()
        {
            await Task.Delay(0);
             listLocations.Add(new ListLocation { Locations = new List<Location>{ }, NameList="test1" });
        }

        #endregion


        #region MainViewModel
        //InMemLocations = niks waard
        static List<Location> InMemLocations = new List<Location>
        {
            new Location{Name="hohoh"}
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
            listLocations.Add( new ListLocation
            {
                Locations = new List<Location> {
                new Location
                {
                    Latitude = position.Latitude.ToString(),
                    Longitude = position.Longitude.ToString(),
                    Name = name,
                    GpsTime = DateTime.UtcNow
                }
            }
            });

        }
        #endregion

        
    }
}
