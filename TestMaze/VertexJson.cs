using System;
using System.Collections;
using System.Collections.Generic;

namespace TestMaze
{
	public class VertexJson
	{
		public int x;
		public int z;
		public List<IntVector2> connectVertices;

		public VertexJson (int x, int y, List<IntVector2> connectVertices)
		{
			this.x = x;
			this.z = z;
			this.connectVertices = connectVertices;
		}
	}
}

