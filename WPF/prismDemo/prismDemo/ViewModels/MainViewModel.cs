using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
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
        private readonly IDialogService dialogService;

        //声明区域导航日志
        public IRegionNavigationJournal region;

        public DelegateCommand<string> OpenCommand { get; private set; }

        public DelegateCommand BackCommand { get; private set; }
        public DelegateCommand<string> ChatCommand { get; private set; }

        //private object body;

        //public object Body
        //{
        //    get { return body; }
        //    set { body = value; RaisePropertyChanged(); }
        //}

        /*
         因为在MainView中定了一个区域 因为使用了prism 所以此处可以直接拿 IRegionManager regionManager
         */
        //public MainViewModel(IRegionManager regionManager)
        //{
        //    OpenCommand = new DelegateCommand<string>(Open);
        //    BackCommand = new DelegateCommand(Back);

        //    this.regionManager = regionManager;
        //}

        /// <summary>
        /// 弹窗
        /// </summary>
        /// <param name="dialogService"></param>
        public MainViewModel(IDialogService dialogService, IRegionManager regionManager)
        {

            OpenCommand = new DelegateCommand<string>(Open);
            BackCommand = new DelegateCommand(Back);

            this.regionManager = regionManager;

            ChatCommand = new DelegateCommand<string>(Chat);
            this.dialogService = dialogService;
        }

        private void Chat(string obj)
        {
            DialogParameters keyValuePairs = new DialogParameters();
            keyValuePairs.Add("Title", "Hello测试");
            dialogService.ShowDialog(obj, keyValuePairs, callback =>
            {
                if (callback.Result == ButtonResult.OK)
                {
                    callback.Parameters.GetValue<string>("value");
                }
            });
            //new ChatView().ShowDialog();
        }

        private void Back()
        {
            if (region == null)
                return;
            //能不能返回上一步
            if (region.CanGoBack)
                //上一步
                region.GoBack();

            //region.GoForward();
        }

        private void Open(string obj)
        {
            //是否可以通过导航属性传递参数过去 导航参数
            NavigationParameters keyValuePairs = new NavigationParameters();
            keyValuePairs.Add("Title", "Hello!");
            //Regions["ContentRegion"] 找到主页面的ContentRegion   设置目标 RequestNavigate 设置什么内容进去 
            //因为此处RequestNavigate("ViewA");肯定十找不到的所以 就要使用依赖注入的形式在app.cs里面注册
            //打开依赖注入的模块
            //首先通过IRegionManager接口获取当全局定义的可用区域 ， 往这个区域动态设置内容
            //设置内容的方式通过依赖注入的形式
            regionManager.Regions["ContentRegion"].RequestNavigate(obj, callBack =>
            {
                //判断是否为true
                if ((bool)callBack.Result)
                {
                    region = callBack.Context.NavigationService.Journal;
                }
            }, keyValuePairs);
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
