using Domain.Entity;
using InfraStructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.IRepository.ServicesRepository
{
    public class ServicesCategory : IServicesRepository<Category>
    {
        private readonly ApplicationDbContext _context;

        public ServicesCategory(ApplicationDbContext context)
        {
            _context = context;
        }


        public bool Delete(Guid Id)
        {
            try
            {
                var result = FindBy(Id);
                result.CurrentState =(int) Helper.eCurrentState.Delete;
                _context.Categories.Update(result);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Category FindBy(Guid? Id)
        {
            try
            {
                return _context.Categories.FirstOrDefault(x => x.Id.Equals(Id) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Category FindBy(string Name)
        {
            try
            {
                // Name.Trim() =>  to avoid the spacess 

                return _context.Categories.FirstOrDefault(x =>x.Name.Equals(Name.Trim()) && x.CurrentState > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Category> GetAll()
        {
            try 
            {
                return _context.Categories.OrderBy(x => x.Name).Where(x=>x.CurrentState >0).ToList();
            }
            catch (Exception) 
            {
                return null;
            }
        }

        //Save and Update
        public bool Save(Category model)
        {

            try
            {
                
                var result = FindBy(model.Id);
                if(result == null) // Save new category
                { 
                    model.Id = Guid.NewGuid();
                    model.CurrentState =(int) Helper.eCurrentState.Active;
                  
                    _context.Categories.Add(model);
                    _context.SaveChanges();
                }
                else // update
                {
                    result.Name = model.Name;
                    result.CurrentState=(int) Helper.eCurrentState.Active;
                    result.Description= model.Description;

                    _context.Categories.Update(result);
                    _context.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
