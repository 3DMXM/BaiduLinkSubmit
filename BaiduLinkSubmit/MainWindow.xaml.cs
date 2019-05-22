using System;
using System.Text;
using System.Windows;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

using System.Xml.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;


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

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string api = this.api.Text;     //获取百度提供的API

            string str1 = "API=" + api + "\n";
            str1 += "Sitemap=" + this.Sitemap.Text ;
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

        private void SitemapBut_Click(object sender, RoutedEventArgs e)
        {
            string Sitemap = this.Sitemap.Text;     //获取sitemap链接

            //将XML文件加载进来
            XDocument document = XDocument.Load(Sitemap);
            //获取到XML的根元素进行操作

            /*
             * 
             *  XElement root = document.Root;
            XElement ele = root.Element("url1").Element("loc");
            System.Xml.Linq.XDocument xdoc = System.Xml.Linq.XDocument.Load("person.xml");//读取xml文件
            System.Xml.Linq.XElement xeRoot = xdoc.Root; //根节点 
            System.Xml.Linq.XElement xele = xeRoot.Element("P1").Element("Person"); //子节点
            MessageBox.Show("id=" + xele.Attribute("id").Value);  //cz001

            */



        }
    }
}
