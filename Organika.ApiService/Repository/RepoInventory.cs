using Organika.ApiService.Models;
using Organika.ApiService.RepositoryContracts;
using Organika.ApiService.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Organika.ApiService.Repository
{
    public class RepoInventory : IRepoInventory
    {
        private readonly OrganikaEntities entities = new OrganikaEntities();
        public IEnumerable<Inventory> GetInventories()
        {
            return entities.Inventories;
        }
        public async Task<Inventory> GetInventory(Guid id)
        {
            return await entities.Inventories.FindAsync(id);
        }
        public async Task<int> CreateInventory(Inventory inventory)
        {
            entities.Inventories.Add(inventory);
            try
            {
                return await entities.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> UpdateInventory(Inventory inventory)
        {
            entities.Entry(inventory).State = EntityState.Modified;
            try
            {
                return await entities.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Inventory> DeleteInventory(Guid id)
        {
            var inventory = await entities.Inventories.FindAsync(id);
            try
            {
                entities.Inventories.Remove(inventory);
                await entities.SaveChangesAsync();
                return inventory;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}