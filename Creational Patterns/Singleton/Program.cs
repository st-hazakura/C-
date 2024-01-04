namespace Space {
    class Program {
        static void Main() {
            Singletone objekt1 = Singletone.GetInstance();
            objekt1.Cas_vytvoreni();
            Thread.Sleep(500);
            Singletone objekt2 = Singletone.GetInstance();
            objekt2.Cas_vytvoreni();
        }
    }

    public class Singletone {
        private static Singletone instance;
        public string Cas { get; private set; }

        private Singletone() {
            Cas = DateTime.Now.ToString("HH:mm:ss");
        }

        public static Singletone GetInstance() {
            if (instance == null) {
                instance = new Singletone();
            }
            return instance;
        }

        public void Cas_vytvoreni() {
            Console.WriteLine(Cas);
        }
    }
}
