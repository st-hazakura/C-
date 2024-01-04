using System.Security.Cryptography.X509Certificates;

namespace Space{
    class Program{
        static void Main(){
            Postava bojovnik = new Postava(new BojovnikUtokFactory());
            bojovnik.ProvedAkce();
            bojovnik.ProvedUtok();
            Console.WriteLine("\n");
            Postava lucisnik = new Postava(new LucisnikStealthFactory());
            lucisnik.ProvedAkce();
            lucisnik.ProvedUtok();
        }

        // Все вариации одного и того же объекта должны жить в одной иерархии классов.
        public interface Zbran{
            void Utok();
        }

        public interface Pohyb{
            void HybatSe();
        }

        public class Mec: Zbran{
            public void Utok(){
                Console.WriteLine("Mec utokuje");
            }
        }

        public class Luk: Zbran{
            public void Utok(){
                Console.WriteLine("Luk strili");
            }
        }


        public class Beh: Pohyb{
            public void HybatSe(){
                Console.WriteLine("Bezi");
            }
        }

        public class Skvatovani: Pohyb{
            public void HybatSe(){
                Console.WriteLine("Jdes pomalu");
            }
        }


        public interface IPostavaFactory{
            Pohyb PohnoutPostavou();
            Zbran AkcePomociZbrane();
        }

        //фабрики всех сочетаний
        public class BojovnikUtokFactory: IPostavaFactory{
            public Pohyb PohnoutPostavou(){
                return new Beh();
            }

            public Zbran AkcePomociZbrane(){
                return new Mec();
            }
        }

        public class LucisnikStealthFactory: IPostavaFactory{
            public Pohyb PohnoutPostavou(){
                return new Skvatovani();
            }

            public Zbran AkcePomociZbrane(){
                return new Luk();
            }
        }


        public class Postava{
            private Pohyb pohyb;
            private Zbran zbran;

            public Postava(IPostavaFactory factory){
                pohyb = factory.PohnoutPostavou();
                zbran = factory.AkcePomociZbrane();
            }

            public void ProvedAkce(){
                pohyb.HybatSe();
            }

            public void ProvedUtok(){
                zbran.Utok();
            }
        }
    }
}