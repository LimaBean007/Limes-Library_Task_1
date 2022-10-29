using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_System
{
    class Book
    {
        private string bookID;
        private string authorSurname;
        private string bookNumber;
        private string bookTopic;

        public Book(string bookTopic, string bookNumber, string authorSurname)
        {
            this.authorSurname = authorSurname;
            this.bookNumber = bookNumber;
            this.bookTopic = bookTopic;
        }

        public Book()
        {
            this.bookID = Util.generateRandomString();
            this.authorSurname = Util.generateName();
            this.bookNumber = Util.generateBookNumber();
            this.bookTopic = Util.generateBookTopic();
        }

        public string BookID { get => bookID; set => bookID = value; }
        public string AuthorSurname { get => authorSurname; set => authorSurname = value; }
        public string BookNumber { get => bookNumber; set => bookNumber = value; }
        public string BookTopic { get => bookTopic; set => bookTopic = value; }
    }
}
