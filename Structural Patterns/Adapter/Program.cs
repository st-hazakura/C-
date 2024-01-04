using System.ComponentModel.Design;

namespace Space{
    class Program{
        static void Main(){
            Ridic ridic = new Ridic();
            Auto auto = new Auto();
            ridic.Dopravit(auto);
            Velbloud velbloud = new Velbloud();
            IDoprava velbloud_dopravni = new VerbloudDopravaAdapter(velbloud);
            ridic.Dopravit(velbloud_dopravni);
        }

        public interface IDoprava{
            void Ridit();
        }

        public class Auto: IDoprava{
            public void Ridit(){
                Console.WriteLine("Ridit");
            }
        }

        public class Ridic{
            public void Dopravit(IDoprava dopravni_prostredek){
                dopravni_prostredek.Ridit();
            }
        }

        public interface IZvire{
            void Jit();
        }

        public class Velbloud{
            public void Jit(){
                Console.WriteLine("Verbloud jde po pisku");
            }
        }


        //Adapter 
        public class VerbloudDopravaAdapter: IDoprava{
            public Velbloud verbloud;

            public VerbloudDopravaAdapter(Velbloud verb){
                verbloud = verb;
            }

            public void Ridit(){
                verbloud.Jit();
            }
        }
        
        

    }
}