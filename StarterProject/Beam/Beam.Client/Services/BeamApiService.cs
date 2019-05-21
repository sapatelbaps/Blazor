using System.Collections.Generic;
using Microsoft.AspNetCore.Blazor;
using System.Net.Http;
using System.Threading.Tasks;
using Beam.Shared;

namespace Beam.Client.Services
{
    public class BeamApiService
    {
        private HttpClient http;

        public BeamApiService(HttpClient httpInstance)
        {
            http = httpInstance;
        }

        public async Task<List<Frequency>> FrequencyList()
        {
            return await http.GetJsonAsync<List<Frequency>>("/api/Frequency/All");
        }

        public async Task<List<Ray>> GetRaysByFrequency(int FrequencyId)
        {
            return await http.GetJsonAsync<List<Ray>>($"/api/Ray/{FrequencyId}");
        }

        public async Task<List<Frequency>> AddFrequency(string Name)
        {
            return await http.PostJsonAsync<List<Frequency>>("/api/Frequency/Add", new Frequency() { Name = Name });
        }

        public async Task<List<Ray>> CreateRay(Ray ray)
        {
            return await http.PostJsonAsync<List<Ray>>("/api/Ray/Add", ray);
        }

        public async Task<User> GetOrCreateUser(string newName)
        {
            return await http.GetJsonAsync<User>($"api/User/Get/{newName}");
        }

        public async Task<List<Ray>> AddPrism(Prism prism)
        {
            return await http.PostJsonAsync<List<Ray>>("/api/Prism/Add", prism);
        }

        public async Task<List<Ray>> RemoveUserRay(int CurrentUserId, int RayId)
        {
            return await http.GetJsonAsync<List<Ray>>($"/api/Prism/Remove/{CurrentUserId}/{RayId}");
        }
        public async Task<List<Ray>> GetUserRays(string Name)
        {
            return await http.GetJsonAsync<List<Ray>>($"/api/Ray/user/{Name}");
        }

    }
}
