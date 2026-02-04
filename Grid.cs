using System.Numerics;
using GameClass;
using Raylib_cs;
using TetrisPieceClass;

namespace GridClass;

public class GridPiece
{
    public static int pieceSize = 40;
    public Color colour;
    public Vector2 position;

    public bool isBorder = false;

    public TetrisPiece? myPiece = null;

    public bool isOccupied = false;

    public void Draw()
    {
        if(isOccupied) colour = Color.Red;
        if (myPiece == null) colour = Color.White;
        else colour = myPiece.colour;

        if (isBorder) colour = Color.Red;

        Raylib.DrawRectangleV(position, new Vector2(pieceSize, pieceSize), colour);
        Raylib.DrawText($"{myPiece?.id}", (int)position.X, (int)position.Y, 10, Color.Black);
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
        numRows = rows;
        numCols = cols;

        grid = new List<List<GridPiece>>();

        int xOffset = (Game.width / 2) - ((numCols / 2) * GridPiece.pieceSize);
        int yOffset = 100;
        offset = new Vector2(xOffset, yOffset);

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

        for (int rows = 0; rows < numRows; rows++)
        {
            grid[rows][0].isBorder = true;
            grid[rows][numCols - 1].isBorder = true;
        }

        for (int cols = 0; cols < numCols; cols++)
        {
            grid[numRows - 1][cols].isBorder = true;
        }
    }

    public void Update()
    {
        for (int rows = 0; rows < grid.Count; rows++)
        {
            for (int cols = 0; cols < grid[rows].Count; cols++)
            {
                grid[rows][cols].Draw();
            }
        }
    }
}