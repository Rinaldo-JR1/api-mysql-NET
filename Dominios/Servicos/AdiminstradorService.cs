using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal.Dominios.DTOS;
using minimal.Dominios.Entidades;
using minimal.Dominios.Interfaces;
using minimal.Infraestrutura.DB;

namespace minimal.Dominios.Servicos
{

    public class AdiminstradorService : iAdimistradorServico
    {
        private DbContexto _context;
        public AdiminstradorService(DbContexto db)
        {
            _context = db;
        }

        public Adiminstrador? Login(LoginDTO loginDto)
        {
            var adm = _context.Adiminstradors.Where(a => a.Senha == loginDto.Senha && a.Email == loginDto.Email).FirstOrDefault();
            return adm;
        }
    }
}