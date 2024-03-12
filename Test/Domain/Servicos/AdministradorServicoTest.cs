using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal.Dominios.Entidades;

namespace Test.Domain.Servicos
{
    public class AdministradorServicoTest
    {
        [TestMethod]
        public void TesteSalvarAdm()
        {
            var adm = new Adiminstrador();
            adm.Id = 1;
            adm.Email = "teste@teste.com";
            adm.Senha = "teste";
            adm.Perfil = "Adm";
        }
    }
}