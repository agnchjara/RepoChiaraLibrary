using DataAccessLayer.Library.EntitiesDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Library;
using Model.Library.InterfacesDAO;
using System;
using System.Collections.Generic;

namespace Library.Tests
{
    [TestClass]
    public class BookDAO_DB_Tests
    {
        [TestMethod]
        public void ReadAllBooksBookDAO_DB()
        {
            int expected = 5;
            BookDAO_DB bookDAO_DB = new BookDAO_DB();

            List<Book> result = bookDAO_DB.ReadAllBooks();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected , result.Count);
        }
    }
}
