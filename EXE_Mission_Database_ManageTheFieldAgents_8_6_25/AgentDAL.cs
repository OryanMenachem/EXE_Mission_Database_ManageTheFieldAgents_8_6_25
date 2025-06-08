using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace EXE_Mission_Database_ManageTheFieldAgents_8_6_25
{
    internal class AgentDAL
    {
        private string connStr = "server=localhost;user=root;password=;database=eagleeyedb";
        private MySqlConnection _conn;

        public MySqlConnection openConnection()
        {
            if (_conn == null)
            {
                _conn = new MySqlConnection(connStr);
            }

            if (_conn.State != System.Data.ConnectionState.Open)
            {
                _conn.Open();
                Console.WriteLine("Connection successful.");
            }

            return _conn;
        }
        public void closeConnection()
        {
            if (_conn != null && _conn.State == System.Data.ConnectionState.Open)
            {
                _conn.Close();
                _conn = null;
                Console.WriteLine("Unconnection");
            }
        }
        public AgentDAL()
        {
            try
            {
                openConnection();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
            }
        }


        public void AddAgent(Agent agent)
        {
            string query = @"INSERT INTO agents VALUES (@id, @codeName, @realName, @location, @status, @missionsCompleted)";

            try
            {

                using (var cmd = new MySqlCommand(query, _conn))
                {
                    cmd.Parameters.AddWithValue("@id", agent.Id);
                    cmd.Parameters.AddWithValue("@codeName", agent.CodeName);
                    cmd.Parameters.AddWithValue("@realName", agent.RealName);
                    cmd.Parameters.AddWithValue("@location", agent.Location);
                    cmd.Parameters.AddWithValue("@status", agent.Status);
                    cmd.Parameters.AddWithValue("@missionsCompleted", agent.MissionsCompleted);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
            }
        }

        public List<Agent> GetAllAgents()
        {
            List<Agent> agentList = new List<Agent>();

            try
            {
                using (var cmd = new MySqlCommand("SELECT * FROM agents", _conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var agent = new Agent(
                            reader.GetInt32("id"),
                            reader.GetString("CodeName"),
                            reader.GetString("RealName"),
                            reader.GetString("Location"),
                            reader.GetString("Status"),
                            reader.GetInt32("MissionsCompleted")
                        );
                        agentList.Add(agent);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching agents: {ex.Message}");
            }
            finally
            {
                closeConnection();
            }
            return agentList;
        }




        public void UpdateAgentLocation(int agentId, string newLocation)
        {
            try
            {
                string updateLocationQuery = @"UPDATE agents SET Location = @newLocation WHERE id = @agentId";

                using (var cmd = new MySqlCommand(updateLocationQuery, _conn))
                {
                    cmd.Parameters.AddWithValue("@newLocation", newLocation);
                    cmd.Parameters.AddWithValue("@agentId", agentId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
            }
            finally
            {
                closeConnection();
            }
        }

        public void DeleteAgent(int agentId)
        {
            try
            {
                string delQuery = $"DELETE FROM agents WHERE `id`= {agentId}";

                using (var cmd = new MySqlCommand(delQuery, _conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error: {ex.Message}");
            }


            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
            }
            finally
            {
                closeConnection();
            }

        }

        public List<Agent> SearchAgentsByCode(string partialCode)
        {
            List<Agent> listAgent = new List<Agent>();

            try
            {
                string query = @"SELECT * FROM agents WHERE CodeName = @partialCode";

                using (var cmd = new MySqlCommand(query, _conn))
                {
                    cmd.Parameters.AddWithValue("@partialCode", partialCode);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var agent = new Agent(
                                reader.GetInt32("id"),
                                reader.GetString("CodeName"),
                                reader.GetString("RealName"),
                                reader.GetString("Location"),
                                reader.GetString("Status"),
                                reader.GetInt32("MissionsCompleted")
                            );
                            listAgent.Add(agent);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
            }
            finally
            {
                closeConnection();
            }

            return listAgent;
        }

    }

}
