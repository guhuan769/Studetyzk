using DryIoc;
using Prism.Events;
using prismDemo1.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace prismDemo1.Views
{
    /// <summary>
    /// ChatView.xaml 的交互逻辑
    /// </summary>
    public partial class ChatView : UserControl
    {
        private readonly IEventAggregator aggregator;

        //订阅
        public ChatView(IEventAggregator aggregator)
        {
            InitializeComponent();
            //Subscribe 订阅
            //aggregator.GetEvent<MessageEvent>().Subscribe(arg =>
            //{
            //    MessageBox.Show($"接收到消息:{arg}");
            //});
            this.aggregator = aggregator;
            aggregator.GetEvent<MessageEvent>().Subscribe(SubMessage);
            

        }

        private void SubMessage(string obj)
        {
            MessageBox.Show($"接收到消息:{obj}");
            //取消订阅
            aggregator.GetEvent<MessageEvent>().Unsubscribe(SubMessage);
        }
    }
}
