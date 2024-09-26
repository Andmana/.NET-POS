using AutoMapper;
using xpos341.datamodels;
using xpos341.viewmodels;

namespace Xpos341.Services
{
    public class CategoryTryService
    {
        private readonly Xpos341Context db;

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
    }
}
