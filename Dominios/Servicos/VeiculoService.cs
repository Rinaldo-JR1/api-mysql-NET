using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using minimal.Dominios.Entidades;
using minimal.Dominios.Interfaces;
using minimal.Infraestrutura.DB;

namespace minimal.Dominios.Servicos
{
    public class VeiculoService : iVeiculoService
    {
        private DbContexto _context;
        public VeiculoService(DbContexto db)
        {
            _context = db;
        }

        public void Apagar(Veiculo veiculo)
        {
            _context.Remove(veiculo);
            _context.SaveChanges();
        }

        public void Atualizar(Veiculo veiculo)
        {
            _context.Veiculos.Update(veiculo);
            _context.SaveChanges();
        }

        public Veiculo? BuscaPorId(int id)
        {
            return _context.Veiculos.Find(id);
        }

        public void Incluir(Veiculo veiculo)
        {
            _context.Veiculos.Add(veiculo);
            _context.SaveChanges();
        }

        public List<Veiculo> Todos(int pagina = 1, string marca = null, string nome = null)
        {
            var query = _context.Veiculos.AsQueryable();
            if (!string.IsNullOrEmpty(nome))
            {
                int itemsPorPagina = 10;
                query = query.Skip(pagina - 1 * itemsPorPagina).Take(itemsPorPagina).Where(v => EF.Functions.Like(v.Nome.ToLower(), $"%{nome}%"));
            }
            return query.ToList();
        }
    }
}