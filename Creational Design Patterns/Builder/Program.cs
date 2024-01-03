using System.ComponentModel;

namespace spase{
    class Program{
        static void Main(){
            IBilder bilder_cheeseburgeru = new Cheeseburger();
            IBilder bilder_vegan = new Vegan_burger();

            Director director = new Director(bilder_vegan);
            director.VytvorBurger();
            
            Burger burger = bilder_vegan.VratBurger();
            Console.WriteLine(burger);
        }

        public class Burger{
            public string? Nazev_Burgeru{get; set;}
            public string? Syr{get; set;}
            public string? Omacka{get; set;}
            public string? Okurky{get; set;}
            public string? Maso{get; set;}

            public override string ToString(){
                return $"{Nazev_Burgeru} s ingrediencemi: {Syr}; {Maso}; {Omacka}; {Okurky} ";
            }
        }


        public interface IBilder{
            void Pridej_Syr();
            void Pridej_Omacku();
            void Pridej_Okurky();
            void Pridej_Maso();
            Burger VratBurger();
        }


        public class Cheeseburger : IBilder{
            private Burger burger;

            public Cheeseburger(){
                this.burger = new Burger();
                this.burger.Nazev_Burgeru = "Cheeseburger";
            }

            public void Pridej_Omacku(){
                burger.Omacka = "Kecup";
            }

            public void Pridej_Maso(){
                burger.Maso = "Maso hovězí";
            }

            public void Pridej_Okurky(){
                burger.Okurky = "Okurky Sladkokysele";
            }

            public void Pridej_Syr(){
                burger.Syr = "Syr";
            }

            public Burger VratBurger(){
                return burger;
            }
        }


        public class Vegan_burger : IBilder{
            private Burger burger;
            public Vegan_burger(){
                this.burger = new Burger();
                this.burger.Nazev_Burgeru = "Vegan Burger";
            }

            public void Pridej_Omacku(){
                burger.Omacka = "Majoneza";
            } 

            public void Pridej_Maso(){
                burger.Maso = "Maso Veganske";
            }

            public void Pridej_Okurky(){

            }

            public void Pridej_Syr(){
                burger.Syr = "Syr vegansky";
            }

            public Burger VratBurger(){
                return burger;
            }
        }


        public class Director{
            private IBilder bilder;

            public Director(IBilder bilder){
                this.bilder = bilder;
            }

            public void VytvorBurger(){
                bilder.Pridej_Omacku();
                bilder.Pridej_Maso();
                bilder.Pridej_Okurky();
                bilder.Pridej_Syr();
                bilder.VratBurger();
            }
        }
    
    
    
    }
}

