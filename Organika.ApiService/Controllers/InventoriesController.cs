using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Organika.ApiService.Models;
using Organika.ApiService.RepositoryContracts;
using Organika.ApiService.ViewModels;

namespace Organika.ApiService.Controllers
{
    [RoutePrefix("api/v1/inventory")]
    public class InventoriesController : ApiController
    {
        private readonly IRepoInventory _repo = null;
        public InventoriesController(IRepoInventory repo)
        {
            _repo = repo;
        }

        [Route("getall")]
        public IHttpActionResult GetInventories()
        {
            var inventories = this._repo.GetInventories();
            var lstInventoryVM = inventories.Select(i => new InventoryVM()
                                {
                                    Id = i.Id,
                                    Name = i.Name,
                                    Price = i.Price,
                                }).ToList<InventoryVM>(); ;


            if (lstInventoryVM == null || lstInventoryVM.Count == 0)
            {
                return NotFound();
            }
            return Ok(lstInventoryVM);
        }

        [HttpGet]
        [ResponseType(typeof(Inventory))]
        [Route("get/{id}")]
        public async Task<IHttpActionResult> GetInventory(Guid id)
        {
            var inventory = await this._repo.GetInventory(id);
            if (inventory == null)
            {
                return NotFound();
            }
            var inventoryVM = new InventoryVM()
            {
                Id = inventory.Id,
                Name = inventory.Name,
                Price = inventory.Price,
            };
            return Ok(inventoryVM);
        }

        [ResponseType(typeof(void))]
        [Route("update")]
        public async Task<IHttpActionResult> PutInventory(InventoryVM inventoryVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var inventory = new Inventory()
            {
                Id = inventoryVM.Id,
                Name = inventoryVM.Name,
                Price = inventoryVM.Price,
                ModifiedBy = inventoryVM.ModifiedBy,
                ModifiedDate = DateTime.Now,
            };
            try
            {
                await this._repo.UpdateInventory(inventory);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(inventoryVM.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(inventoryVM);
        }

        [ResponseType(typeof(Inventory))]
        [Route("create")]
        public async Task<IHttpActionResult> PostInventory(InventoryVM inventoryVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var inventory = new Inventory()
            {
                Id = Guid.NewGuid(),
                Name = inventoryVM.Name,
                Price = inventoryVM.Price,
                CreatedBy = inventoryVM.CreatedBy,
                CreatedDate = DateTime.Now,
            };
            try
            {
                await this._repo.CreateInventory(inventory);
            }
            catch (DbUpdateException)
            {
                if (InventoryExists(inventory.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        [ResponseType(typeof(Inventory))]
        [Route("delete/{id}")]
        public async Task<IHttpActionResult> DeleteInventory(Guid id)
        {
            var inventory = await this._repo.DeleteInventory(id);

            return Ok(inventory);
        }
        private bool InventoryExists(Guid id)
        {
            return this._repo.GetInventories().Count(e => e.Id == id) > 0;
        }
    }
}