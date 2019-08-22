using System;
using System.IO;
using System.Net;
using System.ServiceModel;
using TesteCDL.wsConsulta;



namespace TesteCDL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                filtroConsultaSpcNacionalWS fil = new filtroConsultaSpcNacionalWS();
                fil.cpfCnpj = "12312312387";

                var client = new ConsultaSpcScWSServiceClient();
                client.ClientCredentials.UserName.UserName = "ENTIDADE:USUARIO";
                client.ClientCredentials.UserName.Password = "SENHA";

                using (OperationContextScope scope = new OperationContextScope(client.InnerChannel))
                {
                    var httpRequestProperty = new System.ServiceModel.Channels.HttpRequestMessageProperty();
                    httpRequestProperty.Headers[System.Net.HttpRequestHeader.Authorization] = "Basic " +
                                 Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(client.ClientCredentials.UserName.UserName + ":" + client.ClientCredentials.UserName.Password));
                    OperationContext.Current.OutgoingMessageProperties[System.ServiceModel.Channels.HttpRequestMessageProperty.Name] = httpRequestProperty;

                    var retornoBusca = client.SPCNacional02(fil);
                    foreach (var item in retornoBusca.registrosSPC)
                    {
                        System.Console.Write(item.contrato + " - " + item.valor);
                    }
                }
            }
            catch (CommunicationException ex)
            {
                ex.Message.ToString();
            }
            catch (TimeoutException ex)
            {
                ex.Message.ToString();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}
