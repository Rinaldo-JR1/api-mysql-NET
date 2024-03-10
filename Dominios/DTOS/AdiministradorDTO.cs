using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal.Dominios.Enus;

namespace minimal.Dominios.DTOS
{
    public class AdiministradorDTO
    {
        public string Email { get; set; } = default!;
        public string Senha { get; set; } = default!;
        public PerfilEnum Perfil { get; set; } = default!;
    }
}