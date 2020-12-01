using BuildingBlocks.Core;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace BuildingBlocks.GeoLocation
{
    public class GoogleMapsService : IGoogleMapsService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        public GoogleMapsService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<GoogleEncodeResponse> GetAddress(double latitude, double longitude)
        {
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude},{longitude}&sensor=false&key={_configuration["GoogleMapsPlatform:ApiKey"]}";
            return await _client.GetAsync<GoogleEncodeResponse>(url);
        }

        public async Task<GoogleEncodeResponse> GetCoordinates(string address)
        {
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&sensor=false&key={_configuration["GoogleMapsPlatform:ApiKey"]}";
            return await _client.GetAsync<GoogleEncodeResponse>(url);
        }
    }
}
