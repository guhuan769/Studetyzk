﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using prismDemo.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prismDemo.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;

        public DelegateCommand<string> OpenCommand { get; private set; }

        //private object body;

        //public object Body
        //{
        //    get { return body; }
        //    set { body = value; RaisePropertyChanged(); }
        //}

        /*
         因为在MainView中定了一个区域 因为使用了prism 所以此处可以直接拿 IRegionManager regionManager
         */
        public MainViewModel(IRegionManager regionManager)
        {
            OpenCommand = new DelegateCommand<string>(Open);
            this.regionManager = regionManager;
        }

        private void Open(string obj)
        {
            //Regions["ContentRegion"] 找到主页面的ContentRegion   设置目标 RequestNavigate 设置什么内容进去 
            //因为此处RequestNavigate("ViewA");肯定十找不到的所以 就要使用依赖注入的形式在app.cs里面注册
            //打开依赖注入的模块
            //首先通过IRegionManager接口获取当全局定义的可用区域 ， 往这个区域动态设置内容
            //设置内容的方式通过依赖注入的形式
            regionManager.Regions["ContentRegion"].RequestNavigate(obj);
            //switch (obj)
            //{
            //    case "ViewA":
            //        Body = new ViewA();
            //        break;
            //    case "ViewB":
            //        Body = new ViewB();
            //        break;
            //    case "ViewC":
            //        Body = new ViewC();
            //        break;
            //}
        }
    }
}
