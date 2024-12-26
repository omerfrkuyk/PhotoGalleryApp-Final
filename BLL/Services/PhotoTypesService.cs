using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{

    public interface IPhotoTypesService
    {

        public IQueryable<PhotoTypesModel> Query();

        public ServiceBase Create(PhotoTypes record);
        public ServiceBase Update(PhotoTypes record);
        public ServiceBase Delete(int id);
    }


    public class PhotoTypesService : ServiceBase, IPhotoTypesService
    {

        public PhotoTypesService(Db db) : base(db)
        {
        }
        public IQueryable<PhotoTypesModel> Query()
        {
            return _db.PhotoTypes.OrderBy(p => p.Name).Select(p => new PhotoTypesModel() { Record = p });
        }


        public ServiceBase Create(PhotoTypes record)
        {
            if (_db.PhotoTypes.Any(p => p.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Photo with the same name Exists!!");
            record.Name = record.Name?.Trim();
            _db.PhotoTypes.Add(record);
            _db.SaveChanges();
            return Success("PhotoType Created Successfully.");
        }

        public ServiceBase Update(PhotoTypes record)
        {
            if (_db.PhotoTypes.Any(p => p.Id != record.Id && p.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Photo with the same name Exists!!");

            var entity = _db.PhotoTypes.SingleOrDefault(p => p.Id == record.Id);
            if (entity == null)
                return Error("PhotoType cant be found");
            entity.Name = record.Name?.Trim();
            _db.PhotoTypes.Update(entity);
            _db.SaveChanges();
            return Success("PhotoTypes Updated successfuly.");


        }


        public ServiceBase Delete(int id)
        {
            var entity = _db.PhotoTypes.Include(p=>p.Photos).SingleOrDefault(p => p.Id == id);
            if (entity == null)
                return Error("Photo Types cant be found!!!");
            if (entity.Photos.Any()) //count >0
                return Error("PhotoType has relational photos!!");
            _db.PhotoTypes.Remove(entity);
            _db.SaveChanges();
            return Success("PhotoTypes delete successfully");

        }

    }
}
