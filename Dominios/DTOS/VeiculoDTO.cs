using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace minimal.Dominios.DTOS
{
    public record VeiculoDTO
    {

        public String Nome { get; set; } = default!;

        public String Marca { get; set; } = default!;
        public int Ano { get; set; } = default!;
    }
}