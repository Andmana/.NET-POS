using Newtonsoft.Json;
using xpos341.datamodels;
using xpos341.viewmodels;

namespace Xpos341.Services
{
    public class CategoryService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse response = new VMResponse();

        public CategoryService(IConfiguration _configuration)
        {
            configuration = _configuration;
            RouteAPI = configuration["RouteAPI"];
        }

        public async Task<List<TblCategory>> GetAllData()
        {
            List<TblCategory> data = new List<TblCategory>();

            string apiResponse = await client.GetStringAsync(RouteAPI + "apiCategory/GetAllData");
            data = JsonConvert.DeserializeObject<List<TblCategory>>(apiResponse);

            return data;
        }
    }
}
