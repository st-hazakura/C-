namespace Akce{
    class Program{
        static void Main(string[] args){

            StateMachine machine = new StateMachine();
            machine.SetState(new StateVyberFabric());

            while (true){
                int input = int.Parse(Console.ReadLine());

                machine.HandleInput(input);
            }
        }
    }
}