using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE_Mission_Database_ManageTheFieldAgents_8_6_25
{
    internal class Program
    {
 
            static void Main(string[] args)
            {
                Agent agent1 = new Agent(12345, "Lion", "Ari", "In wood", "Active", 1);
                Agent agent2 = new Agent(1234, "tiger", "Daniel", "In wood", "Injured", 1);
                Agent agent3 = new Agent(123, "Fox", "Ethan", "In wood", "Active", 2);
                Agent agent4 = new Agent(12, "Wolf", "Maya", "In wood", "Injured", 12);
                Agent agent5 = new Agent(1, "Hawk", "Shilo", "at home", "Missing", 4);
                Agent agent6 = new Agent(123456, "Panther", "David", "In sea", "Retired", 0);


                AgentDAL agentDAL = new AgentDAL();
                agentDAL.AddAgent(agent2);
                agentDAL.AddAgent(agent3);
                agentDAL.AddAgent(agent4);
                agentDAL.AddAgent(agent5);
                agentDAL.AddAgent(agent6);




                //List<Agent> agents11 = agentDAL.SearchAgentsByCode("Lion");

                //foreach (Agent agent in agents11)
                //{
                //    Console.WriteLine(agent);
                //}


                //agentDAL.DeleteAgent(12345);

                //agentDAL.UpdateAgentLocation(12345, "At home");

                //List<Agent> agents = agentDAL.GetAllAgents();

                //foreach (Agent agent1 in agents)
                //{
                //    Console.WriteLine(agent1.RealName);
                //}


            }
        
    }
}
