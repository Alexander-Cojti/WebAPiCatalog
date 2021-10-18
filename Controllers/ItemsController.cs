using Catalogo.Dtos;
using Catalogo.Entities;
using Catalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : Controller
    {
        public readonly IItemsRepository repository;
        private readonly ILogger<ItemsController> logger;

        public ItemsController(IItemsRepository _repository, ILogger<ItemsController> _logger)
        {
            this.repository = _repository;
            this.logger = _logger;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsyn(Guid id)
        {
            Item item = null;
            try
            {
                logger.LogInformation("Inicio de búsqueda de item");
                item = await repository.GetItemAsync(id);
            }
            catch (Exception e)
            {
                logger.LogWarning(e.Message);
            }
            if (item is null)
            {
                return NotFound();
            }
            return item.AsDto();
        }
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await repository.GetItemsAsync())
                .Select(item => item.AsDto());
            return items;
        }
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateAsync(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Age = itemDto.Age,
                DateTimeCreate = DateTime.Now.ToString()
            };
            await repository.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItemAsyn), new { id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = await repository.GetItemAsync(id);
            if (existingItem is null)
            {
                return NotFound();
            }
            Item UpdatedItem = existingItem with
            {
                Name = itemDto.Name,
                Age = itemDto.Age
            };

            await repository.UpdateItemAsync(UpdatedItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> UpdateItem(Guid id)
        {
            var existingItem = await repository.GetItemAsync(id);
            if (existingItem is null)
            {
                return NotFound();
            }
            await repository.DeleteItemAsync(id);
            return NoContent();
        }
    }
}
