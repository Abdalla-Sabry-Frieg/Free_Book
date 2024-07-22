using Domain.Entity;
using InfraStructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.IRepository.ServicesRepository
{
    public class ServicesLogCategory : IServicesRepositoryLog<LogCategory>
    {
        private readonly ApplicationDbContext _context;

        public ServicesLogCategory(ApplicationDbContext context) 
        {
            _context = context;
        } 
        public bool Delete(Guid Id, Guid UserId)
        {
            try
            {
                var logCategory = new LogCategory
                {
                    Id =Guid.NewGuid(),
                    UserId =UserId,
                    CategoryId = Id ,
                    Date = DateTime.Now,
                    Action = Helper.Delete
                };
                _context.LogCategories.Add(logCategory);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

        public bool DeleteLog(Guid Id)
        {
            try
            {
                var result = FindBy(Id);
                if (result != null) 
                {
                    _context.LogCategories.Remove(result);
                    _context.SaveChanges();
                    return true;
                }
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

        public LogCategory FindBy(Guid? Id)
        {
           try
            {
                return _context.LogCategories.Include(x=>x.Category).FirstOrDefault(x => x.Id.Equals(Id));
            }
            catch (Exception)
            {
                return null;

            }
        }

        public List<LogCategory> GetAll()
        {

            try
            {
                return _context.LogCategories.Include(x => x.Category).OrderByDescending(x => x.Date).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Save(Guid? Id, Guid UserId)
        {
            try
            {
                var logCategory = new LogCategory
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    CategoryId = Id.Value,
                    Date = DateTime.Now,
                    Action = Helper.Save
                };
                _context.LogCategories.Add(logCategory);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

        public bool Update(Guid? Id, Guid UserId)
        {
            try
            {
                var logCategory = new LogCategory
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    CategoryId = Id.Value,
                    Date = DateTime.Now,
                    Action = Helper.Update
                };
                // Add => make tracking to events in data base 
                _context.LogCategories.Add(logCategory);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
    }
}
