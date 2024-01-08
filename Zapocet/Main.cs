namespace Akce{
    class Program{
        static void Main(string[] args){

        // // Client client2 = new Client(new DivadloMusical());
        // // client2.Predstaveni();
        // // client2.Vstupenky();
        
        // Client client = new Client(new KinoKomedy());

            Client client = new Client(new DivadloMusical());
            client.Predstaveni();

            Console.WriteLine("Zadejte ID seanse");            
            int seanse = int.Parse(Console.ReadLine());
            ICommand selectFilmCommand = new VyberFilmuIDCommand(seanse);
            client.ExecuteCommand(selectFilmCommand);
            
            ICommand ViewBilety = new VyberBiletaIDCommand();
            client.ExecuteCommand(ViewBilety);    
            client.UndoCommand(ViewBilety);        


        }
    }
}