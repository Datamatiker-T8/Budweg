using Budweg.Domain;
using System;
using System.Collections.Generic;
using System.Data;
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
            int result = -1;
            using (SqlConnection con = new(P1DB08ConnectionPath))
            {
                con.Open();
                result = feedback.Id;
                int feedbackRating = feedback.Rating;
                string comment = feedback.Description;

                string query =$"    INSERT INTO FEEDBACK (FeedbackID, Rating, Description)" +
                              $"    VALUES              (@{result},@{feedbackRating},@{comment})";

                SqlCommand com = new(query);
                com.Parameters.Add($"@{result}", SqlDbType.Int);
                com.Parameters.Add($"@{feedbackRating}", SqlDbType.Int);
                com.Parameters.Add($"@{comment}", SqlDbType.NVarChar);
            }

            return result; // :)
        }
        public List<Feedback> GetAll()
        {
            return feedbacks;
        }
        public Feedback GetById(int id)
        {
            Feedback result = null;
            foreach (Feedback feedback in feedbacks)
            {
                if (feedback.Id.Equals(id))
                {
                    result = feedback;
                }
            }
            return result;
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
