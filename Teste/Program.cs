using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using RestSharp.Deserializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


/// <summary>
///
/// O Código deve retornar
///     Problema resolvido
///    
/// Não deve ser adicionado nenhum RETURN a mais
///
/// </summary>

class Program {
    //Nao mexer aqui
    #region Nao mexer aqui
    static void Main(string[] args) {
        Console.WriteLine(Problema());
        Console.ReadLine();
    }
    #endregion
    //
    public static string Problema() {
        try {
            var dados = BuscarInformacoesML();
            if (dados != null && dados.Count() == 4)
                return "Problema resolvido";
            else
                return "Problema com falha";
        } catch (Exception ex) {
            return "Problema com falha";
        }
    }

    /// <summary>
    ///
    ///     Usando RestSharp e Newtonsoft.Json, Consulte a api do CEP e retorne em um array de "DadosRetorno"
    ///     corrigindo qualquer erro que possa ocorrer
    ///    
    ///     Exemplo: https://viacep.com.br/ws/{0}/json/86084026
    ///    
    ///     o metodo deverá retornar os seguintes items:
    ///    
    ///     86084026
    ///     86088050
    ///     86088040
    ///     86088030
    ///
    /// </summary>
    /// <returns></returns>


    private static IEnumerable<DadosRetorno> BuscarInformacoesML() {

            try {

            int n = 4;
            string[] vect = new string[n];

            vect[0] = "86084026";
            vect[1] = "86088050";
            vect[2] = "86088040";
            vect[3] = "86088030";

            IList<DadosRetorno> list = new List<DadosRetorno>();

                for (int i = 0; i < n; i++) {

                    RestClient restClient = new RestClient(string.Format("https://viacep.com.br/ws/{0}/json/", vect[i]));
                    IRestRequest request = new RestRequest(Method.GET);
                    IRestResponse response = restClient.Execute(request);


                    if (response.StatusCode != System.Net.HttpStatusCode.BadRequest) {
                        DadosRetorno dadosRetorno = new JsonDeserializer().Deserialize<DadosRetorno>(response);
                    //DadosRetorno dadosRetorno = JsonConvert.DeserializeObject<DadosRetorno>(response.Content);



                    list.Add(new DadosRetorno() {
                            cep = dadosRetorno.cep,
                            logradouro = dadosRetorno.logradouro,
                            complemento = dadosRetorno.complemento,
                            bairro = dadosRetorno.bairro,
                            localidade = dadosRetorno.localidade,
                            uf = dadosRetorno.uf,
                            unidade = dadosRetorno.unidade,
                            ibge = dadosRetorno.ibge,
                            gia = dadosRetorno.gia
                        });
    
                    } else {

                        Console.WriteLine("Houve um problema na requisição" + response.ErrorMessage);

                    }
                }
                return list;


        } catch (Exception ex) {
            Console.WriteLine("Exception when calling ItemsApi.ItemsIdGet: " + ex.Message);
            Console.WriteLine(ex.StackTrace);
            return null;
        }
    }

}




public class DadosRetorno {

    public string cep { get; set; }
    public string logradouro { get; set; }
    public string complemento { get; set; }
    public string bairro { get; set; }
    public string localidade { get; set; }
    public string uf { get; set; }
    public string unidade { get; set; }
    public string ibge { get; set; }
    public string gia { get; set; }
}
