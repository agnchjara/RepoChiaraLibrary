using Model.Library;
using Model.Library.InterfacesDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DataAccessLayer.Library
{
    public class BookDAO : IBookDAO
    {

        private string xmlFilePath = @"C:\Users\chiara.agnello\Desktop\materiale.studio\ProgettoBiblioteca\Solution.Library\DataAccessLayer.Library\DBXML.xml";
        public Book CreateBook(Book book)
        {
            XDocument doc = XDocument.Load(xmlFilePath);

            var library = doc.Root.Element("Books");
            library.Add(new XElement("Book",
                       new XAttribute("BookId", book.ID.ToString()),
                       new XAttribute("Title", book.Title),
                       new XAttribute("AuthorName", book.AuthorName),
                       new XAttribute("AuthorSurname", book.AuthorSurname),
                       new XAttribute("Publisher", book.PublishingHouse),
                       new XAttribute("Quantity", book.Quantity.ToString()),
                       new XAttribute("IsDeleted", "false")));
            //doc.Root.Add(library); questo passaggio serve? no
            doc.Save(xmlFilePath);
            return book;
            #region
            //    XDocument doc = XDocument.Load(xmlFilePath);
            //    XElement library = doc.Element("Library");
            //    library.Add(new XElement("BookID = ", book.BookID.ToString()),
            //               new XElement("Title = ", book.Title),
            //               new XElement("AuthorName = ", book.AuthorName),
            //               new XElement("AuthorSurname = ", book.AuthorSurname),
            //               new XElement("Publisher = ", book.PublishingHouse),
            //               new XElement("Quantity = ", book.Quantity.ToString()));
            //    doc.Save(xmlFilePath);


            //using (XmlWriter xmlWriter = XmlWriter.Create("Test.xml", xmlWriterSettings))
            //{
            //    xmlWriter.WriteStartDocument();
            //    xmlWriter.WriteStartElement("Library");

            //    xmlWriter.WriteStartElement("Books");
            //    xmlWriter.WriteElementString("Book ID = ", book.BookID.ToString());
            //    xmlWriter.WriteElementString("Title = ", book.Title);
            //    xmlWriter.WriteElementString("Author Name = ", book.AuthorName);
            //    xmlWriter.WriteElementString("Author Surname = ", book.AuthorSurname);
            //    xmlWriter.WriteElementString("Publisher = ", book.PublishingHouse);
            //    xmlWriter.WriteElementString("Quantity = ", book.Quantity.ToString());
            //    xmlWriter.WriteEndElement();

            //    xmlWriter.WriteEndElement();
            //    xmlWriter.WriteEndDocument();
            //    xmlWriter.Flush();
            //    xmlWriter.Close();
            //}
            #endregion

        }
        public bool DeleteBook(Book book)
        {
            bool delete = true;
            XDocument doc = XDocument.Load(xmlFilePath);
            var target = doc.Root.Descendants("Book").
                SingleOrDefault(x => int.Parse(x.Attribute("BookId").Value) == book.ID);
            target.Attribute("Quantity").Value = "0";
            target.Attribute("IsDeleted").Value = "true";
            doc.Save(xmlFilePath);
            
            //if (target.Attribute("IsDeleted").Value != "true")
            //{
            //    delete = false;
            //}

            return delete;
        }
        public List<Book> ReadAllBooks()
        {
            List<Book> bookResult = new List<Book>();

            XDocument doc = XDocument.Load(xmlFilePath);
            var bookNodes = from bookNode in doc.Root.Element("Books").Elements("Book")
                            select bookNode;
            foreach (XElement node in bookNodes)
            {
                Book book = new Book();
                book.ID = int.Parse(node.Attribute("BookId").Value);
                book.Title = node.Attribute("Title").Value;
                book.AuthorName = node.Attribute("AuthorName").Value;
                book.AuthorSurname = node.Attribute("AuthorSurname").Value;
                book.PublishingHouse = node.Attribute("Publisher").Value;
                book.Quantity = int.Parse(node.Attribute("Quantity").Value);
                book.IsDeleted = bool.Parse(node.Attribute("IsDeleted").Value);
                bookResult.Add(book);
            }

            return bookResult;

        }

        public Book UpdateBook(int bookId, Book bookWithNewValues)
        { 
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilePath);
            XmlElement root = doc["Library"];
            XmlNode host = root.SelectSingleNode("Books");
            XmlNodeList booksList = host.SelectNodes("Book");
            foreach (XmlNode bookXML in booksList)
            {
                XmlElement singleBook = (XmlElement)bookXML;
                if (int.Parse(singleBook.Attributes["BookId"].Value) == bookId)
                {
                    XmlNode nuovoBookXML = doc.CreateElement("Book");
                    XmlAttribute theBookId = doc.CreateAttribute("BookId");
                    bookId = int.Parse(singleBook.Attributes["BookId"].Value); 
                    nuovoBookXML.Attributes.Append(theBookId); 
                    XmlAttribute title = doc.CreateAttribute("Title"); 
                    title.Value = bookWithNewValues.Title + "";
                    nuovoBookXML.Attributes.Append(title); 
                    XmlAttribute authorName = doc.CreateAttribute("AuthorName"); 
                    authorName.Value = bookWithNewValues.AuthorName + ""; 
                    nuovoBookXML.Attributes.Append(authorName); 
                    XmlAttribute authorSurname = doc.CreateAttribute("AuthorSurname"); 
                    authorSurname.Value = bookWithNewValues.AuthorSurname + ""; 
                    nuovoBookXML.Attributes.Append(authorSurname); 
                    XmlAttribute publishingHouse = doc.CreateAttribute("Publisher"); 
                    publishingHouse.Value = bookWithNewValues.PublishingHouse + ""; 
                    nuovoBookXML.Attributes.Append(publishingHouse); 
                    XmlAttribute quantity = doc.CreateAttribute("Quantity"); 
                    quantity.Value = bookWithNewValues.Quantity + ""; 
                    nuovoBookXML.Attributes.Append(quantity);
                    XmlAttribute isDeleted = doc.CreateAttribute("IsDeleted");
                    isDeleted.Value = bookWithNewValues.IsDeleted + "";
                    nuovoBookXML.Attributes.Append(isDeleted);
                    host.ReplaceChild(nuovoBookXML, bookXML); 
                   doc.Save(xmlFilePath);
                }
            }

            doc.Save(xmlFilePath);

            return bookWithNewValues; //Controlla se va bene come parametro di ritorno 

        }
    }
}
