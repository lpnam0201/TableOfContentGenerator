namespace TableOfContentGenerator
{
    public static class LuatConstants
    {
        public const string ChapterRegex = @"Chương \w+";
        public const string SectionRegex = @"MỤC \d+\.";
        public const string ArticleRegex = @"Điều \d+\.";
    }

    public static class NghiDinhConstants
    {
        public const string ArticleRegex = @"Điều \d+\.";
        public const string SuaDoiRegex = @"\d+ Sửa đổi";
        public const string SuaDoiBoSungRegex = @"\d+ Sửa đổi, bổ sung";
        public const string BoSungRegex = @"\d+ Bổ sung";
    }

    public static class CongUocConstants
    {
        public const string Chapter1Regex = @"^Phần [A-Z]+$";
        public const string Chapter2Regex = @"^PHẦN [A-Z]+$";
        public const string SectionRegex = @"Mục \d+\.";
        public const string ArticleRegex = @"ĐIỀU \d+\.";
    }
}
