using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Library;
using System.Xml.Linq;
using Model.Library.InterfacesDAO;

namespace DataAccessLayer.Library
{
    public class ReservationDAO : IReservationDAO
    {
        public IBookDAO BookDAO { get; set;}
        public IUserDAO UserDAO { get; set; }
        
        public ReservationDAO(IBookDAO bookDAO, IUserDAO userDAO)
        {
            bookDAO = BookDAO;
            userDAO = UserDAO;
        }
        private string xmlFilePath = @"C:\Users\chiara.agnello\Desktop\materiale.studio\ProgettoBiblioteca\Solution.Library\DataAccessLayer.Library\DBXML.xml";
        public Reservation CreateReservation(Reservation reservation)
        {
            XDocument doc = XDocument.Load(xmlFilePath);

            var library = doc.Root.Element("Reservations");
            library.Add(new XElement("Reservation",
                        new XAttribute("ReservationId", reservation.ID),
                       new XAttribute("UserId", reservation.User.ID),
                       new XAttribute("BookId", reservation.Book.ID),
                       new XAttribute("StartDate", reservation.StartDate.Date),
                       new XAttribute("EndDate", reservation.EndDate?.Date)));  //<-----
            doc.Save(xmlFilePath);
            return reservation;
        }

        public Reservation DeleteReservation(Reservation reservation)
        {
            //XDocument doc = XDocument.Load(xmlFilePath);
            //var library = doc.Root.Element("Reservations");
            throw new NotImplementedException();
        }

        public List<Reservation> ReadAllReservations()
        {
            List<Reservation> reservationResult = new List<Reservation>();

            XDocument doc = XDocument.Load(xmlFilePath);
            var resNodes = from resNode in doc.Root.Element("Reservations").Elements("Reservation")
                            select resNode;
            foreach(XElement node in resNodes)
            {
                Reservation reservation = new Reservation();
                reservation.ID = int.Parse(node.Attribute("BookId").Value);
                reservation.User.ID = int.Parse(node.Attribute("UserId").Value);
                reservation.Book.ID = int.Parse(node.Attribute("BookId").Value);
                reservation.StartDate = ((DateTime)node.Attribute("StartDate"));
                reservation.EndDate = ((DateTime)node.Attribute("EndDate"));
                reservationResult.Add(reservation);
            }
            
            return reservationResult;

        }

        public Reservation UpdateReservation()
        {
            throw new NotImplementedException();
        }
    }
}
