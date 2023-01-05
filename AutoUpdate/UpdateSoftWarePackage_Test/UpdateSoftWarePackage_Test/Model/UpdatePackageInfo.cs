/***
*	Title："数据采集" 项目
*		主题：升级包的信息
*	Description：
*		功能：XXX
*	Date：2021
*	Version：0.1版本
*	Author：Coffee
*	Modify Recoder：
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Model
{
    [XmlRoot]
    public class UpdatePackageInfo
    {
        /// <summary>
        /// 版本号
        /// </summary>
        [XmlElement]
        public string Version { get; set; }

        /// <summary>
        /// 更新包文件地址
        /// </summary>
        [XmlElement]
        public string UpdatePackageFileAddress { get; set; }

        /// <summary>
        /// 更新包配置地址
        /// </summary>
        [XmlElement]
        public string UpdatePackageConfigAddress { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        [XmlElement]
        public string FileName { get; set; }

        /// <summary>
        /// 文件的Hash值
        /// </summary>
        [XmlElement]
        public string FileHash { get; set; }


    }//Class_end

}
