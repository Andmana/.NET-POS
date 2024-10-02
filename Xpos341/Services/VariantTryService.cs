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
                            CreateDate = v.CreateDate,

                            IdCategory = v.IdCategory,
                            NameCategory = c.NameCategory,

                        }).ToList();

            return dataView;
        }
        public VMResponse Create(VMTblVariant dataView)
        {
            //TblVariant dataModel = GetMapper().Map<TblVariant>(dataView);
            TblVariant dataModel = new TblVariant();

            dataModel.NameVariant = dataView.NameVariant;
            dataModel.Description = dataView.Description ?? "Desc of " + dataView.NameVariant;
            dataModel.IdCategory = dataView.IdCategory;
            dataModel.IsDelete = false;
            dataModel.CreateBy = idUser;
            dataModel.CreateDate = DateTime.Now;

            try
            {
                db.Add(dataModel);
                db.SaveChanges();

                response.Message = "Data Added Successfully";
                response.Entity = dataModel;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = "Save Failed : " + ex.Message;
                response.Entity = dataView;
            }

            return response;
        }

        public VMTblVariant GetById(int id)
        {
            VMTblVariant dataView = GetAllData().FirstOrDefault(a => a.Id == id)!;
            return dataView;
        }

        public VMResponse Edit(VMTblVariant dataView)
        {
            TblVariant dataModel = db.TblVariants.Where(a => a.Id == dataView.Id).FirstOrDefault();

            dataModel.NameVariant = dataView.NameVariant;
            dataModel.Description = dataView.Description ?? "Desc of " + dataView.NameVariant;
            dataModel.IdCategory = dataView.IdCategory;
            dataModel.UpdateBy = idUser;
            dataModel.UpdateDate = DateTime.Now;

            try
            {
                db.Update(dataModel);
                db.SaveChanges();

                response.Message = "Data Edited Successfully";
                response.Entity = dataModel;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Edit Failed : " + ex.Message;
                response.Entity = dataView;
            }

            return response;
        }
        public VMResponse Delete(VMTblVariant dataView)
        {
            TblVariant dataModel = db.TblVariants.Where(a => a.Id == dataView.Id).FirstOrDefault();

            dataModel.IsDelete = true;

            try
            {
                db.Update(dataModel);
                db.SaveChanges();

                response.Message = "Data Deleted Successfully";
                response.Entity = dataModel;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Delete Failed : " + ex.Message;
                response.Entity = dataView;
            }

            return response;
        }
    }
}
