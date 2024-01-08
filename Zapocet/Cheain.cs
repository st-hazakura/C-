namespace Akce{
    public interface IState{

        void UzivatelVstup(int vstup, StateMachine machine);
        void PrechodStavu(StateMachine machine);
    }

    public abstract class BaseState : IState{
        public abstract void UzivatelVstup(int input, StateMachine machine);
        public abstract void PrechodStavu(StateMachine machine);
    }


    public class StateMachine{
        private IState currentState;
        public Client CurrentClient { get; private set; }
        public List<Seans> SelectedSeans { get; set; }

        public List<PredstaveniDivadlo> SelectedPredstaveni { get; set; }
        public List<Bilet> ListBiletuF{get; set;}
        public List<BiletDivadlo> ListBiletuD{get; set;}


        public void SetState(IState state){
            currentState = state;
            state.PrechodStavu(this);
        }

        public void SetClient(Client client){
            CurrentClient = client;
        }

        public void HandleInput(int input){
            currentState.UzivatelVstup(input, this);
        }
    }


    public class StateVyberFabric : BaseState{
        public override void PrechodStavu(StateMachine machine){
            Console.Clear();
            Console.WriteLine("=== Vyber Zanru ===");
            Console.WriteLine("1 - Divadlo Musical");
            Console.WriteLine("2 - Divadlo Drama");
            Console.WriteLine("3 - Kino Komedy");
            Console.WriteLine("4 - Kino Horror\n \n");
            Console.WriteLine("Zadejte svuj vyber:");
        }

        public override void UzivatelVstup(int input, StateMachine machine){
            switch (input) {
                case 1:
                    machine.SetClient(new Client(new DivadloMusical()));
                    machine.SetState(new StateVyberSeance());
                    break;
                case 2:
                    machine.SetClient(new Client(new DivadloDrama()));
                    machine.SetState(new StateVyberSeance());
                    break;
                case 3:
                    machine.SetClient(new Client(new KinoKomedy()));
                    machine.SetState(new StateVyberSeance());
                    break;
                case 4:
                    machine.SetClient(new Client(new KinoHorror()));
                    machine.SetState(new StateVyberSeance());
                    break;
            }
        }
    }


    public class StateVyberSeance : BaseState{

        public override void PrechodStavu(StateMachine machine){
            Console.Clear();
            machine.CurrentClient.Predstaveni();
            Console.WriteLine("1 - Vyber Filmu, 2 - Vyber fabric, 3 - Soukromy Ucet") ;
        }

        public override void UzivatelVstup(int input, StateMachine machine){
            IAkce akce = machine.CurrentClient.GetContext();

            switch (input){
                case 1:
                    if(akce is Kino kino){
                    Console.WriteLine("Vyber si film podle id: ");
                    int filmID = int.Parse(Console.ReadLine());
                        machine.SelectedSeans = kino.GetSeansyData().Where(seans => seans.ID == filmID).ToList();
                    
                    }else if (akce is Divadlo divadlo){
                        Console.WriteLine("Vyber si predstaveni podle id: ");
                        int predID = int.Parse(Console.ReadLine());
                        machine.SelectedPredstaveni = divadlo.GetSeansyData().Where(pred => pred.ID == predID).ToList();
                    }
                    machine.SetState(new StateShowTikets());
                    break;

                case 2:
                    machine.SetState(new StateVyberFabric());
                    break;

                case 3:
                    machine.SetState(new Kibinet());
                    break;
            }
        }
    }


    public class StateShowTikets: BaseState{

        public override void PrechodStavu(StateMachine machine){
            Console.Clear();
            Console.WriteLine("=== Prehled Seance ===");
            IAkce akce = machine.CurrentClient.GetContext();
            if (akce is Kino kino) {
                foreach (var seans in machine.SelectedSeans) {kino.InformacePredstaveniIDF(seans.ID);}
            }else if (akce is Divadlo divadlo) {
                foreach (var seans in machine.SelectedPredstaveni) {divadlo.InformacePredstaveniIDD(seans.ID);}}
            
            Console.WriteLine("\n1 - Kouknout se na Bilety");
            Console.WriteLine("2 - Vratit se k vyberu Filmu");
            Console.WriteLine("3 - Soukromy Ucet");
        }

        public override void UzivatelVstup(int input, StateMachine machine){
            IAkce akce = machine.CurrentClient.GetContext();

            switch (input){
                case 1:
                    if(akce is Kino kino){
                        var seans = machine.SelectedSeans.First();
                        machine.ListBiletuF = kino.GetBiletyData().Where( bilet => bilet.SeansID == seans.ID).ToList();
    
                    }else if (akce is Divadlo divadlo){
                        var seans = machine.SelectedPredstaveni.First();
                        machine.ListBiletuD = divadlo.GetBiletyData().Where( bilet => bilet.IDPredstaveni == seans.ID).ToList();
                    }
                    machine.SetState(new VybratBilet());
                    break;

                case 2:
                    machine.SetState(new StateVyberSeance());
                    break;

                case 3:
                    machine.SetState(new Kibinet());
                    break;

            }
        }
    }

    public class VybratBilet: BaseState{
        public override void PrechodStavu(StateMachine machine){
            Console.Clear();
            IAkce akce = machine.CurrentClient.GetContext();
            
            if (akce is Kino kino) {
                Console.WriteLine("Vstupenky pro Film:");
                foreach (var seans in machine.SelectedSeans) {kino.InformaceVstupenkyProSeansF(seans.ID);}
            
            }else if (akce is Divadlo divadlo) {
                Console.WriteLine("Vstupenky pro predstaveni:");
                foreach (var seans in machine.SelectedPredstaveni) {divadlo.InformaceVstupenkyProSeansD(seans.ID);}}

            Console.WriteLine("\n");
            Console.WriteLine("=== Vyber Biletu === \n");
            Console.WriteLine("1 - Koupit bilet");
            Console.WriteLine("2 - Vratit se k vyberu Filmu \n \n");
            Console.WriteLine("Zadejte svuj vyber:");
        }

        public override void UzivatelVstup(int input, StateMachine machine){
            IAkce akce = machine.CurrentClient.GetContext();

            switch (input){
                case 1:
                    Console.WriteLine("Vyber si misto ktere chcete koupit: ");
                    if (akce is Kino kino){
                        int misto = int.Parse(Console.ReadLine());
                        int seanseID = machine.SelectedSeans.First().ID;
                        ICommand koupitbilet = new KoupitBiletCommand(kino, misto, seanseID, machine.CurrentClient);
                        machine.CurrentClient.ExecuteCommand(koupitbilet);
                    }
                    machine.SetState(new Kibinet());
                    break;

                case 2:
                    machine.SetState(new StateVyberSeance());
                    break;

            }
        }
    }

    public class Kibinet: BaseState{
        public override void PrechodStavu(StateMachine machine){
            Console.Clear();
            Console.WriteLine("=== SOUKROMY UCET ===\n");
            Console.WriteLine("Zustatek Na Uctu: " + machine.CurrentClient.BankovniUcet + " CZK");
            Console.WriteLine("\nKuplene Bilety:");

            if (machine.CurrentClient.KupleneBilety.Count > 0) {
                foreach (var bilet in machine.CurrentClient.KupleneBilety) {
                    Console.WriteLine($"Bilet ID: {bilet.ID}, Film/Seans: {bilet.FilmName}, Cena: {bilet.Price} CZK "+
                    $"Seat: {bilet.SeatNumber}");
                }
            } else {
                Console.WriteLine("Nemate zadne kuplene bilety.");
            }
            Console.WriteLine("\n \n1 - Navrat do Vyberu Biletu");
            Console.WriteLine("2 - Vyber Filmu");
            Console.WriteLine("3 - Vratit Bilety");
        }  
        

        public override void UzivatelVstup(int input, StateMachine machine){
            switch(input){
                case 1: 
                    machine.SetState(new VybratBilet());
                    break;

                case 2: 
                    machine.SetState(new StateVyberSeance());
                    break;
                case 3:
                    machine.SetState(new VratitBilety());
                    break;

            }

        }
    }

    public class VratitBilety: BaseState{
        public override void PrechodStavu(StateMachine machine){
            Console.Clear();
            Console.WriteLine("=== VYBER BILETU VRACENI===\n");
            Console.WriteLine("1 - Vratit Bilety");
            Console.WriteLine("2 - Soukromy Ucet");
            Console.WriteLine("3 - Vratit se k vyberu Filmu");
        }

        public override void UzivatelVstup(int input, StateMachine machine){
            IAkce akce = machine.CurrentClient.GetContext();

            switch(input){
                case 1:
                    Console.WriteLine("Napis ID biletu ktery chces vratit: ");
                    if (akce is Kino kino){
                        int biletID  = int.Parse(Console.ReadLine());
                        ICommand koupitbilet = new KoupitBiletCommand(kino, biletID, machine.CurrentClient);
                        machine.CurrentClient.UndoCommand(koupitbilet);
                        
                        Console.WriteLine("Probíhá zpracování vrácení...");
                        for (int i = 0; i <= 100; i += 10) {
                            Console.Write($"\r[{new string('#', i / 10)}{new string('-', 10 - i / 10)}] {i}%");
                            Thread.Sleep(1000); 
                        }
                        Console.WriteLine("\nVrácení dokončeno.");

                    }
                    Console.WriteLine("Pookracujte ve volbach");
                    break;

                case 2: 
                    machine.SetState(new Kibinet());
                    break;

                case 3: 
                    machine.SetState(new StateVyberSeance());
                    break;

            }

        }
    }
    
}