namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ʹ���첽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                //await �ȴ��ô���ִ����Ϻ����
                string html = await httpClient.GetStringAsync("https://www.baidu.com");
                textBox1.Text = html.Substring(0, 20);
                //���߳���ʹ��Thread.Sleep(3000); �ᵼ��UI���ֿ���״̬  Thread��ռ����Դ
                //Thread.Sleep(3000);
                //ʹ��Task�Ͳ������UI���� Task��ռ��CPU��Դ
                await Task.Delay(3000);
                string html1 = await httpClient.GetStringAsync("https://www.bilibili.com/video/BV1pK41137He?p=18&spm_id_from=pageDriver&vd_source=d8a4c09db5e33f3ebcb6438e6fe66cd7");
                textBox1.Text = html1.Substring(20, 40);
            }
        }
    }
}