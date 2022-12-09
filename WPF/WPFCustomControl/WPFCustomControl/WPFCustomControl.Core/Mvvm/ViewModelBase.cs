﻿using Prism.Mvvm;
using Prism.Navigation;

namespace WPFCustomControl.Core.Mvvm
{
    public abstract class ViewModelBase : BindableBase, IDestructible
    {
        protected ViewModelBase()
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
