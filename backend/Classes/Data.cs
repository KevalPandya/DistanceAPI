using backend.Models;
using Newtonsoft.Json;

namespace backend.Classes
{
    public static class Data
    {
        public static IEnumerable<Province> GetProvinces()
        {
            using (var reader = new StreamReader("Data/provinces.json"))
                return JsonConvert.DeserializeObject<List<Province>>(reader.ReadToEnd());
        }

        public static IEnumerable<Site> GetSites()
        {
            using (var reader = new StreamReader("Data/sites.json"))
                return JsonConvert.DeserializeObject<List<Site>>(reader.ReadToEnd());
        }

        public static IEnumerable<Driver> GetDrivers()
        {
            using (var reader = new StreamReader("Data/drivers.json"))
                return JsonConvert.DeserializeObject<List<Driver>>(reader.ReadToEnd());
        }

        public static IEnumerable<Distance> GetDistances()
        {
            using (var reader = new StreamReader("Data/distances.json"))
                return JsonConvert.DeserializeObject<List<Distance>>(reader.ReadToEnd());
        }
    }
}