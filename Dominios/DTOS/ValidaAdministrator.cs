using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal.Dominios.ModelViews;

namespace minimal.Dominios.DTOS
{
    public class ValidaAdministrator
    {
        public ErrosDeValidacao valida(AdiministradorDTO login)
        {
            var validacao = new ErrosDeValidacao
            {
                Mensagem = new List<string>()
            };
            if (string.IsNullOrEmpty(login.Senha))
            {
                validacao.Mensagem.Add("Por favor informe uma senha");
            }
            if (string.IsNullOrEmpty(login.Email))
            {
                validacao.Mensagem.Add("Por favor informe um e-mail");
            }
            if (string.IsNullOrEmpty(login.Perfil.ToString()))
            {
                validacao.Mensagem.Add("Por favor verifique o tipo de perfil");
            }
            return validacao;
        }
    }
}
