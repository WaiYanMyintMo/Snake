using CommandLine;
using System;

namespace Snake
{
    class Program
    {
        static void Main(params string[] args)
        {
            var options = new Options();

            var result = Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed(o => options = o);

            if (result.Tag is ParserResultType.NotParsed)
            {
            }
            else
            {
                Game.Run(options);
            }
        }
    }
}
