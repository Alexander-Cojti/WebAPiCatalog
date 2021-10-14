using Catalogo.Controllers;
using Catalogo.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo.Repositories
{
    public class InMemItemRepository : IItemsRepository
    {
        private readonly List<Item> Items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Kebyn", Age = 10 },
            new Item { Id = Guid.NewGuid(), Name = "Juan", Age = 11 },
            new Item { Id = Guid.NewGuid(), Name = "Nat", Age = 15 }
        };
        public IEnumerable<Item> GetItems()
        {
            return Items;
        }
        public Item GetItemAsync(Guid id)
        {
            return Items.Where(item => item.Id == id).SingleOrDefault();
        }

        public void CreateItem(Item item)
        {
            Items.Add(item);
        }
        public void UpdateItem(Item item)
        {
            var index = Items.FindIndex(existinItem => existinItem.Id == item.Id);
            Items[index] = item;
        }

        public void DeleteItem(Guid id)
        {
            var index = Items.FindIndex(existinItem => existinItem.Id == id);
            Items.RemoveAt(index);
        }
    }
}
