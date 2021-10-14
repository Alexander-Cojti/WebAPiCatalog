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
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItemAsync(id);
            if (item is null)
            {
                return NotFound();
            }
            return item.AsDto();
        }
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repository.GetItems().Select(item => item.AsDto());
            return items;
        }
        [HttpPost]
        public ActionResult<ItemDto> Create(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Age = itemDto.Age,
                DateTimeCreate = DateTime.Now.ToString()
            };
            repository.CreateItem(item);
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = repository.GetItemAsync(id);
            if (existingItem is null)
            {
                return NotFound();
            }
            Item UpdatedItem = existingItem with
            {
                Name = itemDto.Name,
                Age = itemDto.Age
            };

            repository.UpdateItem(UpdatedItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult UpdateItem(Guid id)
        {
            var existingItem = repository.GetItemAsync(id);
            if (existingItem is null)
            {
                return NotFound();
            }
            repository.DeleteItem(id);
            return NoContent();
        }
    }
}
