using CommandLine;
using System.Text.RegularExpressions;
using TableOfContentGenerator;
using Xceed.Document.NET;
using Xceed.Words.NET;
using System.Threading.Tasks;

namespace TableOfContentGenerator
{
    public class LuatProcessor : IProcessor
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
                .Where(x => Regex.IsMatch(x.Text, LuatConstants.ChapterRegex))
                .ToList();
            foreach (var chapter in chapters)
            {
                // make Chapter text + Chapter number on same line
                var oldText = chapter.Text;

                var nextParagraph = chapter.NextParagraph;
                var newText = ComputeNewChapterText(oldText, nextParagraph.Text);

                var options = new StringReplaceTextOptions()
                {
                    SearchValue = oldText,
                    NewValue = newText
                };
                chapter.ReplaceText(options);
                nextParagraph.Remove(false);

                chapter.Heading(HeadingType.Heading1)
                    .Font(new Font("Arial"))
                    .FontSize(10)
                    .Bold();
            }
        }

        private string ComputeNewChapterText(string chapterNumber, string chapterName)
        {
            if (chapterNumber.EndsWith(": "))
            {
                return $"{chapterNumber}{chapterName}";
            }

            if (chapterNumber.EndsWith(":"))
            {
                return $"{chapterNumber} {chapterName}";
            }

            return $"{chapterNumber}: {chapterName}";
        }

        private void ProcessSections(DocX document)
        {
            var sections = document.ParagraphsDeepSearch
                .Where(x => Regex.IsMatch(x.Text, LuatConstants.SectionRegex))
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
                .Where(x => Regex.IsMatch(x.Text, LuatConstants.ArticleRegex))
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
