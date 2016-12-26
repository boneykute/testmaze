using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace TestMaze
{
    public class MazePath
    {
        public List<MazeVertex> vertices;

        public IntVector2 entrancePosition;

        public IntVector2 exitPosition;

        public MazePath()
        {
            vertices = new List<MazeVertex>();
            entrancePosition = new IntVector2(-1, -1);
            exitPosition = new IntVector2(-1, -1);
        }

        public void Add(MazeVertex vertex)
        {
            MazeVertex ver = GetVertex(vertex.x, vertex.z);
            if (ver != null)
            { 
                ver = vertex;
            }
            else
            { 
                vertices.Add(vertex);
            }
        }

        public void RemoveAll()
        {
            vertices.Clear();
        }

        public bool Contain(MazeVertex vertex)
        {
            return vertices.Contains(vertex);
        }

        public bool IsCanConnect(MazePath otherPath)
        {
            foreach (MazeVertex ver in vertices)
            {
                foreach (MazeVertex ver2 in otherPath.vertices)
                {
                    if (ver.IsCanConnect(ver2))
                        return true;
                }
            }
            return false;
        }

        public List<MazePath> CanConnectedNeighbors(List<MazePath> paths)
        {
            List<MazePath> neighbors = new List<MazePath>();
            foreach (MazePath otherPath in paths)
            {
                if (IsCanConnect(otherPath) && this != otherPath)
                {
                    neighbors.Add(otherPath);
                }
            }
            return neighbors;
        }

        //Merge two path that have no vertex in common
        public void Merge(MazePath otherPath)
        {
            if (!IsCanConnect(otherPath))
            {
                //                Console.WriteLine("Two path are not neighbors or diagonal, can not connect!");
                return;
            }

            foreach (MazeVertex ver in vertices)
            {
                bool connected = false;
                foreach (MazeVertex ver2 in otherPath.vertices)
                {
                    if (ver.IsCanConnect(ver2))
                    {
                        ver.Connect(ver2);
                        ver2.Connect(ver);
                        connected = true;
                        break;
                    }
                }
                if (connected)
                    break;
            }

            //Move all vertices from other path to current path
            foreach (MazeVertex ver in otherPath.vertices)
            {
                Add(ver);       
            }
        }

        public bool HasCycle()
        {
            if (vertices.Count == 0)
                return false;

            ResetStateVertices();
            if (VisitVertex(vertices[0], null))
            {
                return true;
            }
            else
            {
                if (TotalVisited() == vertices.Count)
                    return false;
                else
                {
                    foreach (MazeVertex ver in vertices)
                    {
                        if (!ver.isVisited)
                        {
                            if (VisitVertex(ver, null))
                            {
                                return true;
                            }
                        }   
                    }
                }
            }
            return false;
        }

        public int TotalVisited()
        {
            int total = 0;
            foreach (MazeVertex ver in vertices)
            {
                if (ver.isVisited)
                    total++;
            }
            return total;
        }

        public List<MazeVertex> Solve()
        {
            ResetStateVertices();
            MazeVertex startVer = GetVertex(entrancePosition.x, entrancePosition.z);
            MazeVertex endVer = GetVertex(exitPosition.x, exitPosition.z);

            List<MazeVertex> solvedVertices = new List<MazeVertex>();
            VisitVertex(startVer, solvedVertices, startVer, endVer);
            solvedVertices.Add(startVer);
            return solvedVertices;
        }

        public List<MazeVertex> Solve(IntVector2 startPosition, IntVector2 endPosition)
        {
            ResetStateVertices();
            MazeVertex startVer = GetVertex(startPosition.x, startPosition.z);
            MazeVertex endVer = GetVertex(endPosition.x, endPosition.z);

            List<MazeVertex> solvedVertices = new List<MazeVertex>();
            VisitVertex(startVer, solvedVertices, startVer, endVer);
            solvedVertices.Add(startVer);
            return solvedVertices;
        }

        bool VisitVertex(MazeVertex vertex, List<MazeVertex> solvedVertices, MazeVertex startVer, MazeVertex endVer)
        {
            if (vertex == null)
            {
                return false;
            }
            if (vertex.connectVertices == null)
            {
                return false;
            }
            vertex.isVisited = true;
            if (vertex == endVer)
            {
                return true;
            }
            //End leaf
            if (vertex.connectVertices.Count == 1 && vertex != startVer)
            {
                return false;
            }
            foreach (MazeVertex connectedVertex in vertex.connectVertices)
            {
                
                if (!connectedVertex.isVisited)
                {
                    if (VisitVertex(connectedVertex, solvedVertices, startVer, endVer))
                    {
                        solvedVertices.Add(connectedVertex);    
                        return true;
                    }
                }   
            }   
            return false;
        }

        //Return yes if we have visited cell that was not parent - it means we have cycle here
        bool VisitVertex(MazeVertex vertex, MazeVertex parentVertex)
        {
            vertex.isVisited = true;
            if (vertex.connectVertices == null)
            {
                vertex.connectVertices = new List<MazeVertex>();

            }
           foreach (MazeVertex connectedVertex in vertex.connectVertices)
            {
                if (connectedVertex != parentVertex)
                {
                    if (connectedVertex.isVisited)
                    {
                        connectedVertex.LoopGroupId = 2;   
                        highLighted = true;
                        return true;
                    }
                    else
                    {
                        if (VisitVertex(connectedVertex, vertex))
                        {
                            if (!highLighted)
                            {     
                                vertex.LoopGroupId = 2;
                                highLighted = true;
                            }
                            return true;
                        }
                    }
                }   
            }   
            return false;
        }

        bool highLighted = false;
        int count = 0;

        public List<MazePath> BreakIntoConnectedPaths()
        {
            List<MazeVertex> remainVertices = new List<MazeVertex>(vertices);
            List<MazePath> paths = new List<MazePath>();

            while (remainVertices.Count > 0)
            {
                Console.WriteLine(remainVertices.Count);
                count++;
                MazePath path = new MazePath();
                path.entrancePosition = entrancePosition;
                path.exitPosition = exitPosition;
                VisitCell(path, remainVertices.First(), null, remainVertices);
                paths.Add(path);

                //Store the coordinate for entrance and exit 
            }
            Console.WriteLine(paths.Count);
            Console.WriteLine(count);
            return paths;
        }

        void VisitCell(MazePath path, MazeVertex ver, MazeVertex parent, List<MazeVertex> remainVertices)
        {
            remainVertices.Remove(ver);
            path.Add(ver);
            for (int i = 0; i < ver.connectVertices.Count; i++)
            {
                MazeVertex ver2 = ver.connectVertices[i];
                if (ver2 != parent)
                {
                    VisitCell(path, ver2, ver, remainVertices);
                }
            }
        }

        void ResetStateVertices()
        {
            highLighted = false;
            foreach (MazeVertex ver in vertices)
            {
                ver.idLoop = -1;
                ver.LoopGroupId = -1;
                ver.isVisited = false;
            }
        }

        public MazeVertex GetVertex(int x, int z)
        {
            foreach (MazeVertex ver in vertices)
            {
                if (ver.x == x && ver.z == z)
                    return ver;
            }
            return null;
        }
    }

}