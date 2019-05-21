using Beam.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beam.Client.Services
{
    public class DataService
    {
        private readonly BeamApiService _apiService;
        public IReadOnlyList<Frequency> Frequencies { get; private set; }
        public IReadOnlyList<Ray> Rays { get; private set; } = new List<Ray>();
        public User CurrentUser { get; set; }

        private int? selectedFrequency;
        public int SelectedFrequency
        {
            get
            {
                if (!selectedFrequency.HasValue && Frequencies.Count > 0)
                {
                    selectedFrequency = Frequencies.First().Id;
                }
                return selectedFrequency ?? 0;
            }
            set
            {
                selectedFrequency = value;
                GetRays(value).ConfigureAwait(false);
            }
        }

        public DataService(BeamApiService apiService)
        {
            //throw new Exception("Oops");
            _apiService = apiService;
            if (CurrentUser == null) CurrentUser = new User() { Name = "Anon" + new Random().Next(0, 10) };
        }

        public event Action UdpatedFrequencies;
        public event Action UpdatedRays;

        public async Task GetFrequencies()
        {
            Frequencies = await _apiService.FrequencyList();
            UdpatedFrequencies?.Invoke();
        }

        public async Task GetRays(int FrequencyId)
        {
            Rays = new List<Ray>();
            Rays = await _apiService.GetRaysByFrequency(FrequencyId);
            UpdatedRays?.Invoke();
        }

        public async Task AddFrequency(string Name)
        {
            Frequencies = await _apiService.AddFrequency(Name);
            UdpatedFrequencies?.Invoke();
        }

        public async Task CreateRay(string text)
        {
            var ray = new Ray()
            {
                FrequencyId = selectedFrequency.Value,
                Text = text,
                UserId = CurrentUser.Id
            };

            if (CurrentUser.Id == 0)
            {
                await GetOrCreateUser();
                ray.UserId = CurrentUser.Id;
            }

            Rays = await _apiService.CreateRay(ray);
            UpdatedRays?.Invoke();
        }

        public async Task GetOrCreateUser(string newName = null)
        {
            CurrentUser = await _apiService.GetOrCreateUser(newName ?? CurrentUser.Name);
        }

        public async Task PrismRay(int RayId)
        {
            if (CurrentUser.Id == 0) await GetOrCreateUser();
            Prism prism = new Prism() { RayId = RayId, UserId = CurrentUser.Id };
            Rays = await _apiService.AddPrism(prism);
            UpdatedRays?.Invoke();
        }

        public async Task UnPrismRay(int RayId)
        {
            if (CurrentUser.Id == 0) await GetOrCreateUser();
            Rays = await _apiService.RemoveUserRay(CurrentUser.Id, RayId);
            UpdatedRays?.Invoke();
        }

        public async Task<List<Ray>> GetMyRays()
        {
            return await _apiService.GetUserRays(CurrentUser.Name);
        }

        public async Task<List<Ray>> GetUserRays(string Name)
        {
            return await _apiService.GetUserRays(Name ?? CurrentUser.Name);
        }

    }
}
