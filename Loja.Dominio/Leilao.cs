using Loja.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Dominio
{
    public class Leilao
    {
        public const decimal DescontoMaximo = 0.1m;
        public int Id { get; set; }
        public string NomeLote { get; set; }
        public decimal Preco { get; set; }
        public List<Produto> Produtos { get; set; }

        public List<string> validar()
        {
            var erros = new List<string>();

            if (string.IsNullOrEmpty(NomeLote?.Trim()))
            {
                erros.Add("Nome vazio");
            }

            var somaProdutos = Produtos.Sum(p => p.Preco);            

            if ((somaProdutos - Preco) > (somaProdutos * DescontoMaximo))
            {
                erros.Add("Desconto máximo excedido");
            }

            if(Produtos.Count <= 0)
            {
                erros.Add("Nenhum produtos selecionado");
            }

            return erros;
        }
    }
}
