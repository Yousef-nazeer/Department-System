using Demo.BLL.Interfaces;
using Demo.DAL.Context;
using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class
    {
        private readonly MVCAppDbContext _Context;
        public GenericRepository(MVCAppDbContext Context)
        {
            _Context = Context;
            //Context = new MVCApp();
        }
        public async  Task <int> Add(T item)
        {
           await _Context.Set<T>().AddAsync(item);
            return await _Context.SaveChangesAsync();
        }


        public async Task< int> Delete(T item)
        {

            _Context.Set<T>().Remove(item);
            return await _Context.SaveChangesAsync();

        }

        public async Task <T> Get(int id)
            //=> _Context.Set<T>().Where(D => D.Id == id).FirstOrDefault();
         => await _Context.Set<T>().FindAsync(id);
        public async Task< IEnumerable<T>> GetAll()
            =>await _Context.Set<T>().ToListAsync();

        public async Task< int>  Update(T item)
        {
            _Context.Set<T>().Update(item);
            return await _Context.SaveChangesAsync();
        }
    }
}
