using Hotels_Booking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Numerics;

namespace Hotel_Booking.Controllers
{
    public class CrudController : Controller
    {
        public IActionResult list()
        {
            List<Online_Booking> list = new List<Online_Booking>();
            try
            {

                Online_Booking db = new Online_Booking();
                list = db.List();
                return View(list);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult Create()
        {
            Online_Booking Custo = new Online_Booking();
            ViewBag.lst = Custo.GetHotelName();
            return View();
        }


        public ActionResult Insert(Online_Booking Book)
        {
            try
            {
                Online_Booking Customer1 = new Online_Booking();
                Customer1.Insert(Book);

                return RedirectToAction("list", "Crud_");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        [HttpGet]

        public IActionResult Edit(Int32 C_Id)
        {
            using (Online_Booking Custom = new Online_Booking())
            {

                Online_Booking obj = Custom.SelectDataByID(C_Id);
                return View(obj);

            }

        }


        [HttpPost]


        public IActionResult Edit(Online_Booking md)
        {
            try

            {
                Online_Booking customer2 = new Online_Booking();
                customer2.Update(md);
                return RedirectToAction("list", "Crud_");
            }


            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }



        public IActionResult Delete(Int32 C_Id)
        {
            try
            {
                Online_Booking customer3 = new Online_Booking();
                customer3.Delete(C_Id);
                return RedirectToAction("list", "Crud_");

            }


            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




    }

}
