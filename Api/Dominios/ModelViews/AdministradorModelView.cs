using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal.Dominios.Enus;

namespace minimal.Dominios.ModelViews
{
    public record AdministradorModelView
    {
        public int Id { get; set; }
        public String Email { get; set; }
        public PerfilEnum Perfil { get; set; }
    }
}