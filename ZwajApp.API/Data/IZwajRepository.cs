using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZwajApp.API.Models;

namespace ZwajApp.Api.Data
{
    public interface IZwajRepository
    {
        void add<T>(T entity) where T : class;
        void delete<T>(T entity) where T : class;
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task<bool> SaveAll();

    }
}
