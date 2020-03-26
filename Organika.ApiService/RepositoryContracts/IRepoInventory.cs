using Organika.ApiService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Organika.ApiService.RepositoryContracts
{
    public interface IRepoInventory
    {
        IEnumerable<Inventory> GetInventories();
        Task<Inventory> GetInventory(Guid id);
        Task<int> CreateInventory(Inventory inventory);
        Task<int> UpdateInventory(Inventory inventory);
        Task<Inventory> DeleteInventory(Guid id);
    }
}