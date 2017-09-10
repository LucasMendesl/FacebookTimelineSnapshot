using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookTimelineSnapshot
{
    public class CommandLineOptions
    {
        [Option('u', "usuario", Required = true, HelpText = "usuário do facebook")]
        public string User { get; set; }

        [Option('s', "senha", Required = true, HelpText = "senha do facebook")]
        public string Password { get; set; }

        [Option('d', "diretorio", HelpText = "caminho do diretório para download da linha do tempo")]
        public string DirectoryPath { get; set; }

        [HelpOption]
        public string GetHelp()
        {
            return HelpText.AutoBuild(this, current =>
                HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
