using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SuperCarBookingSystem.Models;

namespace SuperCarBookingSystem.Controllers
{
    public class CarController : Controller
    {
        private readonly IMongoCollection<Car> _cars;

        public CarController(IMongoClient client, MogoDbSetting settings)
        {
            var database = client.GetDatabase(settings.DatabaseName);
            _cars = database.GetCollection<Car>("cars");
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _cars.Find(_ => true).ToListAsync();
            return View(cars);
        }

        public async Task<IActionResult> Details(ObjectId id)
        {
            var car = await _cars.Find(c => c.Id == id).FirstOrDefaultAsync();
            if (car == null)
                return NotFound();
            return View(car);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Car car)
        {
            if (ModelState.IsValid)
            {
                car.Id = ObjectId.GenerateNewId();
                await _cars.InsertOneAsync(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        public async Task<IActionResult> Edit(ObjectId id)
        {
            var car = await _cars.Find(c => c.Id == id).FirstOrDefaultAsync();
            if (car == null)
                return NotFound();
            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ObjectId id, Car car)
        {
            if (ModelState.IsValid)
            {
                var result = await _cars.ReplaceOneAsync(c => c.Id == id, car);
                if (result.ModifiedCount > 0)
                    return RedirectToAction(nameof(Index));
                else
                    return NotFound();
            }
            return View(car);
        }

        public async Task<IActionResult> Delete(ObjectId id)
        {
            var result = await _cars.DeleteOneAsync(c => c.Id == id);
            if (result.DeletedCount > 0)
                return RedirectToAction(nameof(Index));
            return NotFound();
        }
    }
}
    

