using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using NLog;
using TM.Persistence.Migrator.CLI.Commands;
using TM.Persistence.Migrator.CLI.Repositories;

namespace TM.Persistence.Migrator.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //args = new string[] { "migration" };
            args = new string[] { "generate" };

            var app = new CommandLineApplication(throwOnUnexpectedArg: false);
            app.HelpOption("-?|-h |--help");
            app.OnExecute(() =>
            {
                app.ShowHelp();
                return 1;
            });

            var logger = LogManager.GetCurrentClassLogger();
            var config = JsonConvert.DeserializeObject<Config>(
                  File.ReadAllText("config.json", Encoding.UTF8)
               );

            var context = ContextFactory.CreateContext(config.DbConfig);

            MigrationCommand.Register(app, logger, context);
            GenerateCommand.Register(app, logger, context);

            try
            {
                Environment.ExitCode = app.Execute(args);
            }
            catch (CommandParsingException ex)
            {
                Console.WriteLine(ex.Message);
                app.ShowHelp();
                Environment.ExitCode = -1;
            }
        }
    }
}
