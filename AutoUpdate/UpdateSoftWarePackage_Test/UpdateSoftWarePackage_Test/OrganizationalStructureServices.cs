using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;


namespace UpdateSoftWarePackage_Test
{
    /// <summary>
    /// 调动OA组织架构
    /// </summary>
    public class OrganizationalStructureServices : ERPDomainServiceBase
    {
        string OAorganizationStructureUrl = "http://域名/SendAPI2/SendDataPort";          //路径+接口名称

        //string OAorganizationStructureUr2 = "http://IP地 址/SendAPI2/SendDataPort";

        public List<OrganizationStructureDto> GetJobDataGonGuan()
        {
            var json = GetPostData(OAorganizationStructureUrl);
            var datas = JsonConvert.DeserializeObject<OrganizationStructureDto>(json);          //OrganizationStructureDto 第三方接口返回数据的接收类
            var data = datas.Data;
            return data;
        }

        #region 通用
        /// <summary>
        /// 获取接口数据方法
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public string GetPostData(string Url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";     //GetRequestStream() 请求方式必须为Post否则导致异常
            request.ContentType = "application/json";
            string appid = "2";
            var time = DateTime.Now.ToString();
            var sign = GetCkeckKey(appid, time);
            //接口参数
            var result = new OAInterfaceParameters
            {
                appid = appid,
                time = time,
                sign = sign,
            };
            var results = result.ToJson();

            byte[] data = Encoding.UTF8.GetBytes(results);
            request.ContentLength = data.Length;

            Stream writer;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (System.Exception e)
            {
                writer = null;
                Console.Write("连接服务器失败:" + e.Message);
            }
            //将请求参数写入流
            writer.Write(data, 0, data.Length);
            writer.Close();//关闭请求流
                           // String strValue = "";//strValue为http响应所返回的字符流
            HttpWebResponse response;
            try
            {
                //获得响应流
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }
            Stream s = response.GetResponseStream();
            //  Stream postData = Request.InputStream;
            StreamReader sRead = new StreamReader(s);
            string postContent = sRead.ReadToEnd();
            sRead.Close();
            return postContent;//返回Json数据
        }


        //检测用户Key和加密算法接口是第三方系统需要验证的，把第三方的验证代码复制过来验证下就好；如果不需要  这两个回调可以忽略
        /// <summary>
        ///检测用户key 
        /// </summary>
        /// <param name="appid">appid，由开发商提供</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        private string GetCkeckKey(string appid, string time)
        {
            //验证
            var appsecret = "bmlrdept01";

            var _sign = md5_ex(appsecret + time + appid);//大写32位
            return _sign;

        }
        /// <summary>
        /// 加密算法
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string md5_ex(string str)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(str));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            Console.WriteLine(sBuilder);
            return sBuilder.ToString();
        }
        #endregion
    }
}






