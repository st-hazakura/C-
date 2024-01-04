class Program{
    static void Main(){
        IZvire slon = new Slon();
        IZvire pes = new Pes();
        IZvire koala = new Koala();

        INavstevnik feedr = new Feedr();
        INavstevnik kluk = new Kluk();

        slon.PrijmoutNavstevnika(feedr);
        pes.PrijmoutNavstevnika(kluk);
    }



    public interface INavstevnik{
        public void NavstivitSlona(Slon slon);
        public void NavstivitPes(Pes pes);
        public void NavstivitKoala(Koala koala);
    }
    
    public class Feedr: INavstevnik {
        public void NavstivitSlona(Slon slon){
            Console.WriteLine("Slon je navstiven Feedrem a nakrmlen");
        }

        public void NavstivitPes(Pes pes){
            Console.WriteLine("Pes je navstiven Feedrem a nenakrmlen");
        }

        public void NavstivitKoala(Koala koala){
            Console.WriteLine("Koala je navstiven Feedrem a nakrmlen");
        }
    }


    public class Kluk : INavstevnik{
        public void NavstivitSlona(Slon slon){
            Console.WriteLine("Slon je navstiven Klukem");
        }

        public void NavstivitPes(Pes pes){
            Console.WriteLine("Pes je navstiven Klukem");
        }

        public void NavstivitKoala(Koala koala){
            Console.WriteLine("Koala je navstiven Klukem");
        }
    }




    public interface IZvire{
        public void PrijmoutNavstevnika(INavstevnik navstevnik);
    }


    public class Slon : IZvire{
        public void PrijmoutNavstevnika(INavstevnik navstevnik){
            navstevnik.NavstivitSlona(this);
        }
    }

    public class Pes : IZvire{
        public void PrijmoutNavstevnika(INavstevnik navstevnik){
            navstevnik.NavstivitPes(this);
        }
    }

    public class Koala : IZvire{
        public void PrijmoutNavstevnika(INavstevnik navstevnik){
            navstevnik.NavstivitKoala(this);
        }
    }
}