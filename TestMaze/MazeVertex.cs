using System;
using System.Collections;
using System.Collections.Generic;

namespace TestMaze
{
	[System.Serializable]
	public class MazeVertex
	{
		public int x, z;
		public bool isVisited;
		public int idLoop = -1;
		public int LoopGroupId = -1;
		public List<MazeVertex> connectVertices;
		public string floorId;
		public string wallId;
		public string[] wallItems = new string[4];

		public string[] GetWallItems ()
		{
			return wallItems;
		}

		public MazeVertex (int x, int z, bool visited = false)
		{
			this.x = x;
			this.z = z;
			this.isVisited = visited;
			this.connectVertices = new List<MazeVertex> ();
            floorId = "";
            wallId = "";
            for (int i = 0; i < wallItems.Length; i++) {
				wallItems [i] = Constants.wallDefault;
			}
		}

		public void ResetDefaultItem ()
		{
			floorId = Constants.floorDefault;
			wallId = Constants.wallDefault;
		}
			
		public void Connect (MazeVertex otherVer)
		{
			if (!connectVertices.Contains (otherVer)) {
				connectVertices.Add (otherVer);
				otherVer.Connect (this);
			}
		}

		public void Disconnect (MazeVertex otherVer)
		{
			if (connectVertices.Contains (otherVer)) {
				connectVertices.Remove (otherVer);
				otherVer.Disconnect (this);
			}

		}

		public void DisconnectAll ()
		{
			List<MazeVertex> temp = new List<MazeVertex> (connectVertices);
			foreach (MazeVertex otherVer in temp) {
				Disconnect (otherVer);
			}
		}


		//Check if two vertex are connected or not
		public bool IsConnected (MazeVertex otherVer)
		{
			return connectVertices.Contains (otherVer);
		}

		public bool IsNeighbor (MazeVertex other)
		{
			return Math.Abs (x - other.x) <= 1 && Math.Abs (z - other.z) <= 1;
		}

		public bool IsCanConnect (MazeVertex other)
		{
			return IsNeighbor (other) && !IsDiagonal (other);
		}

		public bool IsDiagonal (MazeVertex other)
		{
			return Math.Abs (x - other.x) == 1 && Math.Abs (z - other.z) == 1;
		}

		//Reset status for all vertex
		public void Reset ()
		{
			isVisited = false;
			DisconnectAll ();
			wallItems = new string[4];
		}

		public static List<MazeVertex> GetConnectableNeighbors (MazeVertex[,] vertices, MazeVertex ver)
		{
			List<MazeVertex> neighbors = new List<MazeVertex> ();
			for (int i = ver.x - 1; i <= ver.x + 1; i++) {
				for (int j = ver.z - 1; j <= ver.z + 1; j++) {
					if (i >= 0 && i < vertices.GetLength (0) && j >= 0 && j < vertices.GetLength (1)) {
						MazeVertex otherVer = vertices [i, j];
						if (ver != otherVer && ver.IsNeighbor (otherVer) && !ver.IsDiagonal (otherVer))
							neighbors.Add (otherVer);
					}
				}   
			}   
			return neighbors;
		}
	}
}