using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prismDemo1.Event
{
    /*
        发布订阅
     */
    public class MessageEvent : PubSubEvent<string>
    {
        public MessageEvent() { }
    }

    //带实体对象的订阅服务
    public class TestEvent : PubSubEvent<Test>
    {
    }

    public class Test { public int MyProperty { get; set; } }
}
