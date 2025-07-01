using System;

namespace LibraryManagementSystem.Services
{
    public class LogNotificationService : INotificationService
    {
        public void Notify(string message)
        {
            Console.WriteLine($"[Notification] {message}");
        }
    }
} 