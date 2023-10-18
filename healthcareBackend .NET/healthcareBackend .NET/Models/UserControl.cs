using System.ComponentModel.DataAnnotations;

namespace healthcareBackend_.NET.Models
{
	public class UserControl
	{
		[Key]
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Access { get; set; }
	}
}
