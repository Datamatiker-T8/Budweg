using Budweg.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budweg.Persistence
{
    public class FeedbackRepository
    {
        private List<Feedback> feedbacks = new();
        private string P1DB08ConnectionPath = "Server=10.56.8.36;Database=P1DB08; User Id=P1-08;Password=OPENDB_08;";

        public FeedbackRepository()
        {
            using (SqlConnection connection = new(P1DB08ConnectionPath))
            {
                connection.Open();
                string table = "FEEDBACK";
                string values = "FeedbackID, Rating, Description, BrakecaliberID, CostumerID";
                string CommandText = $"SELECT {values} FROM {table}";

                SqlCommand sqlCommand = new(CommandText, connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read() != false)
                    {
                        int feedbackRating = int.Parse(reader["Rating"].ToString());
                        string Comment = reader["Description"].ToString();
                        int feedBackID = int.Parse(reader["FeedbackID"].ToString());
                        //int FK_BrakecaliberID = int.Parse(reader["BrakecaliberID"].ToString());
                        //int FK_CostumerID = int.Parse(reader["CostumerID"].ToString());

                        feedbacks.Add(new Feedback(feedbackRating, Comment, feedBackID));


                    }
                }
            }
        }
        public int? Add(Feedback feedback)
        {
            return null;
        }
        public List<Feedback> GetAll()
        {
            return feedbacks;
        }
        public void Remove(Feedback feedback)
        {
            feedbacks.Remove(feedback);

            using (SqlConnection connection = new(P1DB08ConnectionPath))
            {
                connection.Open();
                string table = "FEEDBACK";
                string query = $"DELETE FROM {table} WHERE {feedback.Id} = @FeedbackID";
            }
        }

    }
}
