using System;

namespace Messenger.Models
{
	public class Message
	{
		public int Id {get; set;}
		public string Name {get; set;}
		public string Content {get; set;}
		public DateTime DatePosted {get; set;}
	}
}