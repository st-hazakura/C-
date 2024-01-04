namespace space{
    class Gym{
        static void Main(){
            Console.WriteLine("Zvolte členství \n ");
            Console.WriteLine("1 - Basic \n2 - Platinum");
            string? vstup = Console.ReadLine();

            switch (vstup){
                case "1":
                    ConcreteCreatorA creatorA = new ConcreteCreatorA();
                    IProduct productA = creatorA.CreateProduct(1330);
                    Console.WriteLine($"\nProduct A: \n {productA.Nazev_Clenstvi} - {productA.Popis_Clenstvi} \n Cena od {productA.Vrat_Cenu_Clenstvi()} Kč");
                    break;
                case "2":
                    ConcreteCreatorB creatorB = new ConcreteCreatorB();
                    IProduct productB = creatorB.CreateProduct(1780);
                    Console.WriteLine($"\nProduct B: \n{productB.Nazev_Clenstvi} - {productB.Popis_Clenstvi} \n Cena od {productB.Vrat_Cenu_Clenstvi()} Kč");
                    break;
                default:
                    Console.WriteLine("Neplatný vstup");
                    break;
            }
        }


        public interface IProduct{
            string? Nazev_Clenstvi{get; }
            string? Popis_Clenstvi{get; }
            int? Vrat_Cenu_Clenstvi();
        }


        public class Product_A: IProduct{
            public string Nazev_Clenstvi{get; private set;}
            public string Popis_Clenstvi{get; private set;}
            public int cena_clenstvi{get; private set;}

            public Product_A(int cena){
                Nazev_Clenstvi = "BASIC";
                Popis_Clenstvi = "MĚSÍČNĚ \n Vstup: Cvičební zóny, Skupinové lekce , Wellness po cvičení";
                this.cena_clenstvi =  cena;
            }

            public int? Vrat_Cenu_Clenstvi(){
                return this.cena_clenstvi;
            }
        }


        public class Product_B: IProduct{
            public string Nazev_Clenstvi{get; private set;}
            public string Popis_Clenstvi{get; set;}
            public int cena_clenstvi{get; set;}

            public Product_B(int cena){
                Nazev_Clenstvi = "PLATINUM";
                Popis_Clenstvi = "MĚSÍČNĚ \n Neomezený vstup: Cvičební zóny, Skupinové lekce, Wellness";
                this.cena_clenstvi = cena;
            }

            public int? Vrat_Cenu_Clenstvi(){
                return this.cena_clenstvi;
            }
        }


        public abstract class Creator{
            public abstract IProduct CreateProduct(int cena_clenstvi); 
        }


        public class ConcreteCreatorA: Creator{
            public override IProduct CreateProduct(int cena_clenstvi){
                return new Product_A(cena_clenstvi);
            }
        }


        public class ConcreteCreatorB: Creator{
            public override IProduct CreateProduct(int cena_clenstvi){
                return new Product_B(cena_clenstvi);
            }
        }

    }
}