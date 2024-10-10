using Newtonsoft.Json;
using xpos341.viewmodels;

namespace Xpos341.Services
{
    public class RoleService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        public string RouteAPI = "";

        private VMResponse response = new VMResponse();

        public RoleService(IConfiguration _configuration)
        {
            configuration = _configuration;
            RouteAPI = configuration["RouteAPI"];
        }

        public async Task<List<VMTblRole>> GetAllData()
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiRole/GetAllData");
            List<VMTblRole> data = JsonConvert.DeserializeObject<List<VMTblRole>>(apiResponse);

            return data;
        }
    }
}
