using Newtonsoft.Json;
using System.Text;
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

        
        public async Task<VMResponse> Create(TblCategory dataParam)
        {
            //Proses convert dari objext ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //proses ubah string ke json
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var request = await client.PostAsync(RouteAPI + "apiCategory/Save", content);

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
        public async Task<bool> CheckCategoryByName(string nameCategory, int id)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiCategory/GetCategoryByName/{nameCategory}/{id}");
            bool data = JsonConvert.DeserializeObject<bool>(apiResponse);
            return data;
        }

        public async Task<TblCategory> GetDataById(int id)
        {

            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiCategory/GetDataById/{id}" ) ;
            TblCategory data = JsonConvert.DeserializeObject<TblCategory>(apiResponse);

            return data;
        }

        public async Task<VMResponse> Edit(TblCategory dataParam)
        {
            //Proses convert dari objext ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //proses ubah string ke json
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var request = await client.PutAsync(RouteAPI + "apiCategory/Edit", content);

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

        public async Task<VMResponse> Delete(int id, int createBy)
        {
            var request = await client.DeleteAsync(RouteAPI + $"apiCategory/Delete/{id}/{createBy}") ;

            if (request.IsSuccessStatusCode)
            {
                var apiResponse = await request.Content.ReadAsStringAsync ();

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
