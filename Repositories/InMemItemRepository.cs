﻿using Catalogo.Controllers;
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
        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(Items);
        }
        public async Task<Item> GetItemAsync(Guid id)
        {
            var item = Items.Where(item => item.Id == id).SingleOrDefault();
            return await Task.FromResult(item);
        }

        public async Task CreateItemAsync(Item item)
        {
            Items.Add(item);
            await Task.CompletedTask;
        }
        public async Task UpdateItemAsync(Item item)
        {
            var index = Items.FindIndex(existinItem => existinItem.Id == item.Id);
            Items[index] = item;
            await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index = Items.FindIndex(existinItem => existinItem.Id == id);
            Items.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}
