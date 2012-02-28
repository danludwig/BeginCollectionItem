using System.Web.Mvc;
using BeginCollectionItem.Mvc3Demo.Models;

namespace BeginCollectionItem.Mvc3Demo.Controllers
{
    public partial class DemoController : Controller
    {
        [HttpGet]
        public virtual ViewResult Order()
        {
            var model = new OrderModel
            {
                Id = 100,
                Items = new[]
                {
                    NewOrderItemModel(),
                },
            };
            return View(model);
        }

        [HttpPost]
        public virtual ViewResult Order(OrderModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    return View(Views.OrderSubmitted, model);
            //}
            var productOptions = ProductOptions();
            foreach (var item in model.Items)
            {
                item.Product.Options = productOptions;
            }
            return View(model);
        }

        [NonAction]
        private static SelectListItem[] ProductOptions()
        {
            return new []
            {
                new SelectListItem{ Value = "1", Text = "Airport", },
                new SelectListItem{ Value = "3", Text = "Stadium", },
                new SelectListItem{ Value = "2", Text = "Skyscraper", },
            };
        }

        [NonAction]
        private static OrderModel.OrderItemModel NewOrderItemModel()
        {
            return new OrderModel.OrderItemModel
            {
                Product = new OrderModel.OrderItemModel.ProductModel
                {
                    Options = ProductOptions(),
                },
            };
        }

        [HttpGet]
        public virtual PartialViewResult OrderItem()
        {
            var model = NewOrderItemModel();
            return PartialView(Views._OrderItem, model);
        }
    }
}
