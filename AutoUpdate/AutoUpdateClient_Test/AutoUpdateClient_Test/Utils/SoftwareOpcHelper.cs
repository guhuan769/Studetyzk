/***
*	Title："基础工具" 项目
*		主题：软件操作帮助类
*	Description：
*		功能：
*		    1、启动软件进程
*		    2、关闭软件进程
*		    3、释放软件进程资源
*	Date：2021
*	Version：0.1版本
*	Author：Coffee
*	Modify Recoder：
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Utils
{
    class SoftwareOPCHelper
    {

        /// <summary>
        /// 获取到本程序进程
        /// </summary>
        /// <returns>返回本程序进程</returns>
        internal static Process GetThisProcess()
        {
            return Process.GetCurrentProcess();
        }

        /// <summary>
        /// 获取到本程序相同的所有进程
        /// </summary>
        /// <param name="processName">进程名称</param>
        /// <returns>返回本程序相同的所有进程</returns>
        internal static Process[] GetAllSameThisProcess(string processName)
        {
            return  Process.GetProcessesByName(processName);
        }

        /// <summary>
        /// 启动软件进程
        /// </summary>
        /// <param name="softwarePathAndName">需要启动的软件路径和名称（比如：c:\software\update.exe）</param>
        /// <param name="args">进程参数</param>
        /// <returns>返回结果（true：表示成功）</returns>
        internal static bool Start(string softwarePathAndName,string args)
        {
            if (string.IsNullOrEmpty(softwarePathAndName)) return false;

            try
            {
                Process process = new Process();
                process.StartInfo.FileName = softwarePathAndName;
                process.StartInfo.Arguments = args;
                bool result = process.Start();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        /// <summary>
        /// 启动软件进程
        /// </summary>
        /// <param name="process">进程</param>
        /// <param name="args">进程参数</param>
        /// <returns>返回结果（true：表示成功）</returns>
        internal static bool Start(Process process, string args)
        {
            if (process==null) return false;

            try
            {
                process.StartInfo.Arguments = args;
                bool result = process.Start();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        /// <summary>
        /// 关闭软件进程
        /// </summary>
        /// <param name="softwarePathAndName">需要启动的软件路径和名称（比如：c:\software\update.exe）</param>
        /// <returns>返回结果（true：表示成功）</returns>
        internal static bool Kill(string softwarePathAndName)
        {
            if (string.IsNullOrEmpty(softwarePathAndName)) return false;

            try
            {
                Process process = new Process();
                process.StartInfo.FileName = softwarePathAndName;
                process.Kill();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// 关闭软件进程
        /// </summary>
        /// <param name="process">进程</param>
        /// <returns>返回结果（true：表示成功）</returns>
        internal static bool Kill(Process process)
        {
            if (process==null) return false;

            try
            {
                process.Kill();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// 释放软件进程资源
        /// </summary>
        /// <param name="softwarePathAndName">需要启动的软件路径和名称（比如：c:\software\update.exe）</param>
        /// <returns>返回结果（true：表示成功）</returns>
        internal static bool RelaseResource(string softwarePathAndName)
        {
            if (string.IsNullOrEmpty(softwarePathAndName)) return false;

            try
            {
                Process process = new Process();
                process.StartInfo.FileName = softwarePathAndName;
                process.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 释放软件进程资源
        /// </summary>
        /// <param name="process">进程</param>
        /// <returns>返回结果（true：表示成功）</returns>
        internal static bool RelaseResource(Process process)
        {
            if (process==null) return false;

            try
            {
                process.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 完全关闭软件进行和释放资源
        /// </summary>
        /// <param name="softwarePathAndName">需要启动的软件路径和名称（比如：c:\software\update.exe）</param>
        /// <returns></returns>
        internal static bool Dispose(string softwarePathAndName)
        {
            bool result = false;
            result = Kill(softwarePathAndName);
            if (!result) return false;
            result = RelaseResource(softwarePathAndName);
            return result;
        }

        /// <summary>
        /// 完全关闭软件进行和释放资源
        /// </summary>
        /// <param name="process">进程</param>
        /// <returns></returns>
        internal static bool Dispose(Process process)
        {
            bool result = false;
            result = Kill(process);
            if (!result) return false;
            result = RelaseResource(process);
            return result;
        }


        /// <summary>
        /// 获取所有进程
        /// </summary>
        /// <returns></returns>
        internal static Process[] GetAllProcess()
        {
            Process[] processes = Process.GetProcesses();
            return processes;
        }

        /// <summary>
        /// 获取指定进程
        /// </summary>
        /// <param name="processName">进程名称</param>
        /// <returns>返回当前的进程</returns>
        internal static Process GetProcess(string processName)
        {
            if (string.IsNullOrEmpty(processName)) return null;

            if (GetAllProcess()!=null || GetAllProcess().Length<=0)
            {
                foreach (var process in GetAllProcess())
                {
                    if (process.ProcessName.Contains(processName))
                    {
                        return process;
                    }
                }
            }

            return null;
        }


    }//Class_end

}
