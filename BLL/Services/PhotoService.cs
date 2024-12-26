using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
#nullable disable
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{

    //public interface IPhotoService
    //{

    //    public IQueryable<PhotoTypesModel> Query();

    //    public ServiceBase Create(Photo record);
    //    public ServiceBase Update(Photo record);
    //    public ServiceBase Delete(int id);
    //}




    public class PhotoService : ServiceBase, IService<Photo, PhotoModel>
    {
        public PhotoService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Photo record)
        {
            if (_db.Photos.Any(p => p.Title.ToLower() == record.Title.ToLower().Trim() && p.isTakenStudio == record.isTakenStudio && p.PhotoDate == record.PhotoDate))
                return Error("Photo with same title, Date and photo condition exists!!");
            record.Title=record.Title?.Trim();
            _db.Photos.Add(record);
            _db.SaveChanges();
            return Success("Photo Created Successfully.");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Photos.Include(p=>p.PhotoOwners).SingleOrDefault(p => p.Id == id);

            if (entity is null)
                return Error("Photo cant be found");
            _db.PhotoOwners.RemoveRange(entity.PhotoOwners);
            _db.Photos.Remove(entity);
            _db.SaveChanges();
            return Success("Photo Deleted Successfully");
        }

        public IQueryable<PhotoModel> Query()
        {
           return _db.Photos.Include(p=>p.PhotoTypes).Include(p=>p.PhotoOwners).ThenInclude(po=>po.Owner).OrderByDescending(p => p.PhotoDate).ThenByDescending(p=>p.isTakenStudio).ThenBy(p=>p.Title).Select(p => new PhotoModel() { Record = p });
          
        }

        private void Select(Func<object, PhotoModel> value)
        {
            throw new NotImplementedException();
        }

        public ServiceBase Update(Photo record)
        {
            if (_db.Photos.Any(p => p.Id!=record.Id&& p.Title.ToLower() == record.Title.ToLower().Trim() &&
            p.isTakenStudio == record.isTakenStudio && p.PhotoDate == record.PhotoDate))
                return Error("Photo with same title, Date and photo condition exists!!");
            var entity = _db.Photos.Include(p=>p.PhotoOwners).SingleOrDefault(p => p.Id == record.Id);
            if (entity == null)
                return Error("Photo not found");

            _db.PhotoOwners.RemoveRange(entity.PhotoOwners);

            entity.Title = record.Title?.Trim();
            entity.isTakenStudio = record.isTakenStudio;
            entity.PhotoDate = record.PhotoDate;
            entity.PhotoOwners = record.PhotoOwners;
            entity.ApertureValue = record.ApertureValue;
            entity.ShutterSpeed = record.ShutterSpeed;
            entity.PhotoTypesID = record.PhotoTypesID;

            entity.PhotoTypes = record.PhotoTypes;

            _db.Photos.Update(entity);
            _db.SaveChanges();
            entity.Id = record.Id;

            _db.Photos.Add(entity);
            _db.SaveChanges();
            return Success("Photo Updated Successfully.");
        }
    }
}
