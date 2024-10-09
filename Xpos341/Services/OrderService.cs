using Newtonsoft.Json;
using System.Text;
using xpos341.viewmodels;

namespace Xpos341.Services
{
    public class OrderService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";

        private VMResponse response = new VMResponse();

        public OrderService(IConfiguration _configuration)
        {
            configuration = _configuration;
            RouteAPI = configuration["RouteAPI"];
        }

        public async Task<VMResponse> SubmitOrder(VMOrderHeader dataParam)
        {
            //Proses convert dari objext ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //proses ubah string ke json
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var request = await client.PostAsync(RouteAPI + "apiOrder/SubmitOrder", content);

            if (request.IsSuccessStatusCode)
            {
                var apiResponse = await request.Content.ReadAsStringAsync();

                response = JsonConvert.DeserializeObject<VMResponse>(apiResponse);
            }
            else
            {
                response.Success = false;
                response.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
            }

            return response;
        }

        public async Task<List<VMOrderHeader>> GetDataOrderHeaderDetail(int idCustomer)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiOrder/GetDataOrderHeaderDetail/{idCustomer}");
            List<VMOrderHeader> data = JsonConvert.DeserializeObject<List<VMOrderHeader>>(apiResponse);

            return data;
        }

        public async Task<int> CountTransaction(int idCustomer)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiOrder/CountTransaction/{idCustomer}");
            int data = JsonConvert.DeserializeObject<int>(apiResponse);

            return data;
        }
    }
}
