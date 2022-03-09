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
    public class BrakecaliberRepository
    {
        private List<BrakeCaliber> brakeCalibers = new();
        private string P1DB08ConnectionPath = "Server=10.56.8.36;Database=P1DB08; User Id=P1-08;Password=OPENDB_08;";
        public BrakecaliberRepository() // create
        {
            using (SqlConnection connection = new(P1DB08ConnectionPath))
            {
                connection.Open();
                string table = "BRAKECALIBER";
                string values = "BrakeCaliberID, BudwegNo, QR_Code, LinkQRCode";
                string CommandText = $"SELECT {values} FROM {table}";

                SqlCommand sQLCommand = new(CommandText, connection);
                using (SqlDataReader sqldatareader = sQLCommand.ExecuteReader())
                {
                    while (sqldatareader.Read() != false)
                    { 
                        int brakeCaliberID = int.Parse(sqldatareader["BrakeCaliberID"].ToString());
                        string budwegNo = sqldatareader["BudwegNo"].ToString();
                        string linkQRCode = sqldatareader["LinkQRCode"].ToString();

                        brakeCalibers.Add(new BrakeCaliber(budwegNo, linkQRCode));
                    }
                }
            }
        }
        public int Add(BrakeCaliber brakeCaliber) 
        {
            int result = -1;
            using (SqlConnection connection = new(P1DB08ConnectionPath))
            {
                connection.Open();
                result = brakeCaliber.BrakeCaliberId;
                string BudwegNo = brakeCaliber.BudwegNo;
                string linkQRCode = brakeCaliber.LinkQRCode;

                string table = "BRAKECALIBER";
                string coloumns = "BrakeCaliberID, BudwegNo, QR_Code, LinkQRCode";
                string values = $"@{result}, @{BudwegNo}, @{linkQRCode}";
                string query =
                    $"INSERT INTO {table} ({coloumns})" +
                    $"VALUES({values})";

                SqlCommand sqlCommand = new(query);
                sqlCommand.Parameters.Add($"@{result}", SqlDbType.Int);
                sqlCommand.Parameters.Add($"@{BudwegNo}", SqlDbType.NVarChar);
                sqlCommand.Parameters.Add($"@{linkQRCode}", SqlDbType.NVarChar);
            }
            return result; 
        }
        public List<BrakeCaliber> GetAll() 
        { 
            return brakeCalibers; 
        }
        public BrakeCaliber GetById(int id) 
        {
            BrakeCaliber result = null;
            foreach (BrakeCaliber brakeCaliber in brakeCalibers)
            {
                if (brakeCaliber.BrakeCaliberId.Equals(id))
                {
                    result = brakeCaliber;
                }
            }
            return result;
        }
        public void Update(BrakeCaliber brakeCaliber)
        {
            using (SqlConnection connection = new(P1DB08ConnectionPath))
            {
                connection.Open();
                int id = brakeCaliber.BrakeCaliberId;
                string BudwegNo = brakeCaliber.BudwegNo;
                string linkQRCode = brakeCaliber.LinkQRCode;

                string table = "BRAKECALIBER";
                string values = $"@{id}, @{BudwegNo}, @{linkQRCode}";
                string query =
                    $"UPDATE {table}" +
                    $"SET BudwegNo = @'{BudwegNo}', LinkQRCode = @'{linkQRCode}', " +
                    $"WHERE BrakeCaliberId = {id}";
            }
        }
        public void Remove(BrakeCaliber brakeCaliber) 
        {
            brakeCalibers.Remove(brakeCaliber);
            // Delete existing owner in database
            using (SqlConnection connection = new(P1DB08ConnectionPath))
            {
                connection.Open();
                string table = "BRAKECALIBER";
                string query = $"DELETE FROM {table} WHERE {brakeCaliber.BrakeCaliberId} = @BrakeCaliberID";
            }
        }
    }
}
