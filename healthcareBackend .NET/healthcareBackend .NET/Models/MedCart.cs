using System.ComponentModel.DataAnnotations;

namespace healthcareBackend_.NET.Models
{
	public class MedCart
	{
		[Key]
		public int CartId { get; set; }
		public int UserId { get; set; } // Foreign key to User table
		public int ItemId { get; set; } // Foreign key to Item table
		public int Quantity { get; set; }

		public virtual MedItems MedItems { get; set; }
	}
}
