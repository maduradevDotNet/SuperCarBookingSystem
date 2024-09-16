using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SuperCarBookingSystem.Models
{
    [Collection("cars")]
    public class Car
    {
        public ObjectId Id { get; set; }

        [Required(ErrorMessage ="you must provide make and model")]
        [Display(Name ="Make and model")]
        public string? Model { get; set; }

        [Required(ErrorMessage ="The number plate is required to identify the vehicle")]
        [Display(Name ="Number plate")]
        public string NumberPlate { get; set; }

        [Required(ErrorMessage ="You must add the location of the car")]
        public string Location { get; set; }

        public bool IsBooked {  get; set; }=false;

    }
}
