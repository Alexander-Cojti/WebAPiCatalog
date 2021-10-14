using Catalogo.Dtos;
using Catalogo.Entities;
using Catalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
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
        public ItemsController(IItemsRepository _repository)
        {
            this.repository = _repository;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsyn(Guid id)
        {
            var item = await repository.GetItemAsync(id);
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
