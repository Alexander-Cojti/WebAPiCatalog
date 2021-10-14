using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo.Dtos
{
    public class UpdateItemDto
    {
        [Required]
        public int Age { get; init; }
        [Required]
        //no valida el require con init
        public string Name { get; init; }
    }
}
