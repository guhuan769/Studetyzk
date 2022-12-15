using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using prismDemo.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace prismDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        #region 默认实现
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注册（依赖注入） 注入ViewA
            //containerRegistry.RegisterForNavigation<ViewA>("View"); 自定义名称 如果不写就是默认 注册三个模块
            //containerRegistry.RegisterForNavigation<ViewA>();
            //containerRegistry.RegisterForNavigation<ViewB>();
            containerRegistry.RegisterForNavigation<ViewC>();
        }

        #region 模块化
        //protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        //{
        //    //此处是强引用 当然也可以通过不同的方式 不引用类库
        //    moduleCatalog.AddModule<prismDemo1Profile>();
        //    moduleCatalog.AddModule<prismDemo2Profile>();
        //    base.ConfigureModuleCatalog(moduleCatalog);
        //} 

        #region 读取文件夹
        protected override IModuleCatalog CreateModuleCatalog()
        {
            /*
                ModulePath 当前文件夹读取配置
            D:\Code\Studetyzk111\Studetyzk\WPF\prismDemo\prismDemo1\bin\Debug\net6.0-windows 在项目根目录下面创建文件夹 Modules
            ..\prismDemo\bin\Debug\net6.0-windows\Modules
             */
            IModuleCatalog mc = new DirectoryModuleCatalog() { ModulePath = $@"{AppDomain.CurrentDomain.BaseDirectory}\Modules\Debug" };
            return mc;
        }
        #endregion
        #endregion
        #endregion
    }
}
