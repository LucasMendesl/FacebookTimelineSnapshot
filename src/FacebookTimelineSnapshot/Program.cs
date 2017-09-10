using System;
using System.IO;
using System.Net;
using System.Linq;
using CommandLine;
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
            try
            {
                CommandLineOptions options = new CommandLineOptions();

                if (Parser.Default.ParseArguments(args, options))
                {
                    Dictionary<string, string> inputData = new Dictionary<string, string>
                    {
                        { "email",  options.User },
                        { "pass", options.Password }
                    };

                    Console.WriteLine("Entrando na pagina do facebook....");

                    FacebookResponse loginPage = request.Get(Consts.MainUrl);
                    IElement form = loginPage.Html.GetElementById("login_form");

                    Console.WriteLine("Realizando login....");

                    string body = BuildRequestBody(inputData, form);
                    FacebookResponse facebookAuthentication = request.Post(Consts.AuthenticationUrl, body, false);

                    if (!facebookAuthentication.IsAuthenticated)
                        throw new UnauthorizedAccessException("Usuário e/ou senha inválidos!");

                    Console.WriteLine("Realizando donwload da timeline...");
                    FacebookResponse userTimeline = request.Get(Consts.MainUrl);

                    using (StreamWriter writer = new StreamWriter($"{options.DirectoryPath}\\{Consts.FileName}"))
                    {
                        writer.Write(userTimeline.Html.Source.Text);
                    }

                    Console.WriteLine($"Donwload realizado no diretório {options.DirectoryPath}!");
                }
                else
                {
                    Console.WriteLine(options.GetHelp());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ooops, ocorreu o seguinte erro: {e.Message}");
                Environment.Exit(-1);
            }

            Environment.Exit(0);
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
