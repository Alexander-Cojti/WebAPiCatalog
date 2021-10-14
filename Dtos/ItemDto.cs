using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo.Dtos
{
    public class ItemDto
    {
        public Guid Id { get; init; }
        public int Age { get; init; }
        public string Name { get; init; }
        public string DateTimeCreate { get; init; }
    }
}
