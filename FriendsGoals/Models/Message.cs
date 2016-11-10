using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FriendsGoals.Models
{
	public class Message
	{
		public ProfileModel Sender { get; set; }
		public ProfileModel Recipient { get; set; }
		
		[Required(ErrorMessage = " ")]
		[DataType(DataType.MultilineText)]
		public string Text { get; set; }
	}
}