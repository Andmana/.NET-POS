using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using xpos341.datamodels;
using xpos341.viewmodels;

namespace Xpos341.Services
{
    public class CategoryTryService
    {
        private readonly Xpos341Context db;
        VMResponse response = new VMResponse();
        int IdUser = 1;

        public CategoryTryService(Xpos341Context _db)
        {
            db = _db;
        }
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TblCategory, VMTblCategory>();
                cfg.CreateMap<VMTblCategory, TblCategory>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }
        public List<VMTblCategory> GetAllData()
        {
            List<TblCategory> dataModel = db.TblCategories
                                            .Where(a => a.IsDelete == false)
                                            .ToList();

            List<VMTblCategory> dataView = GetMapper().Map<List<VMTblCategory>>(dataModel);

            return dataView;
        }
        
        public VMResponse Create(VMTblCategory dataView)
        {
            TblCategory dataModel = GetMapper().Map<TblCategory>(dataView);
            dataModel.IsDelete = false;
            dataModel.CreateDate = DateTime.Now;
            dataModel.CreateBy = IdUser;

            try
            {
                db.Add(dataModel);
                db.SaveChanges();

                response.Message = "OK";
                response.Entity = dataModel;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Failed saved :" + ex.Message;
                response.Entity = dataView;
            }
            return response;
        }
    }
}
