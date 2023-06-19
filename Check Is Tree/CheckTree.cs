using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsATree
{
    public class CheckTree
    {
        //===============================================================================
        //Your Code is Here:
        public struct Vertice
        {
            public  string Name;
            public  List <string>adj ;

        };
        public static bool IsTree(string[] vertices, List<KeyValuePair<string, string>> edges)
        {
           // edges = new List<KeyValuePair<string, string>>();
            int NumberEdges = edges.Count;
            int NumberVertices = vertices.Length;
            Dictionary<string, List<string>> ArrayVertices = new Dictionary<string,
                List<string>>();
            for (int j = 0; j < NumberEdges; j++)
            {
                if (ArrayVertices.ContainsKey(edges[j].Key) == false)
                {
                    List<string> s3 = new List<string>();
                    s3.Add(edges[j].Value);
                    ArrayVertices.Add(edges[j].Key, s3);
                    edges[j].Key.Contains("");
                }
                else if((ArrayVertices.ContainsKey(edges[j].Key)==true))
                {
                    List<string> s4 = ArrayVertices[edges[j].Key];
                    s4.Add( edges[j].Value);
                    ArrayVertices[edges[j].Key] = s4;
                }
            }
            for (int j = 0; j < NumberVertices; j++)
            {
                List<string> s4 = new List<string>();
                if (ArrayVertices.ContainsKey(vertices[j])==false)
                {
                    ArrayVertices.Add(vertices[j], s4);
                }
            }
           /* for(int i=0;i< ArrayVertices.Keys.Count;i++)
            {
                Console.WriteLine("v [" + (i+1)+"] = "+ ArrayVertices.Keys.ElementAt(i));
                List<string> s1 = new List<string>();
                s1 = ArrayVertices.Values.ElementAt(i);
                for (int j=0;j< s1.Count;j++)
                {
                    Console.WriteLine("list [" + (j + 1) + "] = "+s1[j]);
                    
                }
                Console.WriteLine("========================================================");
                
            }*/
            ////
            ///
            int[] color = new int[NumberVertices];
            Dictionary<string, int> visitedVertices = new Dictionary<string, int>();
            Dictionary<string, int> stackVertices = new Dictionary<string, int>();
            for (int i = 0; i < NumberVertices; i++)
            {
                visitedVertices.Add(vertices[i],0);
                stackVertices.Add(vertices[i], 0);
            }
            for (int i = 0; i <NumberVertices; i++)
            {
              if(CycleInGraph(vertices[i],visitedVertices, stackVertices,ArrayVertices)==true)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool CycleInGraph(string v, Dictionary<string, int>
            visitedVertices, Dictionary<string, int> stackVertices, 
            Dictionary<string, List<string>> ArrayVertices)
        {
            if(visitedVertices[v]==1)
            {
                return true;
            }
            if(stackVertices[v]==1)
            {
                return false;
            }
            visitedVertices[v] = 1;
            stackVertices[v] = 1;
            List<string> adj = ArrayVertices[v];
            foreach (string c in adj)
            {
                if (CycleInGraph(c, visitedVertices, stackVertices,ArrayVertices))
                {
                    return true;
                }
            }
            stackVertices[v] = 0;
            visitedVertices[v] = 0;
            return false;
        }
       /*public static bool IsCycleInGraph(
           Vertice[] ArrayVertices,string [] stack,string v,bool[] visitStatue)
        {
            List<string> children = new List<string>();
            for (int j = 0; j < stack.Length; j++)
            {
                if (stack[j].Equals(v))
                {
                 return true;
                }
            }
            int ind=-1;
            for (int j = 0; j < ArrayVertices.Length; j++)
            {
                if( ArrayVertices[j].Name.Equals(v)==true )
                {
                    ind= j;
                    if ((visitStatue[j] == true))
                    {
                        return false;
                    }
                }

            }
            if (ind != -1)
            {
                children = ArrayVertices[ind].adj;
                if (children.Count != 0)
                {
                    stack[ind] = v;
                    visitStatue[ind] = true;

                    for (int j = 0; j < children.Count; j++)
                    {
                        if (IsCycleInGraph(ArrayVertices, stack, ArrayVertices[j].Name,
                        visitStatue) == true)
                        {
                            return true;
                        }
                    }
                }

            }
            for (int j = 0; j < stack.Length; j++)
            {
                stack[0] = "0";
                visitStatue[j] = false;
            }
                return false;
        }

        //===============================================================================
        /////////////////////////////////////////
        /*bool[] visitStatue = new bool[NumberVertices];
        Vertice[] ArrayVertices = new Vertice[NumberVertices];
        string []stack = new string[NumberVertices];
        Console.WriteLine("NumberVertices ="+ NumberVertices);
        Console.WriteLine("NumberEdges ="+ NumberEdges);
        Console.WriteLine("N =" + vertices[0]);
        for (int i = 0; i < NumberVertices; i++)
        {
            stack[i] = "0";
            visitStatue[i] = false;
            ArrayVertices[i].Name = vertices[i];
            List<string> a = new List<string>();
            for (int j = 0; j < NumberEdges; j++)
            {
                if (edges[j].Key.Equals(vertices[i]))
                {
                    a.Add(edges[j].Value);
                   // ArrayVertices[i].adj.Add(edges[j].Value);
                }
            }
            ArrayVertices[i].adj = a;
        }
        Console.WriteLine("f adj =" + ArrayVertices[0].adj[0]);
        Console.WriteLine("f adj =" + ArrayVertices[0].adj[1]);
        //Console.WriteLine("f adj =" + ArrayVertices[0].adj[2]);
        /*bool b = true;
        for (int j = 0; j < NumberVertices; j++)
        {
            if(IsCycleInGraph( ArrayVertices, stack, ArrayVertices[j].Name, 
                visitStatue) ==true)
            {
                Console.WriteLine("index cycle =" + ArrayVertices[j].Name);
                b= false;
                break;
            }
        }
      return b;*/
        //===============================================================================
    }
}
