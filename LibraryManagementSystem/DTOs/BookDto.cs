using LibraryManagementSystem.Models;
using AutoMapper;

namespace LibraryManagementSystem.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsAvailable { get; set; }
        public int? BorrowedByUserId { get; set; }

        public BookDto(Book book)
        {
            Id = book.Id;
            Title = book.Title;
            Author = book.Author;
            IsAvailable = book.IsAvailable;
            BorrowedByUserId = book.BorrowedByUserId;
        }
    }

    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();
        }
    }
} 