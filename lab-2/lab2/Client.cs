using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace Lab2
{
    public class Client
    {
        public async Task<List<Master>> GetMasters()
        {
            return JsonConvert.DeserializeObject<List<Master>>(await new HttpClient().GetStringAsync($"http://localhost:8080/masters"));
        }

        public async Task<Master> GetMaster(long id)
        {
            return JsonConvert.DeserializeObject<Master>(await new HttpClient().GetStringAsync($"http://localhost:8080/getMaster/{id}"));
        }

        public async Task<HttpResponseMessage> CreateMaster(Master newMaster)
        {
            return await new HttpClient().PostAsync($"http://localhost:8080/createMaster", new StringContent(JsonConvert.SerializeObject(newMaster), Encoding.UTF8, "application/json"));
        }

        public async Task<Cat> GetCat(long id)
        {
            return JsonConvert.DeserializeObject<Cat>(await new HttpClient().GetStringAsync($"http://localhost:8080/getCat/{id}"));
        }

        public async Task<HttpResponseMessage> CreateCat(Cat cat)
        {
            return await new HttpClient().PostAsync($"http://localhost:8080/createCat", new StringContent(JsonConvert.SerializeObject(cat), Encoding.UTF8, "application/json"));
        }

        public async Task<HttpResponseMessage> SetFriends(List<Cat> cats, long id)
        {
            return await new HttpClient().PostAsync($"http://localhost:8080/setFriends/{id}", new StringContent(JsonConvert.SerializeObject(cats), Encoding.UTF8, "application/json"));
        }
    }
}
