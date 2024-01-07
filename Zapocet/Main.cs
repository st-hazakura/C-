namespace Akce{
    class Program{
        static void Main(string[] args){

        Client client2 = new Client(new DivadloMusical());
        client2.Predstaveni();
        client2.Vstupenky();
        }
    }
}