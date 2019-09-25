using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace BaiduLinkSubmit
{

    public partial class UpData
    {
        /// <summary>
        /// 检测版本更新，并进行自动更新和下载
        /// </summary>
        public void updatedVersion(bool check)
        {
            try
            {
                string url = "http://www.xmmod.cn/BaiduLinkSubmit/index.php";
                double version = 1.3;

                WebClient MyWebClient = new WebClient();
                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据
                Byte[] pageData = MyWebClient.DownloadData(url); //从指定网站下载数据
                string pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句    
                                                                         //string pageHtml = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句

                //获取新版本
                Regex regVersion = new Regex("Version:(.+)\n");
                Match matchVersion = regVersion.Match(pageHtml);
                double newVersion = Convert.ToDouble(matchVersion.Groups[1].Value);

                //获取新下载地址
                Regex regdownloadLink = new Regex("downloadLink:(.+)");
                Match matchdownloadLink = regdownloadLink.Match(pageHtml);
                string downloadLink = matchdownloadLink.Groups[1].Value;

              

                if (newVersion > version)
                {
                    if (MessageBox.Show("检查的新版本，是否下载新版本？", "更新", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        System.Diagnostics.Process.Start(downloadLink);
                        System.Environment.Exit(0);
                    }

                }
                else
                {
                    if (check)
                    {
                        MessageBox.Show("当前已是最新版本","完成");
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("检测更新失败，请检查是否有网络连接","更新失败");
            }
            


        }
    }
}
