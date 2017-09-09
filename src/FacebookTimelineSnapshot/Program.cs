using System;
using System.IO;
using System.Net;
using System.Linq;
using AngleSharp.Dom;
using System.Collections.Generic;

using FacebookTimelineSnapshot.Http;

namespace FacebookTimelineSnapshot
{
    class Program
    {
        static CookieContainer cookieJar = new CookieContainer();
        static FacebookRequest request = new FacebookRequest(cookieJar);

        static void Main(string[] args)
        {
            Dictionary<string, string> inputData = new Dictionary<string, string>
            {
                { "email", Consts.FacebookLogin },
                { "pass", Consts.FacebookPassword }
            };

            Console.WriteLine("Entrando na pagina do facebook....");

            FacebookResponse loginPage = request.Get(Consts.MainUrl);
            IElement form = loginPage.Html.GetElementById("login_form");

            Console.WriteLine("Realizando login....");

            string body = BuildRequestBody(inputData, form);
            FacebookResponse facebookAuthentication = request.Post(Consts.AuthenticationUrl, body, false);

            if (!facebookAuthentication.IsAuthenticated)
            {
                Console.WriteLine("oops, usuário e/ou senha inválidos!");
                System.Threading.Thread.Sleep(1000);
                Environment.Exit(-1);
            }

            Console.WriteLine("Realizando donwload da timeline...");

            FacebookResponse userTimeline = request.Get(Consts.MainUrl);

            using (StreamWriter writer = new StreamWriter(Consts.TimelinePath))
            {
                writer.Write(userTimeline.Html.Source.Text);
            }
            
            Console.WriteLine($"Donwload realizado no diretório {Consts.TimelinePath}!");
            Console.ReadKey();
        }

        static string BuildRequestBody(Dictionary<string, string> inputData, IElement el)
        {
            IEnumerable<IElement> inputs = el.QuerySelectorAll("input")
                                             .Where(x => x.GetAttribute("name") != null &&
                                                    x.GetAttribute("value") != null);

            foreach (var input in inputs)
                inputData.Add(input.GetAttribute("name"), input.GetAttribute("value"));

            return string.Join("&", inputData.Select(s => $"{s.Key}={s.Value}"));
        }
    }
}
