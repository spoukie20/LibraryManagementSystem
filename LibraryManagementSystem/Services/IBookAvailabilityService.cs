namespace LibraryManagementSystem.Services
{
    public interface IBookAvailabilityService
    {
        Task<bool> IsBookAvailableAsync(int bookId);
    }
} 