using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RatearDesconto.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RatearDesconto
{
    class Program
    {
        static void Main(string[] args)
        {
            //Verifica se arquivo existe
            FileInfo fileInfo = new FileInfo(@"D:\Shared\Integracao_ClickAtende\Exemplo.txt");
            if (!fileInfo.Exists)
                throw new ApplicationException("Arquivo inexistente.");

            //Abre arquivo
            using (TextReader textReader = new StreamReader(fileInfo.FullName, Encoding.UTF8))
            {
                //Lê arquivo
                string conteudo = textReader.ReadToEnd();

                var jss = new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    DateTimeZoneHandling = DateTimeZoneHandling.Unspecified,
                    DateParseHandling = DateParseHandling.DateTimeOffset
                };

                JObject json = (JObject)JsonConvert.DeserializeObject<Object>(conteudo, jss);

                JArray itensArray = (JArray)json["itens"];

                decimal totalDescontoAplicado = 0;

                Venda venda = new Venda(
                    json.Value<DateTimeOffset>("dataHora").DateTime,
                    Auxiliar.VendaStatus.Realizado,
                    json["total"].Value<decimal>("valorSubTotal"),
                    json["total"].Value<decimal>("valorDesconto"),
                    json["total"].Value<decimal>("valorAcrescimo"),
                    json["total"].Value<decimal>("valorTotal"),
                    json.Value<string>("versao"),
                    "ClickAtende",
                    json.Value<string>("id"));

                venda.AdicionarListaItens(itensArray.Select((x, i) => new Item(
                    i + 1,
                    (string)x["codigo"],
                    (string)x["descricao"],
                    (decimal)x["quantidade"],
                    (decimal)x["valorUnitario"],
                    (decimal)x["valorSubTotal"],
                    0,
                    0,
                    0,
                    (decimal)x["valorSubTotal"] / venda.SubTotal)).OrderByDescending(x => x.FatorMultiplicacao).ToList());

                //Caso tenha descconto
                if (venda.Desconto > 0)
                {
                    //Calcular Rateio do Desconto e retornar desconto aplicado
                    totalDescontoAplicado = ItensCalculo.RatearDesconto(venda.Itens, venda.Desconto);

                    //Verifica se o total aplicado é igual ao total de desconto do pedido
                    ItemValidacao.SeDiferente(totalDescontoAplicado, venda.Desconto, "Erro na aplicação de desconto no pedido");

                    foreach (Item item in venda.Itens)
                        Console.WriteLine($"Index: {item.Id,-8} | SubTotal: {item.SubTotal,-8} | Fator: {item.FatorMultiplicacao,-30} | Desconto: {item.Desconto,-15}  | Total: {item.Total,-15}");

                    Console.WriteLine($"SubTotal: {venda.Itens.Sum(x => x.SubTotal),-15} | Desconto: {venda.Itens.Sum(x => x.Desconto),-15}  | Total: {venda.Itens.Sum(x => x.Total),-15}");
                }
            }

            Console.ReadLine();
        }
    }
}
