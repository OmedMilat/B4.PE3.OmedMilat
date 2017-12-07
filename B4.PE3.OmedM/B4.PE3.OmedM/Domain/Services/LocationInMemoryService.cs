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
        #region MainViewModel

        static List<ListLocation> listLocations = new List<ListLocation>
        {

        };

        public async Task<IEnumerable<ListLocation>> GetAllList()
        {
            await Task.Delay(0);
            return listLocations;
        }
        public async Task AddNewLocationList()
        {
            await Task.Delay(0);
            listLocations.Add(new ListLocation { Id = 2, Locations = new List<Location>(), NameList = "test1" });
        }

        #endregion


        #region LocationViewModel
        static List<Location> InMemLocations = new List<Location>
        {

        };

        public async Task<IEnumerable<Location>> GetAll()
        {
            await Task.Delay(0);
            return InMemLocations;
        }

        //public async Task<ListLocation> GetById(Guid id)
        //{
        //    await Task.Delay(0);
        //    return listLocations.FirstOrDefault(lo => lo.Id == id);
        //}

        public async Task AddNewLocation(string name)
        {
            //var test5 = listLocations.FirstOrDefault(lo => lo.Id == 2);
            //test5.Locations = InMemLocations;

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;

            var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

            InMemLocations.Add(new Location
            {
                Latitude = position.Latitude.ToString(),
                Longitude = position.Longitude.ToString(),
                Name = name,
                GpsTime = DateTime.UtcNow
            });

            var test5 = listLocations.FirstOrDefault(lo => lo.Id == 2);

            test5.Locations = InMemLocations;


        }
        public void Clean()
        {
            if(InMemLocations.Count > 0) { 
            for (int i = InMemLocations.Count - 1; i >= 0; i--)
            {

                InMemLocations.RemoveAt(i);
            }
            }
        }
        #endregion
    }
}
