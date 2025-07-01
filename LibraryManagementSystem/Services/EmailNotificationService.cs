using System;

namespace LibraryManagementSystem.Services
{
    public class EmailNotificationService : INotificationService
    {
        public void Notify(string message)
        {
            // Simulate sending an email
            Console.WriteLine($"[Email Notification] {message}");
        }
    }
} 