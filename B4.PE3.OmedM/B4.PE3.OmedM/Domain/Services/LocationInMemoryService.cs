using System.Collections.Generic;
using System.Threading.Tasks;
using B4.PE3.OmedM.Domain.Models;
using Plugin.Geolocator;
using System;
using System.Linq;
using System.IO;
using System.Xml;
using PCLStorage;
using System.Xml.Serialization;
using System.Diagnostics;

namespace B4.PE3.OmedM.Domain.Services
{
    public class LocationInMemoryService
    {
        static ListLocation AddNewListLocation;
        static SaveLocation SaveLocation;
        #region MainViewModel

        static List<ListLocation> listLocations = new List<ListLocation>
        {           
        };

        public async Task<IEnumerable<ListLocation>> GetAllList()
        {
            await Task.Delay(0);
            return listLocations;
        }
        public async Task LoadLocations()
        {
            string fileName = "settings.xml";
            IFolder folder = FileSystem.Current.LocalStorage;
            ExistenceCheckResult result = await folder.CheckExistsAsync(fileName);
            if (result == ExistenceCheckResult.FileExists)
            {
                try
                {
                    IFile file = await folder.GetFileAsync(fileName);
                    string text = await file.ReadAllTextAsync();
                    using (var reader = new StringReader(text))
                    {
                        var serializer = new XmlSerializer(typeof(SaveLocation));
                        SaveLocation settings = (SaveLocation)serializer.Deserialize(reader);
                        for (int i = 0; i < settings.Savelocation.Count; i++)
                        { 
                            listLocations.Add(new ListLocation { Id = settings.Savelocation[i].Id,
                            Locations = settings.Savelocation[i].Locations, NameList = settings.Savelocation[i].NameList });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error reading settings: {ex.Message}");
                }
            }
        }

        public async Task SaveLocations()
        {
            var serializer = new XmlSerializer(typeof(SaveLocation));
            string settingsAsXml = "";
            using (var stringWriter = new StringWriter())
            using (var writer = XmlWriter.Create(stringWriter))
            {
                SaveLocation = new SaveLocation
                {
                    Savelocation = listLocations
                };
                serializer.Serialize(writer, SaveLocation);
                settingsAsXml = stringWriter.ToString();
            }

            IFolder folder = FileSystem.Current.LocalStorage;
            IFile file = await folder.CreateFileAsync("settings.xml",
                CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(settingsAsXml);
        }

        public async Task AddNewLocationList(string nameList)
        {
            AddNewListLocation = new ListLocation();
            await Task.Delay(0);
            AddNewListLocation.Id = Guid.NewGuid();
            AddNewListLocation.NameList = nameList;
            AddNewListLocation.Locations = new List<Location>();
            listLocations.Add(AddNewListLocation);
            await SaveLocations();
        }
        #endregion


        #region LocationViewModel
        static List<Location> InMemLocations = new List<Location>
        {     
        };

        public async Task<IEnumerable<Location>> GetAll(ListLocation listLocation)
        {
            if (listLocation != null)
            {
                Clean();
                var test = await GetById(listLocation.Id);
                for (int i = 0; i < test.Locations.Count; i++)
                    InMemLocations.Add(test.Locations[i]);
            }
            await Task.Delay(0);
            return InMemLocations;
        }

        public async Task<ListLocation> GetById(Guid id)
        {
            await Task.Delay(0);
            return listLocations.FirstOrDefault(lo => lo.Id == id);
        }

        public async Task AddNewLocation(string name, ListLocation listLocation)
        {
            if (listLocation == null)
            {
                listLocation = AddNewListLocation;
            }
            var list = await GetById(listLocation.Id);

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;

            var position = await locator.GetPositionAsync(timeoutMilliseconds: 15000);

            InMemLocations.Add(new Location
            {
                Latitude = position.Latitude.ToString(),
                Longitude = position.Longitude.ToString(),
                Name = name,
                GpsTime = DateTime.UtcNow
            });

            list.Locations.Add(InMemLocations[InMemLocations.Count - 1]);

            await SaveLocations();
        }

        public void Clean()
        {
            if (InMemLocations.Count > 0)
            {
                for (int i = InMemLocations.Count - 1; i >= 0; i--)
                {
                    InMemLocations.RemoveAt(i);
                }
            }
        }
        #endregion
    }
}
