using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hazi2
{
    internal class Program
    {
        class lancolt_lista<T>
        {
            class Elem<T>
            {
                public Elem<T> elozo;
                public T tartalom;
                public Elem<T> kovetkezo;

                public Elem()
                {
                    this.elozo = this;
                    this.kovetkezo = this;
                }

                public Elem(Elem<T> valasztott, T tartalom)
                {
                    this.tartalom = tartalom;
                    Elem<T> elem = this;
                    elem.elozo = valasztott;
                    elem.kovetkezo = valasztott.kovetkezo;
                    valasztott.kovetkezo.elozo = elem;
                    valasztott.kovetkezo = elem;
                }

                public void Delete(Elem<T> e)
                {
                    e.kovetkezo.elozo = e.elozo;
                    e.elozo.kovetkezo = e.kovetkezo;
                }
            }

            private Elem<T> fejelem;
            private Elem<T> aktelem;
            private int count;

            public lancolt_lista()
            {
                fejelem = new Elem<T>();
                aktelem = fejelem;
                count = 0;
            }

            public int Count { get => count; }
            public bool Üres_e() => count == 0;
            private Elem<T> Utolsó() => fejelem.elozo;
            public void Add(T tartalom)
            {
                Elem<T> elem = new Elem<T>(Utolsó(), tartalom);
                count++;
            }
            public void Remove(T tartalom)
            {
                aktelem = fejelem;
                while (!aktelem.tartalom.Equals(tartalom))
                {
                    aktelem = aktelem.kovetkezo;
                }
                aktelem.Delete(aktelem);
                count--;
            }
            public T this[int i]
            {
                get
                {
                    aktelem = fejelem;
                    for (int j = 0; j < i; j++)
                    {
                        aktelem = aktelem.kovetkezo;
                    }
                    return aktelem.tartalom;
                }
            }
            public void InsertAt(int i, T tartalom)
            {
                aktelem = fejelem;
                for (int j = 0; j < i; j++)
                {
                    aktelem = aktelem.kovetkezo;
                }
                Elem<T> item = new Elem<T>(aktelem, tartalom);
                count++;
            }
            public void RemoveAt(int i)
            {
                aktelem = fejelem;
                for (int j = 0; j < i; j++)
                {
                    aktelem = aktelem.kovetkezo;
                }
                aktelem.Delete(aktelem);
                count--;
            }
            public void Diagnosztika()
            {
                aktelem = fejelem.kovetkezo;
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine(aktelem.tartalom);
                    aktelem = aktelem.kovetkezo;
                }
            }
            public void Sort()
            {
                aktelem = fejelem.kovetkezo.kovetkezo;
                while (aktelem != fejelem)
                {
                    Elem<T> valasztott = aktelem;
                    T key = aktelem.tartalom;

                    while (valasztott.elozo != fejelem && Comparer<T>.Default.Compare(valasztott.elozo.tartalom, key) > 0)
                    {
                        valasztott.tartalom = valasztott.elozo.tartalom;
                        valasztott = valasztott.elozo;
                    }

                    valasztott.tartalom = key;
                    aktelem = aktelem.kovetkezo;
                }
            }
            public lancolt_lista(lancolt_lista<T> eredeti)
            {
                fejelem = new Elem<T>();

                if (eredeti.count > 0)
                {
                    Elem<T> aktEredeti = eredeti.fejelem.kovetkezo;
                    Elem<T> aktUj = fejelem;

                    while (aktEredeti != eredeti.fejelem)
                    {
                        Elem<T> ujElem = new Elem<T>(aktUj, aktEredeti.tartalom);
                        aktUj.kovetkezo = ujElem;
                        aktUj = ujElem;

                        aktEredeti = aktEredeti.kovetkezo;
                    }

                    count = eredeti.count;
                }
                else
                {
                    count = 0;
                }
            }
            public lancolt_lista(List<T> eredeti)
            {
                fejelem = new Elem<T>();

                if (eredeti.Count() > 0)
                {
                    int i = 0;
                    Elem<T> aktUj = fejelem;

                    while (i < eredeti.Count())
                    {
                        Elem<T> ujElem = new Elem<T>(aktUj, eredeti[i]);
                        aktUj.kovetkezo = ujElem;
                        aktUj = ujElem;

                        i++;
                    }

                    count = eredeti.Count();
                }
                else
                {
                    count = 0;
                }
            }
        }

        static void Main(string[] args)
        {
            lancolt_lista<int> list = new lancolt_lista<int>();
            list.Add(5);
            list.Add(3);
            list.Add(1);
            list.Add(6);
            list.Add(9);
            list.Diagnosztika();
            Console.WriteLine("1. feladat:");
            Console.WriteLine(list.Count);
            Console.WriteLine("2. feladat:");
            Console.WriteLine(list.Üres_e());
            Console.WriteLine("3. feladat:");
            list.Remove(1);
            list.Diagnosztika();
            Console.WriteLine("4. feladat:");
            Console.WriteLine(list[4]);
            Console.WriteLine("5. feladat:");
            list.InsertAt(0, 1);
            list.Diagnosztika();
            Console.WriteLine("6. feladat:");
            list.RemoveAt(4);
            list.Diagnosztika();
            Console.WriteLine("7. feladat:");
            list.Sort();
            list.Diagnosztika();
            Console.WriteLine("8. feladat:");
            lancolt_lista<int> list2 = new lancolt_lista<int>(list);
            list2.Diagnosztika();
            Console.WriteLine("9. feladat:");
            List<int> llist = new List<int>();
            llist.Add(5);
            llist.Add(3);
            llist.Add(1);
            llist.Add(6);
            llist.Add(9);
            lancolt_lista<int> list3 = new lancolt_lista<int>(llist);
            list2.Diagnosztika();
        }
    }
}
