using System;
using System.Collections;
using System.Collections.Generic;

namespace TestMaze
{
	class MainClass
	{
        private static void DrawAMazeFakeData()
        {
            Console.WriteLine("TEST DRAW A MAZE WITH A VALID VERTICES DATA");
            MazePath path = new MazePath();
            path.entrancePosition = new IntVector2(9, 0);
            path.exitPosition = new IntVector2(5, 5);

            String jsonData = "9___2___8_2__9_3__9_1__dcm8___2___9_2__dcm9___4___9_3__9_5__dcm9___3___8_3__9_4__9_2__dcm8___3___9_3__dcm9___5___9_4__dcm6___4___6_3__dcm7___1___7_2__8_1__dcm5___2___5_3__dcm5___3___5_2__6_3__5_4__dcm7___2___7_3__7_1__dcm7___3___6_3__7_2__7_4__dcm6___3___7_3__5_3__6_4__dcm3___5___2_5__4_5__dcm0___5___1_5__dcm2___5___1_5__3_5__dcm1___5___2_5__0_5__dcm4___5___4_4__3_5__5_5__dcm3___4___4_4__dcm4___4___3_4__4_5__dcm7___5___6_5__8_5__dcm6___5___7_5__5_5__dcm5___5___6_5__4_5__dcm8___5___8_4__7_5__dcm8___4___7_4__8_5__dcm7___4___8_4__7_5__7_3__dcm5___4___5_3__dcm2___2___2_1__3_2__1_2__2_3__dcm2___1___2_2__dcm3___2___3_1__2_2__dcm3___1___3_2__3_0__dcm3___0___3_1__dcm0___1___0_0__1_1__dcm0___0___0_1__dcm1___0___1_1__2_0__dcm1___1___1_2__1_0__0_1__dcm1___2___1_1__2_2__dcm2___0___1_0__dcm9___0___8_0__dcm6___0___7_0__5_0__dcm8___0___8_1__7_0__9_0__dcm8___1___9_1__8_0__7_1__dcm9___1___8_1__9_2__dcm7___0___8_0__6_0__dcm5___0___5_1__6_0__4_0__dcm5___1___5_0__6_1__dcm6___1___5_1__6_2__dcm6___2___6_1__dcm3___3___2_3__4_3__dcm2___4___1_4__2_3__dcm0___2___0_3__dcm0___3___0_4__0_2__dcm0___4___0_3__1_4__dcm1___4___0_4__2_4__dcm2___3___1_3__2_4__3_3__2_2__dcm1___3___2_3__dcm4___1___4_0__4_2__dcm4___0___4_1__5_0__dcm4___3___4_2__3_3__dcm4___2___4_3__4_1__dcm";
            String[] verticesStr = jsonData.Split(new String[] { "dcm" }, StringSplitOptions.None);

            for (int i = 0; i < verticesStr.Length - 1; i++)
            {
                String[] vertexAttrStr = verticesStr[i].Split(new String[] { "___" }, StringSplitOptions.None);
                //X, z
                int x = Int32.Parse(vertexAttrStr[0]);
                int z = Int32.Parse(vertexAttrStr[1]);
                // Connected vertices
                String[] connectedVerticesStr = vertexAttrStr[2].Split(new String[] { "__" }, StringSplitOptions.None);
                List<MazeVertex> connectedVertices = new List<MazeVertex>();
                for (int j = 0; j < connectedVerticesStr.Length - 1; j++)
                {
                    string tmp = connectedVerticesStr[j].Split(new String[] { "_" }, StringSplitOptions.None)[0];
                    int x1 = Int32.Parse(tmp);
                    tmp = connectedVerticesStr[j].Split(new String[] { "_" }, StringSplitOptions.None)[1];
                    int z1 = Int32.Parse(tmp);
                    MazeVertex connectedVertex = new MazeVertex(x1, z1, false);
                    connectedVertices.Add(connectedVertex);
                }

                MazeVertex vertex = new MazeVertex(x, z, false);
                vertex.connectVertices = connectedVertices;
                path.vertices.Add(vertex);
            }
            DrawMaze.DrawMazeStr(10, 6, path);
        }

        private static void DrawAMazeWithAutoGenerate()
        {
            DrawLine2D drawLine = new DrawLine2D();
            drawLine.SetSize(10, 6);
            drawLine.path.entrancePosition = new IntVector2(3, 0);
            drawLine.path.exitPosition = new IntVector2(7, 5);
            drawLine.AutoGenerate();
            DrawMaze.DrawMazeStr(drawLine.sizeWidth, drawLine.sizeHeight, drawLine.path);
 
            Console.WriteLine(drawLine.IsValid());

            //// Extend mazes
            //drawLine.SetSize(11,7, true);
            //drawLine.path.entrancePosition = new IntVector2(1, 0);
            //drawLine.path.exitPosition = new IntVector2(7, 6);

            //// Fill path to customPath
            //drawLine.customPath.vertices.Clear();
            //for(int i = 0; i < drawLine.path.vertices.Count; i++)
            //{
            //    drawLine.customPath.Add(drawLine.path.vertices[i]);
            //}
            //drawLine.customPath.entrancePosition = drawLine.path.entrancePosition;
            //drawLine.customPath.exitPosition = drawLine.path.exitPosition;
            //drawLine.AutoGenerate();
            //DrawMaze.DrawMazeStr(drawLine.sizeWidth, drawLine.sizeHeight, drawLine.path);

            //int width = Constants.random.Next(8, 11);
            //int height = Constants.random.Next(4, 7);

            //DrawLine2D drawLine = new DrawLine2D();
            //drawLine.SetSize(width, height);
            //drawLine.path.entrancePosition = new IntVector2(Constants.random.Next(width - 1), 0);
            //drawLine.path.exitPosition = new IntVector2(Constants.random.Next(width - 1), height - 1);
            //drawLine.AutoGenerate();
            //DrawMaze.DrawMazeStr(drawLine.sizeWidth, drawLine.sizeHeight, drawLine.path);

            //// Extend mazes
            //int newWidth = width + Constants.random.Next(1, 3);
            //int newHeight = height + Constants.random.Next(1, 3);
            //drawLine.SetSize(newWidth, newHeight, true);
            //drawLine.path.entrancePosition = new IntVector2(Constants.random.Next(newWidth - 1), 0);
            //drawLine.path.exitPosition = new IntVector2(Constants.random.Next(newWidth - 1), newHeight - 1);

            //drawLine.customPath.vertices.Clear();
            //for (int i = 0; i < drawLine.path.vertices.Count; i++)
            //{
            //    drawLine.customPath.Add(drawLine.path.vertices[i]);
            //}
            //drawLine.customPath.entrancePosition = drawLine.path.entrancePosition;
            //drawLine.customPath.exitPosition = drawLine.path.exitPosition;
            //drawLine.AutoGenerate();

            //Console.WriteLine(drawLine.sizeWidth + "====" + drawLine.sizeHeight);
            //DrawMaze.DrawMazeStr(drawLine.sizeWidth, drawLine.sizeHeight, drawLine.path);
        }

		public static void Main (string[] args)
		{
			Console.WriteLine ("TEST MAZE!");
            ConsoleKeyInfo keyinfo;
            do
            {
                //DrawAMazeFakeData(); 
                DrawAMazeWithAutoGenerate();
                keyinfo = Console.ReadKey();
            }
            while (keyinfo.Key != ConsoleKey.X);
        }
	}
}
