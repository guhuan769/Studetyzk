/***
*	Title："基础工具" 项目
*		主题：XML帮助类
*	Description：
*		功能：
*		    1、将对象序列化为xml文件且保存
*		    2、将对象序列化为xml字符串
*		    3、将xml文件反序列化为对象
*		    4、将xml字符串反序列化为对象
*	Date：2021
*	Version：0.1版本
*	Author：Coffee
*	Modify Recoder：
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Utils
{
    class XmlHelper
    {

        /// <summary>
        /// 将对象序列化为xml文件且保存
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="filePathAndName">xml存放路径和名称</param>
        /// <returns>True:表示成功</returns>
        public static bool ObjectToXml<T>(T t, string filePathAndName) where T : class
        {
            if (t == null || string.IsNullOrEmpty(filePathAndName)) return false;

            bool success = false;
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                using (FileStream stream = new FileStream(filePathAndName, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(stream, t);
                    success = true;
                }
            }
            catch (Exception ex)
            {
                success = false;
                throw new Exception(ex.Message);
            }
            return success;
        }

        /// <summary>
        /// 将对象序列化为xml字符串
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t">对象</param>
        public static string ObjectToXml<T>(T t) where T : class
        {
            if (t == null) return null;

            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                using (MemoryStream stream = new MemoryStream())
                {
                    formatter.Serialize(stream, t);
                    string result = System.Text.Encoding.UTF8.GetString(stream.ToArray());
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 将xml文件反序列化为对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="filePathAndName">xml文件的路径和名称</param>
        /// <returns>对象</returns>
        public static T XmlToObject<T>(T t, string filePathAndName) where T : class
        {
            if (t == null || string.IsNullOrEmpty(filePathAndName)) return null;

            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                using (FileStream stream = new FileStream(filePathAndName, FileMode.OpenOrCreate))
                {
                    XmlReader xmlReader = new XmlTextReader(stream);
                    T result = formatter.Deserialize(xmlReader) as T;
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 将xml字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="xmlStr">xml文件字符串</param>
        /// <returns></returns>
        public static T XmlToObject<T>(string xmlStr) where T : class
        {
            if (string.IsNullOrEmpty(xmlStr)) return null;

            try
            {
                using (StringReader sr = new StringReader(xmlStr))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(sr) as T;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }//Class_end

}
