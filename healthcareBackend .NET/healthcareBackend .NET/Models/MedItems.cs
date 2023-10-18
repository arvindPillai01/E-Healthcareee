using System.ComponentModel.DataAnnotations;

namespace healthcareBackend_.NET.Models
{
	public class MedItems
	{
		[Key]
		public int ItemId { get; set; }
		public int CategoryId { get; set; } // Foreign key to Category table
		public string ItemName { get; set; }
		public decimal Price { get; set; }
		public string ImageUrl { get; set; }
		public string Seller { get; set; }
		public string Description { get; set; }


		//referencing the medcategory to use in item
		public virtual MedCategory MedCategory{ get; set; }
	}
}
