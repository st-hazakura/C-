using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace Space{
    class Program{
        static void Main(){
            DalkovyOvladacTelevize televize = new DalkovyOvladacTelevize(new Televize());
            
            bool pouzivani = true;
            
            Console.WriteLine("    _______\n   |   _   |\n   |  | |  |\n   |  | |  |\n   |  |_|  |\n   |_______|\n   | 1 2 3 |\n   | 4 5 6 |\n   | 7 8 9 |\n   |   0   |\n   | *   # |\n    -------");
            Console.WriteLine("\nNezapomente pred pozitim zapnout device \n \n");

            Console.WriteLine("Vyberte instrukce \n \n" +
                "z -> Zapnout device \n" +
                "v -> Vypnout device \n"+
                "o -> Zjistit jeli device zapnuty nebo vyptuny \n \n"+
                "n -> Nastavit hlasitost \n"+
                "m -> Mute \n" +
                "h -> Zjistit aktualni hlasitost \n \n" +
                "k -> Vybrat kanal \n" +
                "a -> Zjistit aktualni kanal \n \n" +
                "i -> Ukazat instrukce znovu" +
                "e -> Skoncit");


            while(pouzivani){


                string prikaz = Console.ReadLine();
                if (prikaz == "n" || prikaz =="h" || prikaz =="k" || prikaz == "a" || prikaz == "m"){
                    if (!televize.Stav()){
                        Console.WriteLine("Device je vypnute \nPro pouzivani dalsich funkci musite zapnout televize");
                        continue;
                    }
                }                

                switch(prikaz) {

                    case "z" :
                        televize.Zapnout();
                        break;
                    case "v":
                        televize.Vypnnout();
                        break;
                    case "o":
                        televize.JeZapnute();
                        break;
                    case "n":
                        Console.WriteLine("Prosim zvolte hlasitost \n");
                        int hlasitost = int.Parse(Console.ReadLine());
                        televize.NastavitHlasitost(hlasitost);
                        break;
                    case "h":
                        televize.GetHlasitost();
                        break;
                    case "m":
                        televize.Mute();
                        break;
                    case "k":
                        Console.WriteLine("Prosim zvolte cislo kanalu \n");
                        int cislo_kanalu = int.Parse(Console.ReadLine());
                        televize.NastavitKanal(cislo_kanalu);
                        break;
                    case "a":
                        televize.GetKanal();
                        break;
                    case "e": 
                        pouzivani =false;
                        break;
                    case "i":
                        Console.WriteLine("Vyberte instrukce \n \n" +
                            "z -> Zapnout device \n" +
                            "v -> Vypnout device \n"+
                            "o -> Zjistit jeli device zapnuty nebo vyptuny \n \n"+
                            "n -> Nastavit hlasitost \n"+
                            "m -> Mute \n" +
                            "h -> Zjistit aktualni hlasitost \n \n" +
                            "k -> Vybrat kanal \n" +
                            "a -> Zjistit aktualni kanal \n \n" +
                            "i -> Ukazat instrukce znovu" +
                            "e -> Skoncit");
                        break;                      
                }
            }

        }


        public interface IDevice{
            bool Stav();
            void JeZapnute();
            void Zapnout();
            void Vypnnout();
            void GetHlasitost();
            void NastavitHlasitost(int nastavena_hlasitost);
            void GetKanal();
            void NastavitKanal(int nastaveny_kanal);
        }


        public class Radio : IDevice{

            protected bool jeli_zapnuty;
            protected int hlasitost;
            protected int kanal;

            public Radio(){
                jeli_zapnuty = false;
                hlasitost = 0;
                kanal = 1;
            }

            public bool Stav(){
                return jeli_zapnuty;
            }

            public void JeZapnute(){
                if (jeli_zapnuty){Console.WriteLine("Radio je zapnute");}
                else{Console.WriteLine("Radio je vypnute");}
            }
            public void Zapnout(){
                if (!jeli_zapnuty){
                    Console.WriteLine("Radio bylo zapnuto");
                    jeli_zapnuty = true;
                }else{
                    Console.WriteLine("Radio jiz zapnuto, zkuste jine tlacitko");
                }
            }
            public void Vypnnout(){
                if (!jeli_zapnuty){
                    Console.WriteLine("Radio bylo vypnuto");
                    jeli_zapnuty = false;
                }else{
                    Console.WriteLine("Radio jiz vypnuto, zkuste jine tlacitko");
                }                
            }

            public void GetHlasitost(){
                if (jeli_zapnuty){ Console.WriteLine($"Aktualni hlasitost je {hlasitost}");}
                else{ Console.WriteLine("Radio je vypnute");}
            }

            public void NastavitHlasitost(int nastavena_hlasitost){
                if(jeli_zapnuty){
                    if (nastavena_hlasitost>=0 && nastavena_hlasitost<=100){
                        hlasitost = nastavena_hlasitost;
                        Console.WriteLine($"Hlasitost nastavena na {hlasitost}");
                    }else{
                        Console.WriteLine($"Nelze nastavit, je pouze od 0 do 100 \n hlasitost Radio je: {hlasitost} ");
                    }
                }else{ Console.WriteLine("Radio je vypnute, nemuzete nastavit hlasitost");}
            }

            public void GetKanal(){
                if(jeli_zapnuty){Console.WriteLine($"Aktualni kanal je {kanal}");}
                else{ Console.WriteLine("Radio je vypnute");}
            }

            public void NastavitKanal(int nastaveny_kanal){
                if (jeli_zapnuty){
                    if (nastaveny_kanal>=1 && nastaveny_kanal<=100){
                        kanal = nastaveny_kanal;
                        Console.WriteLine($"Prepnuli jste na kanal {kanal}");
                    }else{
                        Console.WriteLine($"Takovy kanal neexistuje, jsou pouze od 1 do 100 \nVas aktualni kanal: {kanal}");
                    }
                }else{ Console.WriteLine("Radio je vypnute nemuzete nastavit kanal");}
            }

        }


        public class Televize : IDevice{
            protected bool jeli_zapnuty;
            protected int hlasitost;
            protected int kanal;

            public Televize(){
                jeli_zapnuty = false;
                hlasitost = 0;
                kanal = 1;
            }

            public bool Stav(){
                return jeli_zapnuty;
            }

            public void JeZapnute(){
                if(jeli_zapnuty){ Console.WriteLine("Televize je zapnuta");}
                else{Console.WriteLine("Televize je vypnuta");}
            }

            public void Zapnout(){
                Tlacitko();
            }

            public void Vypnnout(){
                Tlacitko();
            }

            public void Tlacitko(){
                if (!jeli_zapnuty){
                    Console.WriteLine("Televize byla zapnuta");
                    jeli_zapnuty = true;
                }else{
                    Console.WriteLine("Televize byla vypnuta");
                    jeli_zapnuty = false;
                }
            }


            public void GetHlasitost(){
                if (jeli_zapnuty){Console.WriteLine($"Aktualni hlasitost je {hlasitost}");}
                else{ Console.WriteLine("Televize je vypnuta \nPro pouzivani dalsich funkci musite zapnout televize");}
            }

            public void NastavitHlasitost(int nastavena_hlasitost){
                if (jeli_zapnuty){
                    if (nastavena_hlasitost>=0 && nastavena_hlasitost<= 100){
                        hlasitost = nastavena_hlasitost;
                        Console.WriteLine($"Hlasitost je zmenena na {hlasitost}");
                    }else {
                        Console.WriteLine($"Nelze nastavit, je pouze od 0 do 100 \n hlasitost Televize je: {hlasitost}");
                    }
                }else{
                    Console.WriteLine("Televize je vypnuta \nPro pouzivani dalsich funkci musite zapnout televize");
                }
            }


            public void GetKanal(){
                if(jeli_zapnuty){Console.WriteLine($"Aktualni kanal je {kanal}");}
                else{ Console.WriteLine("Televize je vypnuta \nPro pouzivani dalsich funkci musite zapnout televize");}
            }

            public void NastavitKanal(int nastaveny_kanal){
                if(jeli_zapnuty){
                    if (nastaveny_kanal>=1 && nastaveny_kanal<=50){
                        kanal = nastaveny_kanal;
                        Console.WriteLine($"Prepnuli jste na kanal {kanal}");
                    }else{
                        Console.WriteLine($"Takovy kanal neexistuje, jsou pouze od 1 do 50 \nVas aktualni kanal: {kanal}");
                    }
                }else{ Console.WriteLine("Televize je vypnuta \nPro pouzivani dalsich funkci musite zapnout televize");}
            }            
        }


        public abstract class DalkovyOvladac{
            protected IDevice Device{get;set;}

            public DalkovyOvladac(IDevice device){
                Device = device;
            }

            public bool Stav(){
                return Device.Stav();
            }

            public void JeZapnute(){
                Device.JeZapnute();
            }

            public void Vypnnout(){
                Device.Vypnnout();
            }

            public void Zapnout(){
                Device.Zapnout();
            }


            public void GetHlasitost(){
                Device.GetHlasitost();
            }

            public void NastavitHlasitost(int nastavena_hlasitost){
                Device.NastavitHlasitost(nastavena_hlasitost);
            }


            public void GetKanal(){
                Device.GetKanal();
            }

            public void NastavitKanal(int nastaveny_kanal){
                Device.NastavitKanal(nastaveny_kanal);
            }
        }


        public class DalkovyOvladacRadio : DalkovyOvladac{
            public DalkovyOvladacRadio(IDevice device) : base(device){}
        }


        public class DalkovyOvladacTelevize : DalkovyOvladac{
            public DalkovyOvladacTelevize(IDevice device) : base(device){}

            public void Mute(){
                if(Device.Stav()){ Device.NastavitHlasitost(0);}
                else{ Console.WriteLine("Televize je vypnuta \nPro pouzivani dalsich funkci musite zapnout televize");}
            }
        } 
    }
}