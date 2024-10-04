using AutoMapper;
using Newtonsoft.Json;
using System.Text;
using xpos341.datamodels;
using xpos341.viewmodels;

namespace Xpos341.Services
{
    public class VariantService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse response = new VMResponse();

        public VariantService(IConfiguration _configuration)
        {
            configuration = _configuration;
            RouteAPI = configuration["RouteAPI"];
        }

        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TblVariant, VMTblVariant>();
                cfg.CreateMap<VMTblVariant, TblVariant>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        public async Task<List<VMTblVariant>> GetAllData()
        {
            // Get API return string json
            string apiResponse = await client.GetStringAsync(RouteAPI + "apiVariant/GetAllData");
            // json convert
            List<VMTblVariant> data = JsonConvert.DeserializeObject<List<VMTblVariant>>(apiResponse);

            return data;
        }

        public async Task<bool> CheckVariantByName(string nameVariant, int id, int idCategory)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiVariant/CheckByName/{nameVariant}/{id}/{idCategory}");
            bool data = JsonConvert.DeserializeObject<bool>(apiResponse);
            return data;
        }

        public async Task<VMResponse> Create(VMTblVariant dataParam)
        {
            TblVariant data = GetMapper().Map<TblVariant>(dataParam);
            //Proses convert dari objext ke string
            string json = JsonConvert.SerializeObject(data);

            //proses ubah string ke json
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var request = await client.PostAsync(RouteAPI + "apiVariant/Save", content);

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
        public async Task<VMTblVariant> GetDataById(int id)
        {

            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiVariant/GetDataById/{id}");
            VMTblVariant data = JsonConvert.DeserializeObject<VMTblVariant>(apiResponse);

            return data;
        }

        public async Task<VMResponse> Edit(VMTblVariant dataParam)
        {
            TblVariant data = GetMapper().Map<TblVariant>(dataParam);
            //Proses convert dari objext ke string
            string json = JsonConvert.SerializeObject(data);

            //proses ubah string ke json
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var request = await client.PutAsync(RouteAPI + "apiVariant/Edit", content);

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
            var request = await client.DeleteAsync(RouteAPI + $"apiVariant/Delete/{id}");

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

        public async Task<VMResponse> MultipleDelete(List<int> ids)
        {
            //Proses convert dari objext ke string
            string json = JsonConvert.SerializeObject(ids);

            //proses ubah string ke json
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var request = await client.PutAsync(RouteAPI + "apiVariant/MultipleDelete", content);

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
