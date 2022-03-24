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

    // ======================================================
    // Fields & Props
    // ======================================================

        private List<BrakeCaliber> brakeCalibers;
        private string CnnStr = Properties.Settings.Default.WPF_Connection;

    // ======================================================
    // Constructor: Adding every BrakeCaliber entity from database to "brakeCalibers" list.
    // ======================================================

        public BrakecaliberRepository()
        {
            byte[] QR_Code = null;
            brakeCalibers = new();
            using (SqlConnection connection = new(CnnStr))
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

                        //##########
                        //new
                        if (!Convert.IsDBNull(sqldatareader["QR_Code"]))//crash if null
                        {
                            QR_Code = (byte[])sqldatareader["QR_Code"];
                        }


                        // result = (someBool) ? if true : if false
                        BrakeCaliber bc = (brakeCaliberID != -1)
                            ? new(brakeCaliberID, caliberName, budwegNo, stockStatus, brakeSystem, linkQRCode, QR_Code)
                            : new(caliberName, budwegNo, stockStatus, brakeSystem, linkQRCode, QR_Code);
                        brakeCalibers.Add(bc);
                    }
                }
            }
        }

    // ======================================================
    // Repository CRUD: Create (Adding entity to database)
    // ======================================================
        public int Add(BrakeCaliber brakeCaliber) 
        {
            int result = -1;
            using (SqlConnection connection = new(CnnStr))
            {
                connection.Open();
                result = brakeCaliber.BrakeCaliberId;
                string caliberName = brakeCaliber.CaliberName;
                string BudwegNo = brakeCaliber.BudwegNo;
                bool stockStatus = brakeCaliber.StockStatus;
                string linkQRCode = brakeCaliber.LinkQRCode;
                string brakeSystem = brakeCaliber.BrakeSystem;
                byte[] qR_Bytes = brakeCaliber.QR_Bytes;

                string table = "BRAKECALIBER";
                string coloumns = "BRAKECALIBER.CaliberName, BRAKECALIBER.BudwegNo, BRAKECALIBER.StockStatus, BRAKECALIBER.BrakeSystem, BRAKECALIBER.LinkQRCode, BRAKECALIBER.QR_Code";
                string values = "@caliberName, @BudwegNo, @stockStatus, @brakeSystem, @linkQRCode, @qR_Bytes";
                string query =
                    $"INSERT INTO {table} ({coloumns})" +
                    $"VALUES ({values})";

                SqlCommand sqlCommand = new(query, connection);

                sqlCommand.Parameters.Add("@caliberName", SqlDbType.NVarChar).Value = brakeCaliber.CaliberName;
                sqlCommand.Parameters.Add("@BudwegNo", SqlDbType.NVarChar).Value = brakeCaliber.BudwegNo;
                sqlCommand.Parameters.Add("@stockStatus", SqlDbType.Bit).Value = brakeCaliber.StockStatus;
                sqlCommand.Parameters.Add("@brakeSystem", SqlDbType.NVarChar).Value = brakeCaliber.BrakeSystem;
                sqlCommand.Parameters.Add("@linkQRCode", SqlDbType.NVarChar).Value = brakeCaliber.LinkQRCode;
                sqlCommand.Parameters.Add("@qR_Bytes", SqlDbType.VarBinary).Value = brakeCaliber.QR_Bytes;

                sqlCommand.ExecuteNonQuery();
            }
            return result;
        }

    // ======================================================
    // Repository CRUD: Read (Reading entity from database)
    // ======================================================

        // Get all from database
        public List<BrakeCaliber> GetAll() 
        {
            return brakeCalibers;
        }

        // Get one entity from database by id
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

    // ======================================================
    // Repository CRUD: Update (Updating existing entity in database)
    // ======================================================

        public void Update(BrakeCaliber brakeCaliber)
        {
            using (SqlConnection connection = new(CnnStr))
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

    // ======================================================
    // Repository CRUD: Delete (Delete existing entity from database)
    // ======================================================

        public void Remove(BrakeCaliber brakeCaliber) 
        {
            brakeCalibers.Remove(brakeCaliber);
            using (SqlConnection connection = new(CnnStr))
            {
                connection.Open();
                string table = "BRAKECALIBER";
                string query = $"DELETE FROM {table} WHERE {brakeCaliber.BrakeCaliberId} = BrakeCaliberID";
                SqlCommand sqlCommand = new(query, connection);
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
