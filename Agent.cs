namespace AgentTreeBuilder
{
    public class Agent{
        public int number{get;set;}
        public int manager{get;set;}
        public Agent(int number, int manager){
            this.number = number;
            this.manager = manager;
        }
    }
}