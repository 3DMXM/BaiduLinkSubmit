using System.Windows;

namespace BaiduLinkSubmit
{
    /// <summary>
    /// About.xaml 的交互逻辑
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
            this.textBox.Text = "1.到百度资源平台(https://ziyuan.baidu.com/linksubmit/index)复制你的接口地址\n2.将要提交的链接粘贴进来\n3.点击'提交'按钮提交链接\n注意：链接是一行一个";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            new UpData().updatedVersion(true);
        }
    }
}
