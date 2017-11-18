using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loja.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Dominio.Tests
{
    [TestClass()]
    public class LeilaoTests
    {
        [TestMethod()]
        public void validarSucessoTest()
        {
            // Arrange
            var leilao = new Leilao();
            leilao.Id = 1;
            leilao.NomeLote = "Podutos";
            leilao.Preco = 100.50m;
            leilao.Produtos = 
                new List<Produto> { new Produto { Preco = 100m} };

            // Act

            var erros = leilao.validar();

            // Assert 

            Assert.IsTrue(erros.Count == 0);
        }

        [TestMethod()]
        public void validarErroTest()
        {
            // Arrange
            var leilao = new Leilao();
            leilao.Id = 1;
            leilao.NomeLote = "Podutos";
            leilao.Preco = 50.50m;
            leilao.Produtos =
                new List<Produto> { new Produto { Preco = 100m } };

            // Act

            var erros = leilao.validar();

            // Assert 

            Assert.IsTrue(erros.Contains("Desconto máximo excedido"));
        }
    }
}