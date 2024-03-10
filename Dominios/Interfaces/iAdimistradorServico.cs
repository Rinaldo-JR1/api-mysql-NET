using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal.Dominios.DTOS;
using minimal.Dominios.Entidades;

namespace minimal.Dominios.Interfaces
{
    public interface iAdimistradorServico
    {
        Adiminstrador? Login(LoginDTO loginDto);
        void Criar(Adiminstrador loginDto);
        List<Adiminstrador> Todos(int pagina = 1);

    }
}