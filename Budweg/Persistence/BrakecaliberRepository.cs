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
        private List<BrakeCaliber> brakeCalibers;
        private string CnnStr = Properties.Settings.Default.WPF_Connection;
        private string P1DB08ConnectionPath = "Server=10.56.8.36;Database=P1DB08; User Id=P1-08;Password=OPENDB_08;";
        public BrakecaliberRepository() // create
        {
            brakeCalibers = new();
            using (SqlConnection connection = new(P1DB08ConnectionPath))
            {
                connection.Open();
                string table = "BRAKECALIBER";
                string values = "BrakeCaliberID, CaliberName, BudwegNo, StockStatus, BrakeSystem, QR_Code, LinkQRCode";
                string CommandText = $"SELECT {values} FROM {table}";
                SqlCommand sQLCommand = new(CommandText, connection);
                using (SqlDataReader sqldatareader = sQLCommand.ExecuteReader())
                {
                    while (sqldatareader.Read() != false)
                    {
                        int brakeCaliberID = int.Parse(sqldatareader["BrakeCaliberID"].ToString());
                        string caliberName = sqldatareader["CaliberName"].ToString();
                        string budwegNo = sqldatareader["BudwegNo"].ToString();
                        bool stockStatus = sqldatareader["StockStatus"].ToString() == "1";
                        string brakeSystem = sqldatareader["BrakeSystem"].ToString();
                        string linkQRCode = sqldatareader["LinkQRCode"].ToString();

                        // result = (someBool) ? if true : if false
                        BrakeCaliber bc = (brakeCaliberID != -1)
                            ? new(brakeCaliberID, caliberName, budwegNo, stockStatus, brakeSystem, linkQRCode)
                            : new(caliberName, budwegNo, stockStatus, brakeSystem, linkQRCode);
                        brakeCalibers.Add(bc);
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
                string caliberName = brakeCaliber.CaliberName;
                string BudwegNo = brakeCaliber.BudwegNo;
                bool stockStatus = brakeCaliber.StockStatus;
                string linkQRCode = brakeCaliber.LinkQRCode;
                string brakeSystem = brakeCaliber.BrakeSystem;

                string table = "BRAKECALIBER";
                string coloumns = "BRAKECALIBER.CaliberName, BRAKECALIBER.BudwegNo, BRAKECALIBER.StockStatus, BRAKECALIBER.BrakeSystem, BRAKECALIBER.LinkQRCode";
                string values = "@caliberName, @BudwegNo, @stockStatus, @brakeSystem, @linkQRCode";
                string query =
                    $"INSERT INTO {table} ({coloumns})" +
                    $"VALUES ({values})";

                SqlCommand sqlCommand = new(query, connection);
                
                sqlCommand.Parameters.Add("@caliberName", SqlDbType.NVarChar).Value = brakeCaliber.CaliberName;
                sqlCommand.Parameters.Add("@BudwegNo", SqlDbType.NVarChar).Value = brakeCaliber.BudwegNo;
                sqlCommand.Parameters.Add("@stockStatus", SqlDbType.Bit).Value = brakeCaliber.StockStatus;
                sqlCommand.Parameters.Add("@brakeSystem", SqlDbType.NVarChar).Value = brakeCaliber.BrakeSystem;
                sqlCommand.Parameters.Add("@linkQRCode", SqlDbType.NVarChar).Value = brakeCaliber.LinkQRCode;

                sqlCommand.ExecuteNonQuery();
            }
            return result; 
        }
        public List<BrakeCaliber> GetAll() 
        {
            List<BrakeCaliber> brakeList = new List<BrakeCaliber>();
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(CnnStr))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = new SqlCommand("select * from [BRAKECALIBER]", connection);
                dataAdapter.Fill(ds);
            }

            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr = dt.Rows[i];
                BrakeCaliber brakeCaliber = new BrakeCaliber();
                brakeCaliber.BrakeCaliberId = (int)dr["BrakeCaliberId"];
                brakeCaliber.CaliberName = dr["CaliberName"].ToString();
                brakeCaliber.BudwegNo = dr["BudwegNo"].ToString();
                brakeCaliber.StockStatus = bool.Parse(dr["StockStatus"].ToString());
                brakeCaliber.BrakeSystem = dr["BrakeSystem"].ToString();
                brakeCaliber.LinkQRCode = dr["LinkQRCode"].ToString();

                brakeList.Add(brakeCaliber);
            }

            return brakeList;
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
            //TO DO Når en caliber bliver fjernet, skal Feedback også fjernes :)
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
