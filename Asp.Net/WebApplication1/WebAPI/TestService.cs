namespace WebAPI
{
    public class TestService
    {
        private string[] file;
        public TestService()
        {
            this.file = Directory.GetFiles("D:\\BaiduNetdiskDownload", "*.exe", SearchOption.AllDirectories);
        }

        public int Count { get { return file.Length; } }
    }
}
