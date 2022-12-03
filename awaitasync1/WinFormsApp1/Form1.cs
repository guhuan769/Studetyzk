namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 使用异步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                //await 等待该代码执行完毕后继续
                string html = await httpClient.GetStringAsync("https://www.baidu.com");
                textBox1.Text = html.Substring(0, 20);
                //主线程中使用Thread.Sleep(3000); 会导致UI出现卡死状态  Thread不占用资源
                //Thread.Sleep(3000);
                //使用Task就不会出现UI假死 Task会占用CPU资源
                await Task.Delay(3000);
                string html1 = await httpClient.GetStringAsync("https://www.bilibili.com/video/BV1pK41137He?p=18&spm_id_from=pageDriver&vd_source=d8a4c09db5e33f3ebcb6438e6fe66cd7");
                textBox1.Text = html1.Substring(20, 40);
            }
        }
    }
}