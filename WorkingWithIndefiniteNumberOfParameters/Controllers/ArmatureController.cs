using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    public class ArmatureController : Controller
    {
        [HttpGet]
        public ActionResult Edit(int productID = 0)
        {
            Armature armature = new Armature() { id = productID, Label = "Asus4510", ArmatureName="Laminated Armature"};
            //use Base class's GetParameters method
            armature.GetParameters();
            armature.FillTheSelectLists();
            return View(armature);
        }

        [HttpPost]
        public ActionResult Edit(Armature armature)
        {
            if (ModelState.IsValid)
            {
                //Access and save product info
                //armature.id;
                //armature.armatureName
                //..

                //Refill the select list of the parameters (thanks to selectListCSV property of the parameter returned from the view) again
                armature.FillTheSelectLists();
                //use Base class's CheckAndUpdateParameters method
                ViewBag.message = armature.CheckAndUpdateParameters(ModelState);
                return View(armature);
            }
            else
            {
                //to see modal state errors
                //IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return View(armature);
        }
    }
}