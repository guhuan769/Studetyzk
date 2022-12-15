using Prism.Ioc;
using Prism.Modularity;
using prismDemo1.ViewModels;
using prismDemo1.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prismDemo1
{
    public class prismDemo1Profile : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //配置大于约定 约定就是  ViewA 为前缀  拼接 ViewAViewModel
            //下面这句代码ViewA对应ViewAViewModel

            containerRegistry.RegisterForNavigation<ChatView, ChatViewViewModel>();
            containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>();
        }
    }
}
