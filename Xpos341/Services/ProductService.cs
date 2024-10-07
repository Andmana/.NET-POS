using Newtonsoft.Json;
using System.Text;
using xpos341.datamodels;
using xpos341.viewmodels;

namespace Xpos341.Services
{
    public class ProductService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse response = new VMResponse();
        public ProductService(IConfiguration _configuration)
        {
            configuration = _configuration;
            RouteAPI = configuration["RouteAPI"];
        }

        public async Task<List<VMTblProduct>> GetAllData()
        {
            // Get API return string json
            string apiResponse = await client.GetStringAsync(RouteAPI + "apiProduct/GetAllData");
            // json convert
            List<VMTblProduct> data = JsonConvert.DeserializeObject<List<VMTblProduct>>(apiResponse);

            return data;
        }
        public async Task<bool> CheckVariantByName(string name, int id, int idVariant)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiProduct/CheckByName/{name}/{id}/{idVariant}");
            bool data = JsonConvert.DeserializeObject<bool>(apiResponse);
            return data;
        }
        public async Task<VMResponse> Create(VMTblProduct dataParam)
        {
            //Proses convert dari objext ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //proses ubah string ke json
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var request = await client.PostAsync(RouteAPI + "apiProduct/Save", content);

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

        public async Task<VMTblProduct> GetDataById(int id)
        {

            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiProduct/GetDataById/{id}");
            VMTblProduct data = JsonConvert.DeserializeObject<VMTblProduct>(apiResponse);

            return data;
        }

        public async Task<VMResponse> Edit(TblProduct dataParam)
        {
            //Proses convert dari objext ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //proses ubah string ke json
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var request = await client.PutAsync(RouteAPI + "apiProduct/Edit", content);

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
            var request = await client.DeleteAsync(RouteAPI + $"apiProduct/Delete/{id}");

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
