using System.Globalization;
using System.Numerics;
using GameClass;
using Raylib_cs;

namespace GridClass;

public class GridPiece
{
    public static int pieceSize = 50;
    public Color colour;
    public Vector2 position;

    public bool occupied = false;

    public void Draw()
    {
        if (!occupied) colour = Color.White;
        Raylib.DrawRectangleV(position, new Vector2(pieceSize, pieceSize), colour);
    }
}
public class Grid
{
    int numRows;
    int numCols;
    public List<List<GridPiece>> grid = new List<List<GridPiece>>();

    Vector2 offset;

    public Grid(int rows, int cols)
    {
        this.numRows = rows;
        this.numCols = cols;

        grid = new List<List<GridPiece>>();

        int xOffset = (Game.width / 2) - ((numCols / 2) * GridPiece.pieceSize);
        offset = new Vector2(xOffset, 50);

    }

    public void MakeGrid()
    {
        for (int rows = 0; rows < numRows; rows++)
        {
            List<GridPiece> row = new List<GridPiece>();
            for (int cols = 0; cols < numCols; cols++)
            {
                GridPiece piece = new GridPiece();
                piece.position.X = GridPiece.pieceSize * cols + offset.X;
                piece.position.Y = GridPiece.pieceSize * rows + offset.Y;

                row.Add(piece);
            }

            grid.Add(row);
        }
    }

    public void Update()
    {
        for (int i = 0; i < grid.Count; i++)
        {
            for (int j = 0; j < grid[i].Count; j++)
            {
                grid[i][j].Draw();
            }
        }


    }
}