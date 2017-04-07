using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace UsandoCaptcha.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ValidarCaptcha()
        {
            //Coloque aqui seu Secret Key
            var secretKey = "Informe aqui seu Secret Key gerado pelo google <3";

            //Obtem a resposta do cliente
            var response = Request["g-recaptcha-response"];

            //Cria um Web Cliente
            var client = new WebClient();

            //Valida os dados, e efetua download do resultado 
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));


            //Desserializa nosso Json obtido como resposta da API
            var obj = JObject.Parse(result);

            //Converte nosso objeto para um boolean
            var status = (bool)obj.SelectToken("success");

            //Verifica o status da requisição
            ViewBag.Mensagem = status ? "Captcha Validado com Sucesso !" : "Prove que você não é um robô, valide o captcha abaixo!";

            //Obtem o host que fez a requisição
            ViewBag.Hostname = obj.GetValue("hostname");

            return View("Index");
        }

    }
}