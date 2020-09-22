using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    public class LampController : Controller
    {
        [HttpGet]
        public ActionResult Edit(int productID = 0)
        {
            Lamp lamp = new Lamp() { id = productID, Label = "Asus4510"};
            //use Base class's GetParameters method
            lamp.GetParameters();
            lamp.FillTheSelectLists();
            return View(lamp);
        }

        [HttpPost]
        public ActionResult Edit(Lamp lamp)
        {
            if (ModelState.IsValid)
            {
                //Access and save product info
                //lamp.id;
                //lamp.label; 

                //Refill the select list of the parameters 
                lamp.FillTheSelectLists();
                //use Base class's CheckAndUpdateParameters method
                ViewBag.message = lamp.CheckAndUpdateParameters(ModelState);
            }
            else
            {
                //to see modal state errors
                //IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return View(lamp);
        }
    }
}