using Newtonsoft.Json;
using PayrollSystem.Domain.Contracts.InfraService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Infrastructure.Service.SmsService
{
    public class SendSms : ISendSms
    {
        public void send(string text, string[] number)
        {
            var model = new SendModel();
            model.UserName = "support";
            model.Password = "Asdfqwer1234";
            model.DomainName = "hesabrayan";
            model.Smsbody = text;
            model.Mobiles = number;
            model.SenderNumber = "30008411008400";
            model.Id = new Random().Next(999999).ToString();
            var json = JsonConvert.SerializeObject(model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");


            var url = "https://mehrafraz.com/fullrest/" + "/api/Send";
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(url, data).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = response.Content.ReadAsStringAsync().Result;
                    //var result = JsonConvert.DeserializeObject<ResponseCodes>(responseContent);
                }

            }


        }
    }

    internal class SendModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DomainName { get; set; }
        public string Smsbody { get; set; }
        public string SenderNumber { get; set; }
        public string[] Mobiles { get; set; }
    }
}
