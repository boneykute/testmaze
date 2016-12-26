namespace TestMaze
{
	[System.Serializable]

	public struct IntVector2
	{
		public int x;
		public int z;

		public IntVector2 (int x, int z, bool visited = false)
		{
			this.x = x;
			this.z = z;
		}

		public IntVector2 Top ()
		{
			return new IntVector2 (x, z + 1);
		}

		public IntVector2 Down ()
		{
			return new IntVector2 (x, z - 1);
		}

		public IntVector2 Left ()
		{
			return new IntVector2 (x - 1, z);
		}

		public IntVector2 Right ()
		{
			return new IntVector2 (x + 1, z);
		}

        public string ToString()
        {
            return x + " : " + z;
        }
	}
}