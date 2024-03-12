using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using minimal.Dominios.ModelViews;

namespace minimal.Dominios.DTOS
{
    public class ValidaVeiculoDTO
    {
        public ErrosDeValidacao validaDTO(VeiculoDTO veiculoDTO)
        {
            var validacao = new ErrosDeValidacao
            {
                Mensagem = new List<string>()
            };

            if (string.IsNullOrEmpty(veiculoDTO.Nome))
            {
                validacao.Mensagem.Add("O nome não pode ser vazio");
            }
            if (string.IsNullOrEmpty(veiculoDTO.Marca))
            {
                validacao.Mensagem.Add("O nome não pode ser vazio");
            }
            if (veiculoDTO.Ano <= 1950)
            {
                validacao.Mensagem.Add("Veiculo muito antigo");
            }
            return validacao;

        }
    }
}