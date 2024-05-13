using Microsoft.AspNetCore.Mvc;

namespace BookingApiReservation.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult Error404()
        {
            return View();
        }
    }
}
