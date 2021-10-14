using Catalogo.Dtos;
using Catalogo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Age = item.Age,
                DateTimeCreate = DateTime.Now.ToString()
            };
        }
    }
}
