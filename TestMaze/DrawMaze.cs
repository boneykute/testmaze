using System;
using System.Collections;
using System.Collections.Generic;

namespace TestMaze
{
	public class DrawMaze
	{
		public static void DrawMazeStr (int width, int height, MazePath path) {
			List<MazeVertex> vertices = path.vertices;
			// sort cell by z
			for (int i = 0; i < vertices.Count - 1; i++) {
				for (int j = i; j < vertices.Count; j++) {
					if (vertices[i].z < vertices[j].z) {
						MazeVertex tmp = vertices[i];
						vertices[i] = vertices[j];
						vertices[j] = tmp;
					}
				}
			}

			// separated by rows
			List<List<MazeVertex>> rows = new List<List<MazeVertex>>();
			List<MazeVertex> row1 = new List<MazeVertex>();
			for (int i = 0; i < vertices.Count; i++) {
				row1.Add(vertices[i]);
				if ((i + 1) % (width) == 0) {
					rows.Add(row1);
					row1 = new List<MazeVertex>();
				}
			}

			// sort separated row by x
			List<List<MazeVertex>> sortedRows = new List<List<MazeVertex>>();
			for (var k = 0; k < rows.Count; k++) {
				List<MazeVertex> row = rows[k];
				for (int i = 0; i < row.Count - 1; i++) {
					for (int j = i; j < row.Count; j++) {
						if (row[i].x > row[j].x) {
							var tmp = row[i];
							row[i] = row[j];
							row[j] = tmp;
						}
					}
				}
				sortedRows.Add(row);
			}


			// create cell data
			List<List<String>> mazeStringArray = new List<List<string>>();
			for (int i = 0; i < sortedRows.Count; i++) {
				List<MazeVertex> row = sortedRows[i];
				List<String> rowString = new List<string>();
				for (int j = 0; j < row.Count; j++) {
					String cellString = createCell(row[j], path.entrancePosition, path.exitPosition);
					rowString.Add(cellString);
				}
				mazeStringArray.Add(rowString);
			}



            String mazeString = "";
            for (var i = 0; i < mazeStringArray.Count; i++)
            {
                var row = mazeStringArray[i];
                var firstLineOfRow = "";
                var secondLineOfRow = "";
                var thirdLineOfRow = "";
                for (var j = 0; j < row.Count; j++)
                {
                    String[] edgeArr = row[j].Split('\n');
                    firstLineOfRow += edgeArr[0];
                    secondLineOfRow += edgeArr[1];
                    thirdLineOfRow += edgeArr[2];
                }
                mazeString += firstLineOfRow + "\n";
                mazeString += secondLineOfRow + "\n";
                mazeString += thirdLineOfRow + "\n";
            }
            Console.WriteLine("BELOW IS A MAZE");
			Console.WriteLine(mazeString);
		}

        class DataToDraw {
            public bool top;
            public bool down;
            public bool left;
            public bool right;

            public DataToDraw(bool top, bool down, bool left, bool right)
            {
                this.top = top;
                this.down = down;
                this.left = left;
                this.right = right;
            }

            public string ToString()
            {
                return (top?"true":"false") + '-' + (down ? "true" : "false") + '-' + (left ? "true" : "false") + '-' + (right ? "true" : "false");
            }
        }

		private static String createCell(MazeVertex cellData, IntVector2 entrancePosition, IntVector2 exitPosition) {
            IntVector2 point = new IntVector2(cellData.x, cellData.z);
			IntVector2 top = point.Top();
			IntVector2 down = point.Down();
			IntVector2 left = point.Left();
			IntVector2 right = point.Right();
            
			bool topCheck = cellData.connectVertices.Find(ver => ver.x == top.x && ver.z == top.z) == null;
            bool downCheck = cellData.connectVertices.Find(ver => ver.x == down.x && ver.z == down.z) == null;
            bool leftCheck = cellData.connectVertices.Find(ver => ver.x == left.x && ver.z == left.z) == null;
            bool rightCheck = cellData.connectVertices.Find(ver => ver.x == right.x && ver.z == right.z) == null;

            DataToDraw dataToDraw = new DataToDraw(topCheck, downCheck, leftCheck, rightCheck);

            if (exitPosition.x == point.x && exitPosition.z == point.z) {
				dataToDraw.top = false;
			}
			if (entrancePosition.x == point.x && entrancePosition.z == point.z) {
                dataToDraw.down = false;
			}

            string cellString = "";
			// first line
			cellString += "+";
			if (dataToDraw.top) {
				cellString += "---";
			} else {
				cellString += "   ";
			}
			cellString += "+";
			cellString += "\n";

			// second line
			if (dataToDraw.left) {
				cellString += "|";
			} else {
				cellString += " ";
			}
			cellString += "   ";
			if (dataToDraw.right) {
				cellString += "|";
			} else {
				cellString += " ";
			}
			cellString += "\n";
			// last line
			cellString += "+";
			if (dataToDraw.down) {
				cellString += "---";
			} else {
				cellString += "   ";
			}
			cellString += "+";
            return cellString;
        }
	}
}

