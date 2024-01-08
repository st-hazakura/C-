namespace Akce{

    public interface ICommand{
        void Execute(IAkce akce);
        void Undo(IAkce akce);
    }

    public class VyberFilmuIDCommand : ICommand {
        private int ID;
        private List<Seans> originalSeansy;
        private List<PredstaveniDivadlo> originalPredstaveni;

        public VyberFilmuIDCommand(int id) {
            ID = id;
        }

        public void Execute(IAkce akce) {
            if (akce is Kino kino){
                ExecuteProFilm(kino, ID);
                kino.InformacePredstaveni();
            }else if (akce is Divadlo divadlo){
                ExicuteProDivadlo(divadlo, ID);
                divadlo.InformacePredstaveni();
            }
        }

        public void Undo(IAkce akce) {
            if (akce is Kino kino){
                kino.SetSeansyData(originalSeansy);
                kino.InformacePredstaveni();
            }else if (akce is Divadlo divadlo){
                divadlo.SetSeansyData(originalPredstaveni);
                divadlo.InformacePredstaveni();
            }
        }


        private void ExecuteProFilm(Kino kino, int IDFilm){
            originalSeansy = new List<Seans>(kino.GetSeansyData());
            List<Seans> FiltrSeanse = kino.GetSeansyData().Where(seans => seans.ID == IDFilm).ToList();
            kino.SetSeansyData(FiltrSeanse);
        }

        private void ExicuteProDivadlo(Divadlo divadlo, int IDPredst){
            originalPredstaveni = new List<PredstaveniDivadlo>(divadlo.GetSeansyData());
            List<PredstaveniDivadlo> FiltrPred = divadlo.GetSeansyData().Where(pred => pred.ID == IDPredst).ToList();
            divadlo.SetSeansyData(FiltrPred);
        }
    }

    public class VyberBiletaIDCommand: ICommand{
        private List<Bilet> origvstfilm;
        private List<BiletDivadlo> origDivadlo;
        private List<Seans> seance;
        private List<PredstaveniDivadlo> seanceD;

        public void Execute(IAkce akce) {
            if (akce is Kino kino){
                ExecuteVstupenkyFilm(kino);
                kino.InformaceVstupenky();
            }else if (akce is Divadlo divadlo){
                ExecuteVstupenkyDivadlo(divadlo);
                divadlo.InformaceVstupenky();
            }
        }

        public void Undo(IAkce akce){
            if (akce is Kino kino){
                kino.SetBiletyData(origvstfilm);
            }else if (akce is Divadlo divadlo){
                divadlo.SetBiletyData(origDivadlo);
            }
        }

        public void ExecuteVstupenkyFilm(Kino kino){
            origvstfilm = new List<Bilet>(kino.GetBiletyData());
            seance = new List<Seans>(kino.GetSeansyData());
            int seansID = seance.First().ID;

            List<Bilet> filtrdata = kino.GetBiletyData().Where(bilet => bilet.SeansID == seansID).ToList();
            kino.SetBiletyData(filtrdata);
        }

        public void ExecuteVstupenkyDivadlo(Divadlo divadlo){
            origDivadlo = new List<BiletDivadlo>(divadlo.GetBiletyData());
            seanceD = new List<PredstaveniDivadlo>(divadlo.GetSeansyData());
            int seansID = seanceD.First().ID;

            List<BiletDivadlo> filrData = divadlo.GetBiletyData().Where(bilet => bilet.IDPredstaveni == seansID).ToList();
            divadlo.SetBiletyData(filrData);
        }
    }


    public class VyberMistaCommand: ICommand{
        private int Misto;
        private int Radek;
        private int Poloha;
        private List<Bilet> originalBiletyFilm;
        private List<BiletDivadlo> originalBiletyDivadlo;

        public VyberMistaCommand(int misto){
            Misto = misto;
        }
        public VyberMistaCommand(int misto, int radek, int poloha): this(misto){
            Radek = radek;
            Poloha = poloha;
        }

        public void Execute(IAkce akce){
            if (akce is Kino kino){
                ExecuteProFilm(kino, Misto);
                kino.InformaceVstupenky();
            }else if (akce is Divadlo divadlo){
                ExecuteProDivadlo(divadlo, Misto, Radek, Poloha);
                divadlo.InformaceVstupenky();
            }
        }

        public void Undo(IAkce akce){

        }

        private void ExecuteProFilm(Kino kino, int misto){
            // seance = new List<Seans>(kino.GetSeansyData());
            // originalBiletyFilm = new List<Bilet>(kino.GetBiletyData());
            // List<Bilet> filtredBilet = kino.GetBiletyData().Where(bilet => bilet.SeatNumber == misto ).ToList();
            // kino.SetBiletyData(filtredBilet);
        }

        private void ExecuteProDivadlo(Divadlo divadlo, int misto, int radek, int poloha){
            // originalBiletyDivadlo = new List<BiletDivadlo>(divadlo.GetBiletyData());
            // List<BiletDivadlo> filtredBilet = divadlo.GetBiletyData().Where(bilet => 
            //     bilet.MistoRadku == misto && bilet.CisloRadku == radek && bilet.SekceID == poloha).ToList();
            // divadlo.SetBiletyData(filtredBilet);
        }
    }
}