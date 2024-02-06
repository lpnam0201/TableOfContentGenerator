using CommandLine;
using System.Text.RegularExpressions;
using TableOfContentGenerator;
using Xceed.Document.NET;
using Xceed.Words.NET;

var options = ReadOptions(args);

using (var fileStream = new FileStream(options.Input, FileMode.Open))
{
    using (var document = DocX.Load(fileStream))
    {
        ProcessChapters(document);
        ProcessSections(document);
        ProcessArticles(document);

        document.SaveAs(options.Output);
    }
}

static Options ReadOptions(string[] args)
{
    return Parser.Default.ParseArguments<Options>(args).Value;
}

static void ProcessChapters(DocX document)
{
    var chapters = document.ParagraphsDeepSearch
        .Where(x => Regex.IsMatch(x.Text, Constants.ChapterRegex))
        .ToList();
    foreach (var chapter in chapters)
    {
        chapter.Heading(HeadingType.Heading1)
            .Font(new Font("Arial"))
            .FontSize(10)
            .Bold();
    }
}

static void ProcessSections(DocX document)
{
    var sections = document.ParagraphsDeepSearch
        .Where(x => Regex.IsMatch(x.Text, Constants.SectionRegex))
        .ToList();
    foreach (var section in sections)
    {
        section.Heading(HeadingType.Heading2)
            .Font(new Font("Arial"))
            .FontSize(10)
            .Bold();
    }
}

static void ProcessArticles(DocX document)
{
    var articles = document.ParagraphsDeepSearch
        .Where(x => Regex.IsMatch(x.Text, Constants.ArticleRegex))
        .ToList();
    foreach (var article in articles)
    {
        article.Heading(HeadingType.Heading3)
            .Font(new Font("Arial"))
            .FontSize(10);
    }
}