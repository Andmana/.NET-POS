using Newtonsoft.Json;
using System.Text;
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

        public async Task<bool> CheckByName(string name, int id)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiRole/CheckByName/{name}/{id}");
            bool data = JsonConvert.DeserializeObject<bool>(apiResponse);
            return data;
        }
        public async Task<VMResponse> Add(VMTblRole dataParam)
        {
            //Proses convert dari objext ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //proses ubah string ke json
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var request = await client.PostAsync(RouteAPI + "apiRole/Add", content);

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

        public async Task<VMTblRole> GetDataById(int id)
        {

            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiRole/GetById/{id}");
            VMTblRole data = JsonConvert.DeserializeObject<VMTblRole>(apiResponse);

            return data;
        }

        public async Task<VMResponse> Edit(VMTblRole dataParam)
        {
            //Proses convert dari objext ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //proses ubah string ke json
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var request = await client.PutAsync(RouteAPI + "apiRole/Edit", content);

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
            var request = await client.DeleteAsync(RouteAPI + $"apiRole/Delete/{id}");

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

        public async Task<VMTblRole> GetDataById_MenuAccess(int id)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiRole/GetDataById_MenuAccess/{id}");
            VMTblRole data = JsonConvert.DeserializeObject<VMTblRole>(apiResponse)!;

            return data;
        }
    }
}
