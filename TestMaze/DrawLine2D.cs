using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace TestMaze
{
    public class DrawLine2D
    {
        public MazeVertex[,] vertices;
        public MazePath path;
        public MazePath customPath;
        public int sizeWidth;
        public int sizeHeight;

        public void SetSize(int sizeWidth, int sizeHeight, bool isLevelup = false)
        {
            if (!isLevelup)
            {
                this.sizeWidth = sizeWidth;
                this.sizeHeight = sizeHeight;
                path = new MazePath();
                customPath = new MazePath();
                vertices = new MazeVertex[sizeWidth, sizeHeight];

                for (int i = 0; i < sizeWidth; i++)
                {
                    for (int j = 0; j < sizeHeight; j++)
                    {
                        MazeVertex vertex = new MazeVertex(i, j, false);
                        vertices[i, j] = vertex;
                    }
                }
            } else
            {
                this.sizeWidth = sizeWidth;
                this.sizeHeight = sizeHeight;
                vertices = new MazeVertex[sizeWidth, sizeHeight];

                for (int i = 0; i < sizeWidth; i++)
                {
                    for (int j = 0; j < sizeHeight; j++)
                    {
                        MazeVertex vertex = new MazeVertex(i, j, false);
                        vertices[i, j] = vertex;
                    }
                }
            }
        }

        public void SetPath(MazePath p, bool isSaveCustom = false, bool isClear = true)
        {
            this.path = p;
            for (int i = 0; i < vertices.GetLength(0); i++)
            {
                for (int j = 0; j < vertices.GetLength(1); j++)
                {
                    MazeVertex ver = this.path.vertices.Find(v => v.x == i && v.z == j);
                    if (ver == null)
                    {
                        vertices[i, j].Reset();
                    }
                    else
                    {
                        vertices[i, j] = ver;
                    }
                }
            }
            if (isClear)
            {
                customPath.vertices.Clear();
            }
            if (isSaveCustom)
            {
                foreach (MazeVertex ver in this.path.vertices)
                {
                    customPath.Add(ver);

                }
            }
        }

        public bool IsValid()
        {
            if (path.HasCycle() || path.vertices.Count < sizeWidth * sizeHeight || path.BreakIntoConnectedPaths().Count > 1)
            {
                return false;
            }
            return true;
        }

        public void AutoGenerate()
        {
            int minPath = (vertices.GetLength(0) * vertices.GetLength(1)) / Constants.minPath;
            int maxPath = (vertices.GetLength(0) * vertices.GetLength(1)) / Constants.maxPath;
            if ((IsValid() && customPath.vertices.Count == 0 || customPath.vertices.Count == sizeWidth * sizeHeight) || path.vertices.Count == 0)
            {
                List<MazeVertex> pth = MazeGeneratorAStar.GeneratePath(vertices, path);
                for (int i = 0; i < pth.Count - 1; i++)
                {
                    if (pth[i].IsCanConnect(pth[i + 1]))
                    {
                        pth[i].Connect(pth[i + 1]);
                    }
                }
                foreach (MazeVertex ver in pth)
                {
                    customPath.Add(ver);
                }
                //
                for (int i = 0; i < vertices.GetLength(0); i++)
                {
                    for (int j = 0; j < vertices.GetLength(1); j++)
                    {
                        MazeVertex ver = vertices[i, j];
                        ver.Reset();
                        ver.ResetDefaultItem();
                    }
                }
                customPath.exitPosition = path.exitPosition;
                customPath.entrancePosition = path.entrancePosition;
                SetPath(MazeGenerator.GenerateKrukal(vertices, customPath));
                customPath.RemoveAll();
            }
            else
            {
                int Count = 0;
            here:
                for (int i = 0; i < vertices.GetLength(0); i++)
                {
                    for (int j = 0; j < vertices.GetLength(1); j++)
                    {
                        MazeVertex ver = vertices[i, j];
                        if (!customPath.Contain(ver))
                        {
                            ver.Reset();
                            ver.ResetDefaultItem();
                        }
                    }
                }

                customPath.exitPosition = path.exitPosition;
                customPath.entrancePosition = path.entrancePosition;
                MazePath newPath = MazeGenerator.GenerateKrukal(vertices, customPath);
                if ((newPath.Solve().Count < minPath || newPath.Solve().Count > maxPath) && Count < 50)
                {
                    Count++;
                    goto here;
                }
                //SetPath(newPath, false, false);
                Console.WriteLine(minPath);
                Console.WriteLine(maxPath);
                Console.WriteLine(newPath.Solve().Count);
                if (newPath.Solve().Count < minPath || newPath.Solve().Count > maxPath)
                {
                    Console.WriteLine("1 - Can't create maze match rule with your handle. Please clear cell");
                }
                else if (newPath.HasCycle() || newPath.vertices.Count < sizeWidth * sizeHeight || newPath.BreakIntoConnectedPaths().Count > 1)
                {
                    Console.WriteLine("2 - Can't create maze match rule with your handle. Please clear cell");
                }
                else
                {
                    SetPath(newPath, false, false);
                }
                SetPath(newPath, false, false);
            }
        }
    }
}