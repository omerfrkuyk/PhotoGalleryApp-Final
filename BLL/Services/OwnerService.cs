using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OwnerService : ServiceBase, IService<Owner,OwnerModel>
    {
        public OwnerService(Db db) : base(db)
        {
        }

        public ServiceBase Create(Owner record)
        {
            throw new NotImplementedException();
        }


        public ServiceBase Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<OwnerModel> Query()
        {
            return _db.Owners.OrderBy(o => o.Name).ThenBy(o => o.Surname).Select(o => new OwnerModel() { Record = o });
        }

        public ServiceBase Update(Owner record)
        {
            throw new NotImplementedException();
        }


    }

    public interface IService
    {
    }
}
