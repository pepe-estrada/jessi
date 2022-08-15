// See https://aka.ms/new-console-template for more information
namespace AgentTreeBuilder;

using System.Collections.Generic;

class TestAgentTreeBuilder{
    static void Main(string[] args){
        Agent head = new Agent(0,0);
        Agent assistantToHead = new Agent(1,0);
        Agent assistantToAssistant = new Agent(2,1);
        Agent otherGuy = new Agent(3,1);
        Agent taco = new Agent(4,0);
        List<Agent> agentList = new List<Agent>{head,assistantToAssistant,assistantToHead,otherGuy,taco};
        Dictionary<int,List<Agent>> agentDictionary = new Dictionary<int, List<Agent>>();
        foreach(var agent in agentList)
        {
            if(agentDictionary.ContainsKey(agent.manager)){
                if(agent.manager!=agent.number){
                    agentDictionary[agent.manager].Add(agent);
                }
            }
            else{
                if(agent.manager!=agent.number){
                    agentDictionary.Add(agent.manager, new List<Agent>{agent});
                }
                else{
                    agentDictionary.Add(agent.manager, new List<Agent>{});
                }
            }
        }
        foreach(int key in agentDictionary.Keys)
        {
            Console.WriteLine("agent: "+key);
            Console.Write("Under Agent: ");
            foreach(Agent agent in agentDictionary[key]){
                Console.Write(agent.number+" ");
            }
            Console.WriteLine();
        }
        
        Console.WriteLine(agentSubAgents(0,agentDictionary).Result);
    }
    public static async Task<String> agentSubAgents(int agentNumber, Dictionary<int,List<Agent>> agentDictionary){
        if(!agentDictionary.ContainsKey(agentNumber))
        {
            return "{\"agent\":"+agentNumber.ToString()+",\"managedAgents\":[]}";  
        }

        else if(agentDictionary[agentNumber].Count.Equals(0))
        {
            return "{\"agent\":"+agentNumber.ToString()+",[]}";    
        }
        else{
            String result = "";
            List<Task<String>> taskList = new List<Task<string>>();
            foreach(Agent agent in agentDictionary[agentNumber]){
                taskList.Add(agentSubAgents(agent.number,agentDictionary));
            }
            while(taskList.Any()){
                Task<String> completedTask = await Task.WhenAny(taskList.ToArray());
                if(result.Equals(""))
                {
                    result+="[";
                }
                result+=completedTask.Result+",";
                taskList.Remove(completedTask);
            }
            
            return "{\"agent\":"+agentNumber+",\"managedAgents\":"+result.Remove(result.Length-1)+"]"+"}";
        }
    }
}
