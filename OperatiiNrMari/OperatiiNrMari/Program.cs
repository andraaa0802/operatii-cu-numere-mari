using System;

namespace OperatiiNrMari
{
    class Program
    {
        static void Main(string[] args)
        {
            int cif1, cif2, n = 0, m = 0, i, j, gasit = 0, aux, lungime;
            int[] nr1 = new int[10001];
            int[] nr2 = new int[10001];
            Console.WriteLine("Cate cifre are primul numar?");
            cif1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Introduceti cifrele primului numar pe linii separate:");
            int[] s1 = new int[cif1];
            for (i = 0; i < cif1; i++)
            {
                s1[i] = int.Parse(Console.ReadLine());
                if (s1[i] > 9 || s1[i] < 0)
                {
                    Console.WriteLine("Trebuie sa introduceti doar cifre naturale!");
                    Environment.Exit(0);
                }
            }
            Console.WriteLine("Cate cifre are al doilea numar?");
            cif2 = int.Parse(Console.ReadLine());
            Console.WriteLine("Introduceti cifrele celui de-al doilea numar pe linii separate:");
            int[] s2 = new int[cif2];
            for (i = 0; i < cif2; i++)
            {
                s2[i] = int.Parse(Console.ReadLine());
                if (s2[i] > 9 || s2[i] < 0)
                {
                    Console.WriteLine("Trebuie sa introduceti doar cifre naturale!");
                    Environment.Exit(0);
                }
            }

            //verificare care numar este mai mare, iar apoi stocare in nr1 (numarul cel mai mare) si in nr2 (numarul cel mai mic)
            if (cif1 > cif2)
            {
                for (i = 0; i < cif1; i++)
                    nr1[i] = s1[i];
                for (j = 0; j < cif2; j++)
                    nr2[j] = s2[j];
                n = cif1;
                m = cif2;
                gasit = 1;
            }
            else if (cif2 > cif1)
            {
                for (i = 0; i < cif2; i++)
                    nr1[i] = s2[i];
                for (j = 0; j < cif1; j++)
                    nr2[j] = s1[j];
                n = cif2;
                m = cif1;
                gasit = 2;
            }
            else if (cif1 == cif2)
            {
                gasit = 0; //numerele (s1 si s2) sunt egale
                for (i = 0; i < cif1; i++)
                {
                    if (s1[i] > s2[i])
                    {
                        gasit = 1; //primul nr (s1) este mai mare
                        break;
                    }
                    else if (s2[i] > s1[i])
                    {
                        gasit = 2; //al doilea nr (s2) este mai mare
                    }
                }
                if (gasit == 0 || gasit == 1)
                {
                    for (i = 0; i < cif1; i++)
                    {
                        nr1[i] = s1[i];
                        nr2[i] = s2[i];
                    }
                }
                if (gasit == 2)
                {
                    for (i = 0; i < cif1; i++)
                    {
                        nr1[i] = s2[i];
                        nr2[i] = s1[i];
                    }
                }
                n = m = cif1;
            }

            //inversez ordinea elementelor din nr1 si nr2 (primul devine ultimul, al doilea devine penultimul etc.)
            for (i = 0; i <= (n - 1) / 2; i++)
            {
                aux = nr1[i];
                nr1[i] = nr1[n - 1 - i];
                nr1[n - 1 - i] = aux;
            }
            for (i = 0; i <= (m - 1) / 2; i++)
            {
                aux = nr2[i];
                nr2[i] = nr2[m - 1 - i];
                nr2[m - 1 - i] = aux;
            }

            lungime = n; //se stie ca n reprezinta numarul maxim de cifre

            sumaNumerelor(nr1, nr2, n, m,lungime);
            
            diferentaNumerelor(nr1, nr2, n, m, lungime, gasit);
            
            produsulNumerelor(nr1, nr2, n, m, lungime);

            impartireaNumerelor(nr1, nr2, n, m, lungime, gasit);

            ridicarePutere(s1, s2, cif1, cif2);

            radacinaPatrata(s1, s2, cif1, cif2);
           
        }

        
        private static void produsulNumerelor(int[] nr1, int[] nr2, int n, int m, int lungime)
        {
            int s, i, j, carry, poz, produs, sum, num;
            int[] rezultat_p = new int[n + m];
            s = 0;
            for (i = 0; i < n; i++)
            {
                carry = 0;
                poz = s;
                for (j = 0; j < m; j++)
                {
                    produs = nr1[i] * nr2[j];
                    sum = produs + rezultat_p[poz] + carry;
                    num = sum % 10;
                    carry = sum / 10;
                    rezultat_p[poz] = num;
                    poz++;
                }
                rezultat_p[poz] = rezultat_p[poz] + carry;
                s++;
            }
            Console.Write("Produsul este: ");
            for (i = n + m - 1; i >= 0; i--)
                Console.Write(rezultat_p[i] + " ");
            Console.WriteLine();
        }

        private static void diferentaNumerelor(int[] nr1, int[] nr2, int n, int m, int lungime, int gasit)
        {
            int i, item_nr1, item_nr2;
            int[] rezultat_d = new int[lungime];
            for (i = 0; i < lungime; i++)
            {
                item_nr1 = (i < n) ? nr1[i] : 0;
                item_nr2 = (i < m) ? nr2[i] : 0;

                if (item_nr1 > item_nr2)
                    rezultat_d[i] = item_nr1 - item_nr2;
                else if (item_nr2 > item_nr1)
                {
                    rezultat_d[i] = (item_nr1 + 10) - item_nr2;
                    if (i < lungime - 1)
                        nr1[i + 1]--;
                }
            }
            Console.Write("Diferenta este: ");
            if (gasit == 2) //daca, initial al doilea numar a fost cel mai mare, atunci diferenta numerelor este negativa si scriu un "-" in fata rezultatului
                Console.Write("- ");
            for (i = lungime - 1; i >= 0; i--)
                Console.Write(rezultat_d[i] + " ");
            Console.WriteLine();
        }

        private static void sumaNumerelor(int[] nr1, int[] nr2, int n, int m, int lungime)
        {
            int i, item_nr1, item_nr2, suma, carry;
            int[] rezultat_s = new int[lungime + 1];
            for (i = 0; i < lungime; i++)
            {
                item_nr1 = (i < n) ? nr1[i] : 0;
                item_nr2 = (i < m) ? nr2[i] : 0;

                suma = rezultat_s[i] + item_nr1 + item_nr2;
                rezultat_s[i] = suma % 10;

                carry = suma / 10;
                rezultat_s[i + 1] += carry;
            }
            Console.Write("Suma este: ");
            for (i = lungime; i >= 0; i--)
                Console.Write(rezultat_s[i] + " ");
            Console.WriteLine();
        }
        private static void radacinaPatrata(int[] s1, int[] s2, int cif1, int cif2)
        {

            if (cif1 == 1 && s1[0] == 1)
                Console.WriteLine("Radical din nr1 este: 1");
            else if (cif1 == 0 && s1[0] == 0)
                Console.WriteLine("Radical din nr1 este: 0");
            if (cif1 == 1 && s2[0] == 1)
                Console.WriteLine("Radical din nr2 este: 1");
            else if (cif2 == 0 && s2[0] == 0)
                Console.WriteLine("Radical din nr1 este: 0");
            else
                Console.WriteLine("Radicalul nu a fost implementat pt aceste numere");

        }

        private static void ridicarePutere(int[] s1, int[] s2, int cif1, int cif2)
        {
            if (cif1 == 1 && s1[0] == 1)
                Console.WriteLine("Nr1 ridicat la Nr2 este: 1");
            else if (cif2 == 1 && s2[0] == 1)
            {
                Console.WriteLine("Nr1 ridicat la Nr2 este: ");
                for (int i = 0; i < cif1; i++)
                    Console.Write(s1[i] + " ");
            }
            else if (cif1 == 1 && s1[0] == 0)
                Console.WriteLine("Nr1 ridicat la Nr2 este: 0");
            else if (cif2 == 1 && s2[0] == 0)
                Console.WriteLine("Nr1 ridicat la Nr2 este: 0");
            else
                Console.WriteLine("Ridicarea la putere nu a fost implementata pentru aceste numere");
        }

        private static void impartireaNumerelor(int[] nr1, int[] nr2, int n, int m, int lungime, int gasit)
        {
            int i, j;

            if (gasit == 0) //inseamna ca numerele sunt egale
                Console.WriteLine("Catul numerelor este: 1");
            else if (gasit == 2) //inseamna ca numarul 2 este mai mare decat numarul 1
            {
                Console.Write("Catul numerelor este: 0, iar restul este: ");
                for (i = m-1; i>=0; i--)
                    Console.Write(nr2[i]);
                Console.WriteLine();
            }
            else if (gasit == 1)
            {
                Console.WriteLine("Catul numerelor nu a fost implementat pentru aceste numere ");
            }
        }

    }
}
