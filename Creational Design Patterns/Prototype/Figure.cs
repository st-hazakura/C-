using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace space1{
    class Program{
        static void Main(){
            Trojuhelnik trojuhelnik = new Trojuhelnik(0,0, "cervena", 20, 10);
            Trojuhelnik clone = (Trojuhelnik)trojuhelnik.Clone();
            
            Console.WriteLine(trojuhelnik);
            Console.WriteLine(clone);

            Kruznice kruznice = new Kruznice(2,2, "modra", 10);
            Kruznice clone2 = (Kruznice)kruznice.Clone();

            Console.WriteLine(kruznice);
            Console.WriteLine(clone2);
        }
    
        public abstract class Forma{
            protected int X{get; set;}
            protected int Y{get; set;}
            protected string Barva{get;set;}

            public Forma(int x, int y, string barva){ //Forma(Forma forma)
                X = x;                                   // X = forma.X;
                Y = y;                                   // Y = forma.Y;
                Barva = barva;                           // Barva = forma.Barva;
            }

            public abstract Forma Clone();
        }


        public class Trojuhelnik : Forma{
            protected int Vyska{get; set;}
            protected int Sirka{get; set;}

            public Trojuhelnik(int x, int y, string barva, int vyska, int sirka) : base(x,y, barva){
                Sirka = sirka;
                Vyska = vyska;
            }

            public override Forma Clone(){
                return new Trojuhelnik(X, Y, Barva, Vyska, Sirka);
            }

            public override string ToString(){
                return $"Pozice x: {X}, Pozice y: {Y}, Barva: {Barva}, Vyska: {Vyska}, Sirka: {Sirka}";
            }
        }


        public class Kruznice: Forma{
            protected int Radius{get; set;}

            public Kruznice(int x, int y, string barva, int radius): base(x, y, barva){
                Radius = radius;
            }

            public override Forma Clone(){
                return new Kruznice(X, Y, Barva, Radius);
            }

            public override string ToString(){
                return $"Pozice x: {X}, Pozice y: {Y}, Barva: {Barva}, Radius: {Radius}";
            }
        }
    }
}