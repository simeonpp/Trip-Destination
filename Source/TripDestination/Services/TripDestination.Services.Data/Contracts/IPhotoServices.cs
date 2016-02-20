using TripDestination.Data.Models;

namespace TripDestination.Services.Data.Contracts
{
    public interface IPhotoServices
    {
        Photo GetByFileName(string fileName);
    }
}
