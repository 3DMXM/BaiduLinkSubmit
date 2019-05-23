using System;
using System.Text;
using System.Windows;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

using System.Xml.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;


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
            try
            {
                string str = "";
                str = File.ReadAllText("API.ini");   //获取本地数据

                //API数据
                Regex regAPI = new Regex("API=(.+)\n");
                Match matchAPI = regAPI.Match(str);
                String strAPI = matchAPI.Groups[1].Value;
                this.api.Text = strAPI;

                //sitemap数据
                Regex regSitemap = new Regex("Sitemap=(.+)");
                Match matchSitemap = regSitemap.Match(str);
                String strSitemap = matchSitemap.Groups[1].Value;
                this.Sitemap.Text = strSitemap;
            }
            catch (Exception)
            {
                MessageBox.Show("未找到API.ini文件,请确认文件是否存在\n或请解压后运行");
            }
            



        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string api = this.api.Text;     //获取百度提供的API
                                    
            Regex reg = new Regex("data.zz.baidu.com(.+)site=(.+)token=");
            Regex reg2 = new Regex("data.zz.baidu.com(.+)appid=(.+)token=(.+)type=");


            if (reg.IsMatch(api) || reg2.IsMatch(api))
            {
                if (this.Url.Text != "")
                {
                    //提交数据

                    try
                    {
                        //使用POST进行提交
                        var request = (HttpWebRequest)WebRequest.Create(api);   //提交链接地址

                        var postData = this.Url.Text;  //获取需要提交的url链接,以换行分割

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

                        string str1 = "API=" + api + "\n";  //保存提交的数据
                        str1 += "Sitemap=" + this.Sitemap.Text;
                        File.WriteAllText("API.ini", str1);
                    }
                    catch (WebException ex)
                    {
                        //如果错误
                        MessageBox.Show("错误:"+ex);                        
                    }
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

        private void SitemapBut_Click(object sender, RoutedEventArgs e)
        {
            string Sitemap = this.Sitemap.Text;     //获取sitemap链接

            this.Url.Text = "获取中，请稍后....";

            try
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(Sitemap);
                var c = xd["urlset"].ChildNodes;

                string loc = "";
                for (int i = 0; i < c.Count; i++)
                {
                    loc += c[i]["loc"].InnerXml + "\n";
                }
                this.Url.Text = loc;

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:"+ex);
              
            }

        }
       
    }
}
