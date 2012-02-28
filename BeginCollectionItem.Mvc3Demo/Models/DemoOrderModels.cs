using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BeginCollectionItem.Mvc3Demo.Models
{
    public class OrderModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Order Number")]
        [Required]
        public string Number { get; set; }

        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required]
        public DateTime? Date { get; set; }

        public OrderItemModel[] Items { get; set; }
        public class OrderItemModel
        {
            [Display(Name = "Quantity")]
            [Required(ErrorMessage = "Please select a quantity.")]
            [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
            public int? Quantity { get; set; }

            public ProductModel Product { get; set; }
            public class ProductModel
            {
                [Display(Name = "Product")]
                [Required(ErrorMessage = "Please select a product.")]
                public int? Id { get; set; }
                public SelectListItem[] Options { get; set; }
            }
        }
    }
}