using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Globalization;

namespace Loja.Mvc.Helpers
{
    public class CulturaHelpers
    {
        public CulturaHelpers()
        {
            ObterRegiao();
        }

        public const string LinguagemPadrao = "pt-BR";

        public string Abreviacao { get; private set; }
        public CultureInfo CultureInfo { get; private set; }
        public List<string> LinguagemSuportadas { get; }
            = new List<string> { LinguagemPadrao, "es", "en-US" };
        public string NomeNativo { get; private set; }

        private void ObterRegiao()
        {
            var linguagem = LinguagemPadrao;
            var linguagemSelecionada = HttpContext.Current.Request.Cookies[Cookie.LinguagemSelecionada];

            if (linguagemSelecionada != null && LinguagemSuportadas.Contains(linguagemSelecionada.Value))
            {
                linguagem = linguagemSelecionada.Value;
            }

            var cultura = CultureInfo.CreateSpecificCulture(linguagem);

            this.CultureInfo = cultura;

            var regiao = new RegionInfo(cultura.LCID);

            NomeNativo = regiao.NativeName;
            Abreviacao = regiao.TwoLetterISORegionName.ToLower();

        }
    }
}