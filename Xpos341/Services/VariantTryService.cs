using AutoMapper;
using xpos341.datamodels;
using xpos341.viewmodels;

namespace Xpos341.Services
{
    public class VariantTryService
    {
        private readonly Xpos341Context db;
        VMResponse response = new VMResponse();

        int idUser = 1;

        public VariantTryService(Xpos341Context _db)
        {
            db = _db;
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

        public List<VMTblVariant> GetAllData()
        {
            List<VMTblVariant> dataView = new List<VMTblVariant>();
            dataView = (from v in db.TblVariants
                        join c in db.TblCategories on v.IdCategory equals c.Id 
                        where v.IsDelete == false
                        select new VMTblVariant
                        {
                            Id = v.Id,
                            NameVariant = v.NameVariant,
                            Description = v.Description,

                            IdCategory = v.IdCategory,
                            NameCategory = c.NameCategory,

                        }).ToList();

            return dataView;
        }
    }
}
