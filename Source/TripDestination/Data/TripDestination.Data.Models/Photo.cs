namespace TripDestination.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Photo
    {
        [Index]
        public int Id { get; set; }
    }
}