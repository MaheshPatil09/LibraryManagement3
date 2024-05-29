using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using LibraryManagement.BookModel;
using LibraryManagement.BookEntity;
using LibraryManagement.StudentModel;
using LibraryManagement.StudentEntity;
using LibraryManagement.IssueModel;
using LibraryManagement.IssueEntity;
namespace WebApplication3.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class LibraryMangement : Controller
    {
        public Container Container;
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "LibrayManagement";
        public string ContainerName = "Student";

        private Container GetContainer()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database database = cosmosClient.GetDatabase(DatabaseName);
            Container container = database.GetContainer(ContainerName);
            return container;

        }
        public LibraryMangement()
        {
            Container = GetContainer();
        }
        //FOR BOOK 
        [HttpPost]
        public async Task<BookModel> AddBook(BookModel bookModel)
        {
            BookEntity bookEntity = new BookEntity();
            bookEntity.Title = bookModel.Title;
            bookEntity.Author = bookModel.Author;
            bookEntity.PublishedDate = bookModel.PublishedDate;
            bookEntity.IsIssued = bookModel.IsIssued;
            bookEntity.ISBN = bookModel.ISBN;
            bookEntity.Id = Guid.NewGuid().ToString();
            bookEntity.UId = bookEntity.Id;
            bookEntity.DocumentType = "Book";
            bookEntity.CreatedBy = "Om";
            bookEntity.CreatedOn = DateTime.Now;
            bookEntity.UpdatedBy = "Sahil";
            bookEntity.UpdatedOn = DateTime.Now;
            bookEntity.Active = true;
            bookEntity.Archieved = false;

            BookEntity response = await Container.CreateItemAsync(bookEntity);
            BookModel bookModel1 = new BookModel();
            bookModel1.UId = response.UId;
            bookModel1.Title = response.Title;
            bookModel1.Author = response.Author;
            bookModel1.PublishedDate = response.PublishedDate;
            bookModel1.ISBN = response.ISBN;
            bookModel1.IsIssued = response.IsIssued;
            return bookModel1;
        }
        [HttpGet]
        public async Task<BookModel> GetBookByName(string Title)
        {
            var book = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.Title == Title && q.Active == true && q.Archieved == false).FirstOrDefault();
            BookModel bookModel = new BookModel();
            bookModel.Title = book.Title;
            bookModel.Author = book.Author;
            bookModel.PublishedDate = book.PublishedDate;
            bookModel.ISBN = book.ISBN;
            bookModel.IsIssued = book.IsIssued;
            return bookModel;
        }
        [HttpGet]
        public async Task<List<BookModel>> BookNotIssued()
        {
            var book = Container.GetItemLinqQueryable<BookModel>(true).ToList();
            List<BookModel> bookModel = new List<BookModel>();
            foreach (var book1 in book)
            {
                BookModel bookModel1 = new BookModel();
                if (book1.IsIssued == false)
                {
                    bookModel1.Author = book1.Author;
                    bookModel1.Title = book1.Title;
                    bookModel1.ISBN = book1.ISBN;
                    bookModel1.PublishedDate = book1.PublishedDate;
                }
                bookModel.Add(bookModel1);
            }
            return bookModel;
        }
        [HttpGet]
        public async Task<List<BookModel>> BookIssued()
        {
            var book = Container.GetItemLinqQueryable<BookModel>(true).ToList();
            List<BookModel> bookModel = new List<BookModel>();
            foreach (var book1 in book)
            {
                BookModel bookModel1 = new BookModel();
                if (book1.IsIssued == true)
                {
                    bookModel1.Author = book1.Author;
                    bookModel1.Title = book1.Title;
                    bookModel1.ISBN = book1.ISBN;
                    bookModel1.PublishedDate = book1.PublishedDate;
                }
                bookModel.Add(bookModel1);
            }
            return bookModel;
        }

        [HttpGet]
        public async Task<List<BookModel>> GetAllBook()
        {
            var book = Container.GetItemLinqQueryable<BookModel>(true).ToList();

            return book;
        }
        [HttpGet]

        public async Task<BookModel> GetBookByUId(string UId)
        {
            var student = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.UId == UId && q.Active == true && q.Archieved == false).FirstOrDefault();
            BookModel bookModel = new BookModel();
            bookModel.UId = student.UId;
            bookModel.Title = student.Title;
            bookModel.Author = student.Author;
            bookModel.PublishedDate = student.PublishedDate;
            bookModel.ISBN = student.ISBN;
            bookModel.IsIssued = student.IsIssued;
            return bookModel;
        }
        [HttpPost]

        public async Task<BookModel> UpdateBook(BookModel bookModel)
        {
            var book = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.UId == bookModel.UId && q.Active == true && q.Archieved == false).FirstOrDefault();
            book.Archieved = true;
            await Container.ReplaceItemAsync(book, book.UId);
            book.Id = Guid.NewGuid().ToString();
            book.UpdatedBy = "Prajwal";
            book.UpdatedOn = DateTime.Now;
            book.Version = book.Version + 1;
            book.Active = true;
            book.Archieved = false;
            book.Title = bookModel.Title;
            book.Author = bookModel.Author;
            book.PublishedDate = bookModel.PublishedDate;
            book.ISBN = bookModel.ISBN;
            book.IsIssued |= bookModel.IsIssued;
            book = await Container.CreateItemAsync(book);
            BookModel bookModel1 = new BookModel();
            bookModel1.UId = book.UId;
            bookModel1.Title = book.Title;
            bookModel1.Author = book.Author;
            bookModel1.ISBN = book.ISBN;
            bookModel1.PublishedDate = book.PublishedDate;
            bookModel1.IsIssued |= book.IsIssued;
            return bookModel1;

        }
        // FOR MEMBER
        [HttpPost]
        public async Task<StudentModel> AddStudent(StudentModel student)
        {
            StudentEntity studentEntity = new StudentEntity();
            studentEntity.Id = Guid.NewGuid().ToString();
            studentEntity.UId = studentEntity.Id;
            studentEntity.DocumentType = "student";
            studentEntity.CreatedBy = "Mahesh";
            studentEntity.CreatedOn = DateTime.Now;
            studentEntity.UpdatedBy = "Sujeet";
            studentEntity.UpdatedOn = DateTime.Now;
            studentEntity.Active = true;
            studentEntity.Archieved = false;
            studentEntity.Name = student.Name;
            studentEntity.DateOfBirth = student.DateOfBirth;
            studentEntity.Email = student.Email;

            var student1 = await Container.CreateItemAsync(studentEntity);
            StudentModel studentModel = new StudentModel();
            studentModel.UId = student.UId;
            studentModel.Name = student.Name;
            studentModel.DateOfBirth = student.DateOfBirth;
            studentModel.Email = student.Email;
            return studentModel;

        }
        [HttpGet]
        public async Task<List<StudentModel>> GetAll()
        {
            var student = Container.GetItemLinqQueryable<StudentEntity>(true).Where(q => q.Active == true && q.Archieved == false && q.DocumentType == "student");
            List<StudentModel> students = new List<StudentModel>();
            foreach (var student1 in student)
            {
                StudentModel model = new StudentModel();
                model.UId = student1.UId;
                model.Name = student1.Name;
                model.DateOfBirth = student1.DateOfBirth;
                model.Email = student1.Email;
                students.Add(model);
            }
            return students;
        }
        [HttpGet]
        public async Task<StudentModel> GetStudentByUId(string UID)
        {
            var student = Container.GetItemLinqQueryable<StudentEntity>(true).Where(q => q.UId == UID && q.Active == true && q.Archieved == false).FirstOrDefault();
            StudentModel model = new StudentModel();
            model.UId = student.UId;
            model.Name = student.Name;
            model.Email = student.Email;
            model.DateOfBirth= student.DateOfBirth;
            return model;
        }
        [HttpPost]

        public  async Task<StudentModel> UpdateStudent(StudentModel studentModel)
        {
            var student = Container.GetItemLinqQueryable<StudentEntity>(true).Where(q => q.UId == studentModel.UId && q.Active == true && q.Archieved == false).FirstOrDefault();
             student.Archieved = true;
            await Container.ReplaceItemAsync(student, student.UId);
            student.Id = Guid.NewGuid().ToString();
            student.Version = student.Version + 1;
            student.Email = studentModel.Email;
            student.Active = true;
            student.Archieved = false;
            student.UpdatedBy = "Goldy";
            student.UpdatedOn = DateTime.Now;
            student.DateOfBirth = studentModel.DateOfBirth;
            student.Name = studentModel.Name;

            student = await Container.CreateItemAsync(student);
            StudentModel model = new StudentModel();
            model.UId = student.UId;
            model.Name = student.Name;
            model.Email = student.Email;
            model.DateOfBirth = student.DateOfBirth;
            return model;

        }
        //ISSUE BOOK
        [HttpPost]
        public async Task<IssueModel> IssueBook(IssueModel issueModel)
        {
            IssueEntity issueEntity = new IssueEntity();
            issueEntity.Id = Guid.NewGuid().ToString();
            issueEntity.UId = issueEntity.Id;
            issueEntity.DocumentType = "Issue";
            issueEntity.CreatedBy = "Mahesh";
            issueEntity.CreatedOn = DateTime.Now;
            issueEntity.UpdatedBy = "Mayuri";
            issueEntity.UpdatedOn = DateTime.Now;
            issueEntity.Active = true;
            issueEntity.Archieved = false;
            issueEntity.BookId = issueModel.BookId;
            issueEntity.IssueDate = issueModel.IssueDate;
            issueEntity.ReturnDate = issueModel.ReturnDate;
            issueEntity = await Container.CreateItemAsync(issueEntity);
            IssueModel model = new IssueModel();
            model.UId = issueEntity.UId;
            model.Id = issueEntity.Id;
            model.BookId = issueEntity.BookId;
            model.IssueDate = issueEntity.IssueDate;
            model.ReturnDate = issueEntity.ReturnDate;
            return model;
        }
        [HttpGet]
        public async Task<IssueModel> GetByUId(string UId)
        {
            var book = Container.GetItemLinqQueryable<IssueEntity>(true).Where(q => q.UId == UId && q.Active == true && q.Archieved == false).FirstOrDefault();
            IssueModel issueModel = new IssueModel();
            issueModel.UId = book.UId;
            issueModel.Id = book.Id;
            issueModel.BookId = book.Id;
            issueModel.IssueDate = book.IssueDate;
            issueModel.ReturnDate = book.ReturnDate;
            return issueModel;
        }

        [HttpPost]
        public async Task<IssueModel> IsseUpdate(IssueModel issueModel)
        {
            var book = Container.GetItemLinqQueryable<IssueEntity>(true).Where(q => q.UId == issueModel.UId && q.Active == true && q.Archieved == false).FirstOrDefault();
            book.Archieved = true;
            await Container.ReplaceItemAsync(book, book.UId);
            book.Id = Guid.NewGuid().ToString();
            book.Version = book.Version + 1;
            book.UpdatedBy = "Goldy";
            book.UpdatedOn = DateTime.Now;
            book.Active = true;
            book.Archieved = false;
            book.BookId = book.BookId;
            book.IssueDate = issueModel.IssueDate;
            book.ReturnDate = issueModel.ReturnDate;
            book = await Container.CreateItemAsync(book);
            IssueModel issueModel1 = new IssueModel();
            issueModel1.UId = book.UId;
            issueModel1.BookId = book.BookId;
            issueModel1.IssueDate = book.IssueDate;
            issueModel1.ReturnDate = book.ReturnDate;
            return issueModel1;
        }
    }
}
