namespace space{
    class Clone{
        static void Main(){
            Uzivatel uzivatel = new Uzivatel(4658, "cervena");
            Uzivatel clone = (Uzivatel)uzivatel.Clone();
            uzivatel.VratitInformace();
            clone.VratitInformace();
        }


        public interface IPrototype{
            public IPrototype Clone();
            void VratitInformace();
        }

        public class Uzivatel: IPrototype{
            private int ID{get; set;}
            public string Barva{get; set;}

            public Uzivatel(int id, string barva){
                ID = id;
                Barva = barva;
            }

            public IPrototype Clone(){
                return new Uzivatel(ID, Barva);
            }

            public void VratitInformace(){
                Console.WriteLine("ID: " + ID + " Barva: " + Barva);
            }
        }
    }
}