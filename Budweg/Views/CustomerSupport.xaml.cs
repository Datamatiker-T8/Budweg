using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Budweg
{
    /// <summary>
    /// Interaction logic for KundeSupport.xaml
    /// </summary>
    public partial class KundeSupport : Page
    {
        public KundeSupport()
        {
            InitializeComponent();
            FillComboBox();
        }

        private void FillComboBox()
        {
            string conString = "Server=10.56.8.36;Database=P1DB08;User Id=P1-08;Password=OPENDB_08;";

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from [BRAKECALIBER]";
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("BRAKECALIBER");
                da.Fill(dt);

                myComboBox.ItemsSource = dt.DefaultView;
                myComboBox.DisplayMemberPath = "BudwegNo";
            }

        }

    }
}
