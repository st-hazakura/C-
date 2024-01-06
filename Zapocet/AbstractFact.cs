namespace Akce{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    public interface IAkce{
        void Saly();
        void Predstaveni();
        void Vstupenky();
    }


    public class Zal{
        public int ID { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
    }

    public class Filmy{
        public string FilmName { get; set; }
        public string Genre { get; set; }
        public string AgeRestriction { get; set; }
        public string Description { get; set; }
    }

    public class Seans{
        public int ID { get; set; }
        public int ZalID { get; set; }
        public string StartTime { get; set; }
        public string FilmName { get; set; }
        public string AgeRestriction{get;set;}
        public string Genre {get; set;}
        public string Description  {get; set;}
        public int Price {get; set;}
    }

    public class Bilet{
        public int ID { get; set; }
        public int SeansID { get; set; }
        public int SeatNumber { get; set; }
        public string FilmName {get; set;}
        public bool IsSold { get; set; }
        public int Price { get; set; }
    }

    public class Kino : IAkce
    {
        private List<Zal> Zaly;
        private List<Filmy> Films;
        private List<Seans> Seansy = new List<Seans>();
        private List<Bilet> Bilety = new List<Bilet>();

        public Kino(){
            Zaly = LoadData<Zal>(@"C:\Users\Home\Desktop\vyučovaní\Programovani\C#\Zapocet\zalykino.json");
            Films = LoadData<Filmy>(@"C:\Users\Home\Desktop\vyučovaní\Programovani\C#\Zapocet\kinoFilmy.json");
        }

        public void Saly(){
            foreach (var zal in Zaly){
                Console.WriteLine($"Зал '{zal.Name}' вмещает {zal.Capacity} человек");
            }
        }

        public void Predstaveni(){
            Random rand = new Random();
            int pocetpredstaveni = 5;
            var startTime = new TimeSpan(10, 0, 0);
            var endTime = new TimeSpan(22, 0, 0);
            var currentTime = startTime;

            while (currentTime < endTime && pocetpredstaveni>0){
                var randomFilm = Films[new Random().Next(Films.Count)];
                var randomZal = Zaly[new Random().Next(Zaly.Count)];

                Seansy.Add(new Seans{
                ID = Seansy.Count + 1,
                ZalID = randomZal.ID,
                StartTime = currentTime.ToString(@"hh\:mm"), 
                FilmName = randomFilm.FilmName,
                AgeRestriction = randomFilm.AgeRestriction,
                Genre = randomFilm.Genre,
                Description = randomFilm.Description    
                });

                for (int i = 0; i < randomZal.Capacity; i++){
                    Bilety.Add(new Bilet{
                        ID = Bilety.Count + 1,
                        SeansID = Seansy.Last().ID,
                        FilmName = randomFilm.FilmName,
                        SeatNumber = i+1,
                        IsSold = false,
                        Price = 300
                    });
                }

                currentTime = currentTime.Add(new TimeSpan(2, 0, 0));
                pocetpredstaveni --;
            }

            foreach (var seans in Seansy){
                Console.WriteLine($"Сеанс ID: {seans.ID}, Фильм: {seans.FilmName}, Начало: {seans.StartTime}, Зал: {seans.ZalID}");
                Console.WriteLine($"  Жанр: {seans.Genre}, Возрастное ограничение: {seans.AgeRestriction}");
                Console.WriteLine($"  Описание: {seans.Description}, Price: {seans.Price}\n");
            }
        }


        public void Vstupenky(){
            foreach (var bilety in Bilety){
                //var availableTickets = Bilety.Count(b => b.SeansID == seans.ID && !b.IsSold);
                //Console.WriteLine($"Сеанс '{seans.FilmName}' в {seans.StartTime}: доступных билетов - {availableTickets}");
                Console.WriteLine($"Билет ID: {bilety.ID}, Сеанс ID: {bilety.SeansID}, Доступность: {bilety.IsSold}\n" +
                $"Название фильма: {bilety.FilmName},Номер места: {bilety.SeatNumber}, Price: {bilety.Price} \n \n");
            }
        }
        
        private static List<T> LoadData<T>(string filePath){
            return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(filePath));
        }
    }



    public class ZalDivadlo{
        public string NazevSalu {get; set;}
        public Dictionary<string, SectionInfo> Sections { get; set; }
    }

    public class SectionInfo{
        public int Radek { get; set; }
        public int MistoRadku { get; set; }
    }

    public class PredstaveniDivadlo{
        public string NazevSalu {get; set;}
        public string NazvanieSpektakla {get; set;}
        public string Genre {get; set;}
        public string OpisSpektakla {get; set;}
        public string CasZacatku {get; set;}
        public string Delka {get; set;}
        public int VekOgranic {get; set;}

    }

    public class SpectaklesDivadlo{
        public string NazvanieSpektakla {get; set;}
        public string Genre {get; set;}
        public string OpisSpektakla {get; set;}
        public string Delka {get; set;}
        public int VekOgranic {get; set;}

    }

    public class BiletDivadlo{
        public int ID { get; set; }
        public string NazvanieSpektakla {get; set;}
        public string CasZacatku {get; set;}
        public int CisloRadku { get; set; }
        public int MistoRadku {get; set;}
        public string CeleMisto {get; set;}
        public int Cena {get; set;}
        public bool IsSold { get; set; }
    }

    public class Divadlo: IAkce{
        private List<ZalDivadlo> Zaly;
        private List<SpectaklesDivadlo> Spectakles;
        private List<PredstaveniDivadlo> PredstaveniD = new List<PredstaveniDivadlo>();
        private List<BiletDivadlo> Bilety = new List<BiletDivadlo>();


        public Divadlo(){
            Zaly = LoadData<ZalDivadlo>(@"C:\Users\Home\Desktop\vyučovaní\Programovani\C#\Zapocet\salDivadlo.json");
            Spectakles = LoadData<SpectaklesDivadlo>(@"C:\Users\Home\Desktop\vyučovaní\Programovani\C#\Zapocet\spectacles.json");
        }


        public void Saly(){
            foreach (var zal in Zaly){
                Console.WriteLine($"Название зала: {zal.NazevSalu}");
                foreach (var section in zal.Sections){
                    Console.WriteLine($"  Секция: {section.Key}");
                    Console.WriteLine($"    Рядов: {section.Value.Radek}");
                    Console.WriteLine($"    Мест в ряду: {section.Value.MistoRadku}");
                }
                Console.WriteLine(); // Для пустой строки между залами
            }
        }
        

        public void Predstaveni(){
            Random rand = new Random();
            int pocetpredstaveni = 5;
            int unikedId = 1;

            var cenasekci = new Dictionary<string, int>{
                {"Parter", 500},
                {"Balcony", 700},
                {"Box", 1000}
            };

            while (pocetpredstaveni>0){
                var randomZal = Zaly[new Random().Next(Zaly.Count)];
                var randomSpectacle = Spectakles[new Random().Next(Spectakles.Count)];

                int rok = DateTime.Now.Year;
                int mesic = rand.Next(1, 13);
                int den = rand.Next(1, 31);
                int cas = rand.Next(16, 22);
                int minuta = rand.Next(0, 4) * 15;
                DateTime date = new DateTime(rok, mesic, den, cas, minuta, 0);
                string caspredstaveni = date.ToString("MM-dd HH:mm");

                int celkovaCapacita = 0;
                foreach (var section in randomZal.Sections){
                    int mistaVSekci = section.Value.Radek * section.Value.MistoRadku;
                    celkovaCapacita += mistaVSekci;
                }

                PredstaveniD.Add(new PredstaveniDivadlo{
                    NazevSalu = randomZal.NazevSalu,
                    NazvanieSpektakla = randomSpectacle.NazvanieSpektakla,
                    Genre = randomSpectacle.Genre,
                    OpisSpektakla = randomSpectacle.OpisSpektakla,
                    CasZacatku = caspredstaveni,
                    Delka = randomSpectacle.Delka,
                    VekOgranic = randomSpectacle.VekOgranic
                });


                foreach (var section in randomZal.Sections){
                    for (int radek = 1; radek<= section.Value.Radek; radek ++){
                        for (int mistoradku = 1; mistoradku <= section.Value.MistoRadku; mistoradku ++){
                            
                            int cenabiletu = cenasekci[section.Key];
                            string mesto = $" Sekce: {section.Key}, Radek: {radek}, Misto: {mistoradku}";
                            Bilety.Add(new BiletDivadlo{
                                ID = unikedId++,
                                NazvanieSpektakla = randomSpectacle.NazvanieSpektakla,
                                CasZacatku = caspredstaveni,
                                CisloRadku = radek,
                                MistoRadku = mistoradku,
                                CeleMisto = mesto,
                                Cena = cenabiletu,
                                IsSold = new Random().Next(2) == 0
                            });
                        }
                    }
                pocetpredstaveni --;
                }
            }
            Console.WriteLine("Все представления:");
            foreach (var predstaveni in PredstaveniD)
            {
                Console.WriteLine($"Название зала: {predstaveni.NazevSalu}, Название спектакля: {predstaveni.NazvanieSpektakla}");
                Console.WriteLine($"Жанр: {predstaveni.Genre}, Описание: {predstaveni.OpisSpektakla}");
                Console.WriteLine($"Время начала: {predstaveni.CasZacatku}, Продолжительность: {predstaveni.Delka}, Возрастное ограничение: {predstaveni.VekOgranic}");
                Console.WriteLine();
            }

        }


        public void Vstupenky(){
            foreach (var bilety in Bilety){
                Console.WriteLine(  $"Билет ID: {bilety.ID}, Nazvanie: {bilety.NazvanieSpektakla}, Cas zacatku: {bilety.CasZacatku}\n" +
                                    $"Radek: {bilety.CisloRadku}, Misto: {bilety.MistoRadku}, CeleMisto: {bilety.CeleMisto}\n" +
                                    $"Cena: {bilety.Cena}, Dostupnost: {bilety.IsSold}\n \n");
            }
        }


        private static List<T> LoadData<T>(string filePath){
            return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(filePath));
        }
    }


    public interface IAkceFactory{
        IAkce CreateAkce();
    }




    public class KinoProDetiFactory: IAkceFactory{
        public IAkce CreateAkce(){
            return new Kino();
        }
    }


    public class DivadloProDetiFactory: IAkceFactory{
        public IAkce CreateAkce(){
            return new Divadlo();
        }
    }

    public class AgeRestrict{
        public int Age{get; set;}
        public string Description{get; set;}

        public AgeRestrict(int age, string description){
            Age = age;
            Description = description;
        }

        public int GetAge(){
            return Age;
        }
    }

    public class KinoProDospeleFactory: IAkceFactory{
        AgeRestrict AgeRestrict;        

        public IAkce CreateAkce(){
            return new Kino();
        }
    }

    public class DivadloProDospeleFactory: IAkceFactory{
        public IAkce CreateAkce(){
            return new Divadlo();
        }
    }



    public class Client{
        private IAkce Akce;

        public Client(IAkceFactory factory){
            Akce = factory.CreateAkce();
        }

        public void Saly(){
            Akce.Saly();
        }
        
        public void Predstaveni(){
            Akce.Predstaveni();
        }

        public void Vstupenky(){
            Akce.Vstupenky();
        }
    }
}   