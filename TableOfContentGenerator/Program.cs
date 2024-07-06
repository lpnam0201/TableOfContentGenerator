using CommandLine;
using System.Text.RegularExpressions;
using TableOfContentGenerator;
using Xceed.Document.NET;
using Xceed.Words.NET;

var options = ReadOptions(args);


Options ReadOptions(string[] args)
{
    return Parser.Default.ParseArguments<Options>(args).Value;
}

IProcessor processor;
switch(options.Mode)
{
    case ProcessingMode.Luat:
        new LuatProcessor().Process(options);
        break;
    case ProcessingMode.NghiDinh:
        new NghiDinhProcessor().Process(options);
        break;
    case ProcessingMode.CongUoc:
        new CongUocProcessor().Process(options);
        break;
    default:
        throw new NotSupportedException("Unrecognized mode");
}