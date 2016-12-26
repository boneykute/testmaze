using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TestMaze
{
	public class MazeGenerator
	{
		public bool isChange = false;
		private List<int> listRandomPath = new List<int> ();
		private List<MazePath> listPathCache = new List<MazePath> ();

		List<MazeVertex> activeVertices = new List<MazeVertex> ();

		//Use Krukal's algorithm to generate maze
		public static MazePath GenerateKrukal (MazeVertex[,] vertices, MazePath drawPath)
		{
			//List all path in the maze
			List<MazePath> paths = new List<MazePath> ();
			List<MazePath> existedPaths = drawPath.BreakIntoConnectedPaths ();
            Console.WriteLine("EXISTED PATH: " + existedPaths.Count);
			//Add path from existing draw path
			existedPaths.ForEach ((p) => {
				paths.Add (p);
			});

			//Create more path from single cell - each cell is a path
			for (int i = 0; i < vertices.GetLength (0); i++) {
				for (int j = 0; j < vertices.GetLength (1); j++) {
					bool existInPaths = false;
					MazeVertex vertex = vertices [i, j];
					foreach (MazePath path in existedPaths) {
						if (path.Contain (vertex)) {
							existInPaths = true;
							break;
						}
					}
                    if (!existInPaths) {
						MazePath newPath = new MazePath ();
						newPath.Add (vertex);
						paths.Add (newPath);
					}	
				}
			}
			return GetPath (paths, drawPath);

		}


		private static MazePath GetPath(List<MazePath> paths, MazePath drawPath)
		{
			//Start connecting path util we have only one path
			while (paths.Count > 1) {

                //Get a random path
                //				MazePath path = paths [Constants.random.Next(paths.Count)];
                MazePath path = paths[0];

                //Find a random neighbor for it
                List<MazePath> neighbors = path.CanConnectedNeighbors (paths);

				//Error cant find any neighbor 
				if (neighbors.Count == 0) {
					Console.WriteLine ("Can find any neighbor for path " + path);
					break;
				}
                //				MazePath neighbor = neighbors [Constants.random.Next(neighbors.Count)]; 
                MazePath neighbor = neighbors[0];

                //Merge two path
                path.Merge (neighbor);

				//Remove neighbor from paths - it was swallowed
				paths.Remove (neighbor);
			}
			paths[0].entrancePosition = drawPath.entrancePosition;
			paths[0].exitPosition = drawPath.exitPosition;

			return paths [0];
		}

		void VisitVertex (MazeVertex[,] vertices, MazeVertex ver)
		{
			if (!ver.isVisited)
			{
				ver.isVisited = true;	
				activeVertices.Add (ver);
			}
			List<MazeVertex> neighBors = MazeVertex.GetConnectableNeighbors (vertices, ver);

			List<MazeVertex> randomNeighbors = new List<MazeVertex> ();
			while (neighBors.Count > 0)
			{
				int index = 0;
				MazeVertex randomVer = neighBors[index];
				randomNeighbors.Add (randomVer);
				neighBors.Remove (randomVer);
			}

			foreach (MazeVertex otherVer in randomNeighbors) {
				if (!otherVer.isVisited) {
					ver.Connect (otherVer);
					VisitVertex (vertices, otherVer);
					return;
				}
			}
			activeVertices.Remove (ver);
			if (activeVertices.Count > 0)
				VisitVertex (vertices, activeVertices[activeVertices.Count - 1]);
		}
	}
}