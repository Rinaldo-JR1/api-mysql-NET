using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal.Dominios.Enus;

namespace minimal.Dominios.ModelViews
{
    public record AdmLogado
    {
        public int Id { get; set; }
        public String Email { get; set; }
        public String Token { get; set; }
        public string Perfil { get; set; }
    }
}