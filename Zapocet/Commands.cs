namespace Akce{

    public interface ICommand{
        void Execute(IAkce akce);
        void Undo(IAkce akce);
    }

    public class KoupitBiletCommand: ICommand{

        private int Radek;
        private int Poloha;
        private int seanseIDD;
        private List<BiletDivadlo> koupenybiletDivadlo;


        private Kino kino;
        private int misto;
        private int seanseID;
        private Client client;
        private int biletID;

        public KoupitBiletCommand(Kino kino, int misto, int seanseID, Client client) { 
            this.kino = kino;
            this.misto = misto; 
            this.seanseID = seanseID;
            this.client = client;
        }

        public KoupitBiletCommand(Kino kino, int biletID, Client client) { 
            this.kino = kino;
            this.biletID = biletID;
            this.client = client;
        }
        
        public void Execute(IAkce akce){
            if (akce is Kino kino){
                ExecuteProFilm(kino, misto, seanseID, client);
            }else if (akce is Divadlo divadlo){
                // ExecuteProDivadlo(divadlo, Misto, Radek, Poloha);
                divadlo.InformaceVstupenky();
            }
        }

        public void Undo(IAkce akce){
            if (akce is Kino kino){
                UndoProFilm( kino, biletID, client);
            }else if (akce is Divadlo divadlo){
                // ExecuteProDivadlo(divadlo, Misto, Radek, Poloha);
                divadlo.InformaceVstupenky();
            }

        }

        private void ExecuteProFilm(Kino kino, int misto, int seanseID, Client client){
            Bilet koupenyBiletFilm = kino.GetBiletyData().FirstOrDefault(bilet => bilet.SeatNumber == misto && bilet.SeansID == seanseID);
            int dostpenez = client.BankovniUcet - koupenyBiletFilm.Price;
            if (koupenyBiletFilm.IsSold) {
                if (dostpenez>=0){
                koupenyBiletFilm.IsSold = false;
                client.BankovniUcet -= koupenyBiletFilm.Price; 
                kino.SetBiletyData(kino.GetBiletyData());
                client.KupleneBilety.Add(koupenyBiletFilm);}
                else{Console.WriteLine("Nemate dost penez");}
            }else{
                Console.WriteLine("Tento bilet jiz koupeny");
            }
        }

        private void UndoProFilm(Kino kino, int biletID, Client client){
                Bilet bilet = client.KupleneBilety.FirstOrDefault(bil => bil.ID == biletID);
                if (!bilet.IsSold){
                    client.BankovniUcet += bilet.Price;
                }else{Console.WriteLine("Bilet lze koupit chyba");}
                Bilet vracenyBilet = kino.GetBiletyData().FirstOrDefault(bilet => bilet.ID == biletID );
                if (!vracenyBilet.IsSold){
                    vracenyBilet.IsSold = true;
                    client.KupleneBilety.Remove(bilet);
                }else{Console.WriteLine("Bilet nenalezen");}
        }
        

    }

        // private void ExecuteProDivadlo(Divadlo divadlo, int misto, int radek, int poloha){
        //     // originalBiletyDivadlo = new List<BiletDivadlo>(divadlo.GetBiletyData());
        //     // List<BiletDivadlo> filtredBilet = divadlo.GetBiletyData().Where(bilet => 
        //     //     bilet.MistoRadku == misto && bilet.CisloRadku == radek && bilet.SekceID == poloha).ToList();
        //     // divadlo.SetBiletyData(filtredBilet);
        // }
    
}