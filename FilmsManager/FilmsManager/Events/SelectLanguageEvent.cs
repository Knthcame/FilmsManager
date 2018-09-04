using FilmsManager.Models;
using Prism.Events;

namespace FilmsManager.Events
{ 
	public class SelectLanguageEvent : PubSubEvent<LanguageModel>
    {
    }
}
