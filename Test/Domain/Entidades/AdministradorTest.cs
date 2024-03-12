using minimal.Dominios.Entidades;

namespace Test.Domain.Entidades

{
    public class AdministradorTest
    {
        [TestMethod]
        public void TestarGetESetPropriedades()
        {
            var adm = new Adiminstrador();
            adm.Id = 1;
            adm.Email = "teste@teste.com";
            adm.Senha = "teste";
            adm.Perfil = "Adm";
            Assert.AreEqual(1, adm.Id);
            Assert.AreEqual("teste@teste.com", adm.Email);
            Assert.AreEqual("teste", adm.Senha);
            Assert.AreEqual("Adm", adm.Perfil);
        }

    }
}