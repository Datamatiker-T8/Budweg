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
    public class ResourceRepository
    {
        private List<Resource> resources = new();
        private string P1DB08ConnectionPath = "Server=10.56.8.36;Database=P1DB08; User Id=P1-08;Password=OPENDB_08;";
        
        public ResourceRepository()
        {
            using(SqlConnection con = new(P1DB08ConnectionPath))
            {
                con.Open();
                string query = $"SELECT RessourceID, Title, Views, Version FROM RESSOURCE";

                SqlCommand com = new(query, con);
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    while(reader.Read() != false)
                    {
                        int RessourceID = int.Parse(reader["RessourceID"].ToString());
                        string Title = reader["Title"].ToString();
                        int Views = int.Parse(reader["Views"].ToString());
                        string Version = reader["Version"].ToString();

                        resources.Add(new Resource(Title, Version));
                    }
                }
            }
        }
        public int Add(Resource resource)
        {
            int result = -1;
            using (SqlConnection con = new(P1DB08ConnectionPath))
            {
                con.Open();
                result = resource.ResourceId;
                int Views = resource.Views;
                string Title = resource.Title;
                string Version = resource.VersionNumber;

                string query = $"    INSERT INTO RESSOURCE (RessourceID, Title, Views, Version)" +
                              $"    VALUES              (@{result},@{Title},@{Views},@{Version}))";

                SqlCommand com = new(query);
                com.Parameters.Add($"@{result}", SqlDbType.Int);
                com.Parameters.Add($"@{Title}", SqlDbType.NVarChar);
                com.Parameters.Add($"@{Views}", SqlDbType.Int);
                com.Parameters.Add($"@{Version}", SqlDbType.NVarChar);
            }

            return result; // :)
        }
        public List<Resource> GetAll()
        {
            return resources;
        }

        public Resource GetById(int id)
        {
            Resource result = null;
            foreach (Resource resource in resources)
            {
                if (resource.ResourceId.Equals(id))
                {
                    result = resource;
                }
            }
            return result;
        }
        public void Remove(Resource resource)
        {
            resources.Remove(resource);

            using (SqlConnection connection = new(P1DB08ConnectionPath))
            {
                connection.Open();
                string table = "RESSOURCE";
                string query = $"DELETE FROM {table} WHERE {resource.ResourceId} = @RessourceID";
                SqlCommand cmd = new SqlCommand(query);
            }
        }
    }
}
