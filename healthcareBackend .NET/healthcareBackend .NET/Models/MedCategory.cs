using System.ComponentModel.DataAnnotations;

namespace healthcareBackend_.NET.Models
{
	public class MedCategory
	{
		[Key]
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
	}
}
