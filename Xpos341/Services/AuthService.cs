using Newtonsoft.Json;
using xpos341.viewmodels;

namespace Xpos341.Services
{
    public class AuthService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        public string RouteAPI = "";

        private VMResponse response = new VMResponse();

        public AuthService(IConfiguration _configuration)
        {
            configuration = _configuration;
            RouteAPI = configuration["RouteAPI"];
        }

        public async Task<VMTblCustomer> CheckLogin(string email, string password)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiAuth/CheckLogin/{email}/{password}");
            VMTblCustomer data = JsonConvert.DeserializeObject<VMTblCustomer>(apiResponse);

            return data;
        }
    }
}
