using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SuperCarBookingSystem.Models;

namespace SuperCarBookingSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly IMongoCollection<Booking> _bookings;

        public BookingController(IMongoClient client, MogoDbSetting settings)
        {
            var database = client.GetDatabase(settings.DatabaseName);
            _bookings = database.GetCollection<Booking>("bookings");
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _bookings.Find(_ => true).ToListAsync();
            return View(bookings);
        }

        public async Task<IActionResult> Details(ObjectId id)
        {
            var booking = await _bookings.Find(b => b.Id == id).FirstOrDefaultAsync();
            if (booking == null)
                return NotFound();
            return View(booking);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                booking.Id = ObjectId.GenerateNewId();
                await _bookings.InsertOneAsync(booking);
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        public async Task<IActionResult> Edit(ObjectId id)
        {
            var booking = await _bookings.Find(b => b.Id == id).FirstOrDefaultAsync();
            if (booking == null)
                return NotFound();
            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ObjectId id, Booking booking)
        {
            if (ModelState.IsValid)
            {
                var result = await _bookings.ReplaceOneAsync(b => b.Id == id, booking);
                if (result.ModifiedCount > 0)
                    return RedirectToAction(nameof(Index));
                else
                    return NotFound();
            }
            return View(booking);
        }

        public async Task<IActionResult> Delete(ObjectId id)
        {
            var result = await _bookings.DeleteOneAsync(b => b.Id == id);
            if (result.DeletedCount > 0)
                return RedirectToAction(nameof(Index));
            return NotFound();
        }
    }
}
