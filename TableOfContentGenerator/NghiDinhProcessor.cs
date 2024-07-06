using CommandLine;
using System.Text.RegularExpressions;
using TableOfContentGenerator;
using Xceed.Document.NET;
using Xceed.Words.NET;
using System.Threading.Tasks;

namespace TableOfContentGenerator
{
    internal class NghiDinhProcessor : IProcessor
    {
        public void Process(Options options)
        {
            using (var fileStream = new FileStream(options.Input, FileMode.Open))
            {
                using (var document = DocX.Load(fileStream))
                {
                    ProcessArticles(document);
                    ProcessWhatChanges(document);

                    document.SaveAs(options.Output);
                }
            }
        }

        private void ProcessArticles(DocX document)
        {
            var articles = document.ParagraphsDeepSearch
                .Where(x => Regex.IsMatch(x.Text, NghiDinhConstants.ArticleRegex))
                .ToList();
            foreach (var article in articles)
            {
                article.Heading(HeadingType.Heading1)
                    .Font(new Font("Arial"))
                    .FontSize(10);
            }
        }

        private void ProcessWhatChanges(DocX document)
        {
            var whatChanges = document.ParagraphsDeepSearch
                .Where(x => 
                    Regex.IsMatch(x.Text, NghiDinhConstants.SuaDoiRegex)
                    || Regex.IsMatch(x.Text, NghiDinhConstants.SuaDoiBoSungRegex)
                    || Regex.IsMatch(x.Text, NghiDinhConstants.BoSungRegex))
                .ToList();
            foreach (var whatChange in whatChanges)
            {
                whatChange.Heading(HeadingType.Heading2)
                    .Font(new Font("Arial"))
                    .FontSize(10);
            }
        }
    }
}
