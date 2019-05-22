using System;
using System.Text;
using System.Windows;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

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
            string str = "";
            str = File.ReadAllText("API.ini");            
            this.api.Text = str;
           
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string api = this.api.Text;     //获取百度提供的API

            string str1 = api;

            File.WriteAllText("API.ini", str1);

            //使用POST进行提交
            var request = (HttpWebRequest)WebRequest.Create(api);   //提交链接地址

            var postData = this.Url.Text;  //获取需要提交的url链接,以换行分割

            Regex reg = new Regex("data.zz.baidu.com(.+)site=(.+)token=");
            Regex reg2 = new Regex("data.zz.baidu.com(.+)appid=(.+)token=(.+)type=");


            if (reg.IsMatch(api) || reg2.IsMatch(api))
            {
                if (this.Url.Text != "")
                {
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
                else
                {
                    MessageBox.Show("请输入URL链接");
                }
            }
            else
            {
                MessageBox.Show("请输入正确的接口地址");
            }


            
           

        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            About fm = new About();
            fm.Show();
        }
       
    }
}
