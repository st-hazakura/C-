using System.Text.RegularExpressions;

namespace Space{
    class Programm{
        static void Main(){
            Otvor otvor = new Otvor(5);
            Valec valec = new Valec(5);

            Console.WriteLine(otvor.VejdeFigue(valec));

            Krychle krychle = new Krychle(8);
            IValcovyOtvor valecadapt = new KrychleAdapter(krychle);
            Console.WriteLine(otvor.VejdeFigue(valecadapt));
        }

        public class Otvor{
            protected int radius;

            public Otvor(int r){
                radius = r;
            }

            public void RadiusOtvoru(){
                Console.WriteLine($"Radius otvoru: {radius}");
            }

            public bool VejdeFigue(IValcovyOtvor otvor){
                return otvor.VratitRadiusValce() <= radius;
            }
        }

        public interface IValcovyOtvor{
            int VratitRadiusValce();
        }

        public class Valec: IValcovyOtvor{
            protected int radius;
            public Valec(int r){
                radius = r;
            }

            public int VratitRadiusValce(){
                return radius;
            }
        }

        public interface IKrychle{
            int VratitStranuKrychle();
        }

        public class Krychle: IKrychle{
            protected int sirka;

            public Krychle(int s){
                sirka = s;
            }

            public int VratitStranuKrychle(){
                return sirka;
            }
        }

        public class KrychleAdapter: IValcovyOtvor{
            Krychle krychle;

            public KrychleAdapter(Krychle k){
                krychle = k;
            }

            public int VratitRadiusValce(){
                return (int)(krychle.VratitStranuKrychle() * Math.Sqrt(2) / 2);
            }
        }
    }
}