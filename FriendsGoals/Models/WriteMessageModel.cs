using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FriendsGoals.Models
{
	public class WriteMessageModel
	{
		public AppUser Sender { get; set; }

		[Required(ErrorMessage = " ")]
		public string RecipientId { get; set; }

		[Required(ErrorMessage = " ")]
		[DataType(DataType.MultilineText)]
		public string Text { get; set; }
	}
}