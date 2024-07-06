using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace TableOfContentGenerator
{
    public class CongUocProcessor : IProcessor
    {
        public void Process(Options options)
        {
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
        }

        private void ProcessChapters(DocX document)
        {
            var chapters = document.ParagraphsDeepSearch
                .Where(x => 
                    Regex.IsMatch(x.Text, CongUocConstants.Chapter1Regex)
                    || Regex.IsMatch(x.Text, CongUocConstants.Chapter2Regex))
                .ToList();
            foreach (var chapter in chapters)
            {
                chapter.Heading(HeadingType.Heading1)
                    .Font(new Font("Arial"))
                    .FontSize(10)
                    .Bold();
            }
        }

        private void ProcessSections(DocX document)
        {
            var sections = document.ParagraphsDeepSearch
                .Where(x => Regex.IsMatch(x.Text, CongUocConstants.SectionRegex))
                .ToList();
            foreach (var section in sections)
            {
                section.Heading(HeadingType.Heading2)
                    .Font(new Font("Arial"))
                    .FontSize(10)
                    .Bold();
            }
        }

        private void ProcessArticles(DocX document)
        {
            var articles = document.ParagraphsDeepSearch
                .Where(x => Regex.IsMatch(x.Text, CongUocConstants.ArticleRegex))
                .ToList();
            foreach (var article in articles)
            {
                article.Heading(HeadingType.Heading3)
                    .Font(new Font("Arial"))
                    .FontSize(10);
            }
        }
    }
}
