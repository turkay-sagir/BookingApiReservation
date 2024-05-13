using BookingApiReservation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using static BookingApiReservation.Models.HotelListViewModel;

namespace BookingApiReservation.Controllers
{
    public class BookHotelController : Controller
    {
		public async Task<string> GetDestinationId(string cityName)
		{
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"https://booking-com.p.rapidapi.com/v1/hotels/locations?name={cityName}&locale=tr"),
				Headers =
		{
			{ "X-RapidAPI-Key", "8c4457005amshee1c41afe87df5ep170466jsneb5fcaaaee25" },
			{ "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
		},
			};
			using (var response = await client.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				var locations = JsonConvert.DeserializeObject<List<LocationViewModel>>(body);
				if (locations != null && locations.Count > 0)
				{
					return locations[0].dest_id;
				}
				else
				{
					return null;
				}
			}
		}


		public async Task<List<HotelListViewModel.Result>> HotelList(SearchViewModel p)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com.p.rapidapi.com/v1/hotels/search?checkout_date={p.checkout}&order_by=popularity&filter_by_currency=TRY&room_number={p.room}&dest_id={p.dest_id}&dest_type=city&adults_number={p.guest}&checkin_date={p.checkin}&locale=tr&units=metric&include_adjacency=true&children_number={p.children}&categories_filter_ids=class%3A%3A2%2Cclass%3A%3A4%2Cfree_cancellation%3A%3A1&page_number=0&children_ages=5%2C0"),
                Headers =
    {
        { "X-RapidAPI-Key", "8c4457005amshee1c41afe87df5ep170466jsneb5fcaaaee25" },
        { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<HotelListViewModel>(body);
				return result.result.ToList();
			}
        }

        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Search(SearchViewModel p)
        {
            var destId = await GetDestinationId(p.city_name);
            if (destId==null)
            {
                return RedirectToAction("Error404","ErrorPage");
            }

            p.dest_id = destId;

            var hotels = await HotelList(p);
            if(hotels==null)
            {
                return RedirectToAction("Error404", "ErrorPage");
            }

            return View("Index",hotels);

        }



    }
}
