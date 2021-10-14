using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo.Entities
{
    public record Item
    {
        public Guid Id { get; init; }
        public int Age { get; init; }
        public string Name { get; init; }
        public string DateTimeCreate { get; init; }
    }
}
