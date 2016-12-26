using System;

namespace TestMaze
{
	public static class MazeDirections 
	{

		public const int Count = 4;

		public static MazeDirection RandomValue 
		{
			get 
			{
                //return (MazeDirection)Constants.random.Next(0, Count);
                return (MazeDirection)0;
            }
		}

		public static MazeDirection[] opposites = 
		{
			MazeDirection.South,
			MazeDirection.West,
			MazeDirection.North,
			MazeDirection.East
		};

		public static MazeDirection[] directions = 
		{
			MazeDirection.North,
			MazeDirection.East,
			MazeDirection.South,
			MazeDirection.West
		};

		public static MazeDirection GetOpposite (this MazeDirection direction) 
		{
			return opposites[(int) direction];
		}
		
		static IntVector2[] vectors = 
		{
			new IntVector2(0, 1),
			new IntVector2(1, 0),
			new IntVector2(0, -1),
			new IntVector2(-1, 0)
		};
		
		public static IntVector2 ToIntVector2 (this MazeDirection direction) 
		{
			return vectors[(int) direction];
		}

		public static MazeDirection ToDirection (this IntVector2 vector) 
		{
			for (int i = 0; i < vectors.Length; i++) 
			{
				if (vector.x == vectors[i].x && vector.z == vectors[i].z) 
				{
                    return MazeDirections.directions[i];
				}
			}
			return MazeDirection.East;
		}
	}
}