using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epam_task_4.Event
{
    public delegate void Result();
    class Event
    {
        public event Result ResultEvent;
        public void OnResultEvent()
        {
            ResultEvent();
        }
    }
}
