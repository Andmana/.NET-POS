﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using xpos341.datamodels;
using xpos341.viewmodels;

namespace Xpos341.Services
{
    public class CategoryTryService
    {
        private readonly Xpos341Context db;
        int IdUser = 1;
        VMResponse response = new VMResponse();

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
        [HttpPost]
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
                response.Message = "Failed saved : " + ex.Message;
                response.Entity = dataView;
            }
            return response;
        }

        [HttpPost]
        public VMTblCategory GetById(int id)
        {
            //TblCategory dataModel = db.TblCategories.FirstOrDefault(a => a.Id == id);
            TblCategory dataModel = db.TblCategories.Find(id);
            VMTblCategory dataview = GetMapper().Map<VMTblCategory>(dataModel);
            return dataview;
        }

        public VMResponse Edit(VMTblCategory dataview) {

            TblCategory dataModel = db.TblCategories.Find(dataview.Id);

            dataModel.NameCategory = dataview.NameCategory;
            dataModel.Description = dataview.Description;
            dataModel.UpdateBy = IdUser;
            dataModel.UpdateDate = DateTime.Now;

            try
            {

                db.Update(dataModel);
                db.SaveChanges();

                response.Message = "Data success Updated";
                response.Entity = GetMapper().Map<VMTblCategory>(dataModel);
            }
            catch (Exception ex) {
                response.Success = false;
                response.Message = "Failed saved : " + ex.Message;
                response.Entity = GetMapper().Map<VMTblCategory>(dataModel);
            }
            return response;
        }

        public VMResponse Delete(VMTblCategory dataview)
        {

            TblCategory dataModel = db.TblCategories.Find(dataview.Id);
            dataModel.IsDelete = true;
            dataModel.UpdateBy = IdUser;
            dataModel.UpdateDate = DateTime.Now;

            try
            {
                db.Update(dataModel);
                db.SaveChanges();

                response.Message = "Data success Deleted";
                response.Entity = GetMapper().Map<VMTblCategory>(dataModel);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Failed saved : " + ex.Message;
                response.Entity = GetMapper().Map<VMTblCategory>(dataModel);
            }
            return response;
        }
    }
}
