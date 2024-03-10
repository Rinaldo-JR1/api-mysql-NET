using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal.Dominios.Entidades;

namespace minimal.Dominios.Interfaces
{
    public interface iVeiculoService
    {
        List<Veiculo> Todos(int pagina = 1, string marca = null,string nome = null);
        Veiculo? BuscaPorId(int id);
        void Incluir(Veiculo veiculo);
        void Atualizar(Veiculo veiculo);
        void Apagar(Veiculo veiculo);
    }
}