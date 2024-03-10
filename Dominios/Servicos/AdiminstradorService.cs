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

        public void Criar(Adiminstrador adm)
        {
            _context.Adiminstradors.Add(adm);
            _context.SaveChanges();
        }

        public Adiminstrador? Login(LoginDTO loginDto)
        {
            var adm = _context.Adiminstradors.Where(a => a.Senha == loginDto.Senha && a.Email == loginDto.Email).FirstOrDefault();
            return adm;
        }

        public List<Adiminstrador> Todos(int pagina)
        {
            var query = _context.Adiminstradors.AsQueryable();
            int itemsPorPagina = 10;
            query = query.Skip(pagina - 1 * itemsPorPagina).Take(itemsPorPagina);
            return query.ToList();
        }
    }
}