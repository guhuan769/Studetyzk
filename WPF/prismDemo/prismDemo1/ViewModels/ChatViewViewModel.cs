using ImTools;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using prismDemo1.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prismDemo1.ViewModels
{
    //继承IdialogAware 属于弹窗 对话服务
    public class ChatViewViewModel : BindableBase, IDialogAware
    {
        private readonly IEventAggregator aggregator;

        public DelegateCommand CanelCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }
        public ChatViewViewModel(IEventAggregator aggregator)
        {
            CanelCommand = new DelegateCommand(Canel);
            SaveCommand = new DelegateCommand(Save);
            this.aggregator = aggregator;
            //发布  使用messageEvent 发布一个Hello

        }

        private void Save()
        {
            aggregator.GetEvent<MessageEvent>().Publish("Publish Hello");
            OnDialogClosed();
            //RequestClose.Invoke(new DialogResult(ButtonResult.Yes));
        }

        private void Canel()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));
        }

        public string Title { get; set; }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            DialogParameters keyValuePairs = new DialogParameters();
            keyValuePairs.Add("value", "Hello");
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, keyValuePairs));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.GetValue<string>("Title");
        }
    }
}
