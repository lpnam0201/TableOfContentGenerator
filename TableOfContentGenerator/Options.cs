using CommandLine;

namespace TableOfContentGenerator
{
    public class Options
    {
        [Option("Input", Required = true)]
        public string Input { get; set; }
        [Option("Output", Required = true)]
        public string Output { get; set; }
    }
}
