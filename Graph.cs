using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork
{
    internal class Graph
    {

        private int[,] matr;
        private string[] names;
        private int countEdges;

        private string PathSource = @"..\AdjacencyMatrix.txt";
        internal static readonly char[] separator = [' ', '\n', '\t', '\r'];

        public bool IsOriened;
        public bool HasRings;
        

        public Graph()
        {
            Reload();
        }

        public void Reload()
        {
            using FileStream stream = new FileStream(PathSource, FileMode.OpenOrCreate, FileAccess.Read);
            byte[] buff = new byte[stream.Length];
            stream.Read(buff, 0, buff.Length);
            string rezult = Encoding.Default.GetString(buff);
            string[] namePlMatr = rezult.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            int countNodes = (int)Math.Sqrt(namePlMatr.Length);
            if (countNodes == 0)
                throw new ArgumentException("Empty file");

            matr = new int[countNodes, countNodes];
            names = new string[countNodes];
            for (int i = 0; i < countNodes; i++)
                names[i] = namePlMatr[i];
            try
            {
                for (int i = 0; i < countNodes; i++)
                {
                    for (int j = 0; j < countNodes; j++)
                    {
                        matr[i, j] = int.Parse(namePlMatr[i * countNodes + j + countNodes]);
                    }
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Error in file: {e.Message}");
            }

            for (int i = 0; i < names.Length; i++)
            {
                for (int j = i; j < names.Length; j++)
                {
                    if (matr[i, j] == matr[j, i] && i != j && matr[i, j] != 0)
                        countEdges++;
                    else if (i == j && matr[i, j] != 0)
                        countEdges++;
                    else if (matr[i, j] != 0)
                        countEdges += 2;
                }
            }

            CheckOriented();
        }

        private void CheckOriented()
        {
            HasRings = false;
            IsOriened = false;
            for (int i = 0; i < names.Length; i++)
            {
                for (int j = i; j < names.Length; j++)
                {
                    if (matr[i, j] != matr[j, i]) 
                    {
                        IsOriened = true;
                        HasRings = true;
                    }
                    if (matr[i, j] == -matr[j, i] && matr[i, j] != 0)
                        IsOriened = true;
                }
            }
        }

        public void MinimumFrame()
        {
            if(IsOriened == true)
            {
                Console.WriteLine("Algorithm of Kruscal is used only for undirected graph");
                return;
            }

            (int, int, int)[] edges = new (int, int, int)[names.Length * names.Length];
            int cnt = 0;
            for (int i = 0; i < names.Length; i++)
            {
                for (int j = i; j < names.Length; j++)
                {
                    edges[cnt] = (i, j, matr[i, j]);
                    cnt++;
                }
            }
            edges = Sorts.Sort(edges);

            
            DisjointSet set = new DisjointSet(names.Length);
            DynamicArray<(int, int)> includedEdges = new DynamicArray<(int, int)>();
            foreach (var edge in edges)
            {
                if (edge.Item3 == 0)
                    continue;

                if (set.Find(edge.Item1) == set.Find(edge.Item2) && set.Find(edge.Item1) != null)
                    continue;

                set.Union(edge.Item1, edge.Item2);
                includedEdges.AddLast((edge.Item1, edge.Item2));
                if (set.IsFull())
                    break;
            }

            int LenGaph = 0;
            for (int i = 0; i < includedEdges.Count; i++)
            {
                Console.WriteLine(names[includedEdges[i].Item1].ToString() + " " + names[includedEdges[i].Item2].ToString());
                LenGaph += matr[includedEdges[i].Item1, includedEdges[i].Item2];
            }
            Console.WriteLine(LenGaph);
        }

        public override string ToString()
        {
            string rez = "";
            foreach (var item in names)
            {
                rez += item + " ";
            }
            rez += "\n";

            for (int i = 0; i < names.Length; i++)
            {
                for (int j = 0; j < names.Length; j++)
                {
                    rez += matr[i, j].ToString() + " ";
                }
                rez += "\n";
            }
            return rez;
        }

        public void PresentAsAdjacencyList()
        {
            DynamicArray<DynamicArray<(string, int)>> AdjacencyList = new DynamicArray<DynamicArray<(string, int)>>(names.Length);
            for (int i = 0; i < names.Length; i++)
            {
                AdjacencyList.AddLast(new DynamicArray<(string, int)>(2));
                for (int j = 0; j < names.Length; j++)
                {
                    if (matr[i, j] != 0)
                        AdjacencyList[i]!.AddLast((names[j], matr[i, j]));
                }
            }

            for (int i = 0; i < names.Length; i++)
            {
                Console.WriteLine(names[i] + ": " + AdjacencyList[i]!.ToString());
            }
        }

        public struct DisjointSet(int count = 1000)
        {
            readonly int?[] vertexes = new int?[count];
            readonly int[] ranks = new int[count];

            public void NewSet(int x)
            {
                vertexes[x] = x;
                ranks[x] = 0;
            }

            public int? Find(int x)
            {
                if (vertexes[x] == null)
                    return null;
                return (x == vertexes[x] ? x : vertexes[x] = Find((int)vertexes[x]!));
            }

            public void Union(int? x, int? y)
            {
                if (Find((int)x!) == null && x != null)
                    NewSet((int)x);
                if (Find((int)y!) == null && y != null)
                    NewSet((int)y);

                if ((x = Find((int)x!)) == (y = Find((int)y!)) && x == null && y == null)
                    return;

                if (ranks[(int)x!] < ranks[(int)y!])
                    vertexes[(int)x] = y;
                else
                {
                    vertexes[(int)y] = x;
                    if (ranks[(int)x] == ranks[(int)y])
                        ranks[(int)x]++;
                }
            }

            public bool IsFull()
            {
                int? t = vertexes[0];
                foreach (var item in vertexes)
                {
                    if (item == null || item != t)
                        return false;
                }
                return true;
            }
        }
        
    }


}

namespace System
{
    public static partial class MyMath
    {
        public static uint Fibbonaci(int x)
        {
            uint rez = 0;
            for (uint i = 1; i <= x; i++)
            {
                rez += i;
            }
            return rez;
        }
    }
}
