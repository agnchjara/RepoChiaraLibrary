using Model.Library;
using Model.Library.InterfacesDAO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Library.EntitiesDB;
using System.Runtime.InteropServices.ComTypes;


namespace DataAccessLayer.Library.EntitiesDB
{
    public class ReservationDAO_DB : IReservationDAO
    {
        public Reservation CreateReservation(Reservation reservation)
        {
            using (SqlConnection conn = DBConnectionProva.GetSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"up_CreateReservation";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    //Per ogni parametro
                    SqlParameter bookId = new SqlParameter("BookId", System.Data.SqlDbType.Int);
                    bookId.Value = reservation.Book.ID;
                    cmd.Parameters.Add(bookId);

                    SqlParameter userId = new SqlParameter("UserId", System.Data.SqlDbType.Int);
                    userId.Value = reservation.User.ID;
                    cmd.Parameters.Add(userId);

                    SqlParameter startDate = new SqlParameter("StartDate", System.Data.SqlDbType.DateTime);
                    startDate.Value = reservation.StartDate;
                    cmd.Parameters.Add(startDate);

                    SqlParameter endDate = new SqlParameter("EndDate", System.Data.SqlDbType.DateTime);
                    endDate.Value = reservation.EndDate;
                    cmd.Parameters.Add(endDate);

                    cmd.ExecuteNonQuery();
                    int id = ReadAllReservations().Select(b => b.ID).Max();
                    reservation.ID = id;
                    
                }
                conn.Close();
            }
            return reservation;
        }
        /// <summary>
        /// Questo metodo aggiorna la quantità del Boook e aggiorna la EndDate a Today 
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        public bool DeleteReservation(Reservation reservation)
        {
            bool deleted = true;
            using(SqlConnection conn = DBConnectionProva.GetSqlConnection())
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"up_DeleteReservation";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter reservationId = new SqlParameter("ReservationId", System.Data.SqlDbType.Int);
                    reservationId.Value = reservation.ID;
                    cmd.Parameters.Add(reservationId);

                    SqlParameter bookId = new SqlParameter("BookId", System.Data.SqlDbType.Int);
                    bookId.Value = reservation.Book.ID;
                    cmd.Parameters.Add(bookId);

                    SqlParameter endDate = new SqlParameter("EndDate", System.Data.SqlDbType.DateTime);
                    endDate.Value = reservation.EndDate;   //da assegnare nella BL
                    cmd.Parameters.Add(endDate);

                }
            }
            return deleted;
        }

        public List<Reservation> ReadAllReservations()
        {
            List<Reservation> reservations = new List<Reservation>();
            using (SqlConnection conn = DBConnectionProva.GetSqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    //Testo della query : nome della stored Procedure
                    cmd.CommandText = "SELECT * FROM Reservations;";
                   
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Book book = new Book();
                        User user = new User();
                        int reservationId = Int32.Parse(reader["ReservationId"].ToString());
                        book.ID = Int32.Parse(reader["BookId"].ToString());
                        user.ID = Int32.Parse(reader["UserId"].ToString());
                        DateTime startDate = DateTime.Parse(reader["StartDate"].ToString());
                        DateTime? endDate = DateTime.Parse(reader["EndDate"].ToString());

                        Reservation reservation = new Reservation(reservationId, book, user, startDate, endDate);
                        reservations.Add(reservation);
                    }
                } conn.Close();
            }
            return reservations;
        }

        public Reservation UpdateReservation()
        {
            throw new NotImplementedException();
        }
    }

}
