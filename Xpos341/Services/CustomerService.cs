using Newtonsoft.Json;
using System.Text;
using xpos341.viewmodels;

namespace Xpos341.Services
{
    public class CustomerService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse response = new VMResponse();

        public CustomerService(IConfiguration _configuration)
        {
            configuration = _configuration;
            RouteAPI = configuration["RouteAPI"];
        }
        public async Task<List<VMTblCustomer>> GetAllData()
        {
            // Get API return string json
            string apiResponse = await client.GetStringAsync(RouteAPI + "apiCustomer/GetAllData");
            // json convert
            List<VMTblCustomer> data = JsonConvert.DeserializeObject<List<VMTblCustomer>>(apiResponse);

            return data;
        }

        public async Task<VMTblCustomer> GetById(int id)
        {
            // Get API return string json
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiCustomer/GetById/{id}");
            // json convert
            VMTblCustomer data = JsonConvert.DeserializeObject<VMTblCustomer>(apiResponse);

            return data;
        }

        public async Task<bool> CheckExists(string email, int id)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiCustomer/CheckExists/{email}/{id}");
            bool data = JsonConvert.DeserializeObject<bool>(apiResponse);
            return data;
        }

        public async Task<VMResponse> Add(VMTblCustomer dataParam)
        {
            //Proses convert dari objext ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //proses ubah string ke json
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var request = await client.PostAsync(RouteAPI + "apiCustomer/Add", content);

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

        public async Task<VMResponse> Edit(VMTblCustomer dataParam)
        {
            //Proses convert dari objext ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //proses ubah string ke json
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var request = await client.PutAsync(RouteAPI + "apiCustomer/Edit", content);

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

        public async Task<VMResponse> Delete(int id)
        {
            var request = await client.DeleteAsync(RouteAPI + $"apiCustomer/Delete/{id}");

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
    }


}
