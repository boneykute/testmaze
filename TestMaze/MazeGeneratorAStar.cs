using System.Collections;
using System.Collections.Generic;
using TestMaze;
using System;

namespace TestMaze
{
	public class MazeGeneratorAStar
	{
		public static List<MazeVertex> GeneratePath (MazeVertex[,] vertices, MazePath drawPath)
		{
			int minPath = (vertices.GetLength (0) * vertices.GetLength (1)) / Constants.minPath;
			int maxPath = (vertices.GetLength (0) * vertices.GetLength (1)) / Constants.maxPath;
			//int numberStep = Constants.random.Next(minPath, maxPath);
            int numberStep = 15;
            for (int i = 0; i < vertices.GetLength (0); i++) {
				for (int j = 0; j < vertices.GetLength (1); j++) {
					vertices [i, j].isVisited = false;
				}
			}

			List<MazeVertex> path = new List<MazeVertex> ();
			MazeVertex verExit = vertices [drawPath.exitPosition.x, drawPath.exitPosition.z];
			MazeVertex verG = vertices [drawPath.entrancePosition.x, drawPath.entrancePosition.z];
			vertices [drawPath.entrancePosition.x, drawPath.entrancePosition.z].isVisited = true;
			path.Add (vertices [drawPath.entrancePosition.x, drawPath.entrancePosition.z]);
  
			here:
			int remainingStep = numberStep - path.Count;
			if (remainingStep <= GetShortest (verG, verExit)) {
				isIncrease = false;
				// Random khong tang them buoc
			} else {
				isIncrease = true;
				// Random tang them buoc
			}
			MazeVertex newVer = GetNextVertex (vertices, verG, verExit);
			if (newVer != null) {
				newVer.isVisited = true;
				path.Add (newVer);
				verG = newVer;
			}
			if (verG != null) {
				if (verG != verExit) {
					goto here;
				}
			}
			return path;

		}

		public static bool isIncrease = false;

		public static MazeVertex GetNextVertex (MazeVertex[,] vertices, MazeVertex verCurrent, MazeVertex verTo)
		{
			if (verCurrent == null || verTo == null)
				return null;
			List<MazeVertex> listTrue = GetListVertex (vertices, verCurrent, verTo);
			if (listTrue != null && listTrue.Count > 0) {
                //				return listTrue [Constants.random.Next(listTrue.Count)];
                return listTrue[0];
            } else {
				isIncrease = !isIncrease;
				List<MazeVertex> listFail = GetListVertex (vertices, verCurrent, verTo);
				if (listFail != null && listFail.Count > 0) {
                    //					return listFail [Constants.random.Next(listFail.Count)];
                    return listFail[0];
                }

				return verTo;
			}
		}

		public static List<MazeVertex> GetListVertex (MazeVertex[,] vertices, MazeVertex verCurrent, MazeVertex verTo)
		{
			List<MazeVertex> listRandom = new List<MazeVertex> ();
			Dir dir;
			if (verCurrent.x > verTo.x) {
				if (isIncrease) {
					// Re phai
					dir = Dir.Right;                
				} else {
					dir = Dir.Left;  
				}
				MazeVertex ver1 = GetVertexByDirection (vertices, verCurrent, dir);
				if (ver1 != null) {
					listRandom.Add (ver1);
				}
			} else if (verCurrent.x <= verTo.x) {
				if (isIncrease) {
					dir = Dir.Left;                
				} else {
					dir = Dir.Right;  
				}
				MazeVertex ver2 = GetVertexByDirection (vertices, verCurrent, dir);
				if (ver2 != null) {
					listRandom.Add (ver2);
				}
			}
			if (verCurrent.z > verTo.z) {
				if (isIncrease) {
					dir = Dir.Up;                
				} else {
					dir = Dir.Down;  
				}
				MazeVertex ver3 = GetVertexByDirection (vertices, verCurrent, dir);
				if (ver3 != null) {
					listRandom.Add (ver3);
				}

			} else if (verCurrent.z <= verTo.z) {
				if (isIncrease) {
					dir = Dir.Down;                
				} else {
					dir = Dir.Up;  
				}
				MazeVertex ver4 = GetVertexByDirection (vertices, verCurrent, dir);
				if (ver4 != null) {
					listRandom.Add (ver4);
				}
			}
			return listRandom;
		}

		public enum Dir
		{
			Left,
			Right,
			Up,
			Down
		}

		public static MazeVertex GetVertexByDirection (MazeVertex[,] vertices, MazeVertex verCurrent, Dir dir)
		{
			switch (dir) {
			case Dir.Left:
				if (verCurrent.x - 1 >= vertices.GetLength (0) || (verCurrent.x - 1 < 0))
					return null;
				if (!vertices [verCurrent.x - 1, verCurrent.z].isVisited) {
					return vertices [verCurrent.x - 1, verCurrent.z];
				}
				return null;
                // Re trai
			case Dir.Right:
				if (verCurrent.x + 1 >= vertices.GetLength (0))
					return null;
				if (!vertices [verCurrent.x + 1, verCurrent.z].isVisited) {
					return vertices [verCurrent.x + 1, verCurrent.z];
				}
				return null;
                // Re phai
			case Dir.Up:
				if (verCurrent.z + 1 >= vertices.GetLength (1))
					return null;
				if (!vertices [verCurrent.x, verCurrent.z + 1].isVisited) {
					return vertices [verCurrent.x, verCurrent.z + 1];
				}
				return null;
			case Dir.Down:
				if (verCurrent.z - 1 >= vertices.GetLength (1) || (verCurrent.z - 1 < 0))
					return null;
				if (!vertices [verCurrent.x, verCurrent.z - 1].isVisited) {
					return vertices [verCurrent.x, verCurrent.z - 1];
				}
				return null;
			}
			return null;
		}

		public static int GetShortest (MazeVertex verFrom, MazeVertex verTo)
		{
			if (verFrom == null || verTo == null)
				return 0;
			return (Math.Abs (verFrom.x - verTo.z) + Math.Abs (verFrom.z - verTo.z));
		}
	}
}
