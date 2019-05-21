using System;
using System.Text;
using System.Windows;
using System.Net;
using System.IO;

namespace BaiduLinkSubmit
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string api = this.api.Text;     //获取百度提供的API


            //使用POST进行提交
            var request = (HttpWebRequest)WebRequest.Create(api);   //提交链接地址

            var postData = this.Url.Text;  //获取需要提交的url链接,以换行分割


            //提交数据
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);

            }
            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();    //返回值
            
            this.returnData.Text = responseString;    //显示返回值

        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            About fm = new About();
            fm.Show();
        }
    }
}
