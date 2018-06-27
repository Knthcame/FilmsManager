using FilmsManager.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmsManager.Events
{
    public class PickImageEvent : PubSubEvent<PickImageModel>
    {
    }
}
