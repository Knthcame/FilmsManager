using FilmsManager.Models;
using Prism.Events;

namespace FilmsManager.Events
{
	public class PickImageEvent : PubSubEvent<PickImageModel>
    {
    }
}
