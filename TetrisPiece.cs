using System.Numerics;
using System.Reflection.Metadata;
using GameClass;
using GridClass;
using Raylib_cs;

namespace TetrisPieceClass;

public class TetrisPiece
{
    public Game game;
    public Color colour;
    public List<Vector2> gridPositions = new List<Vector2>();

    int[] extremes = new int[4];
    Vector2[] extremesPositions = new Vector2[4];

    public TetrisPiece(Game game)
    {
        this.game = game;
    }

    public void Draw()
    {
        foreach (Vector2 vector in gridPositions)
        {
            int x = (int)vector.X;
            int y = (int)vector.Y;

            game.grid.grid[y][x].myPiece = this;
            game.grid.grid[y][x].isOccupied = true;
        }   

        for (int i = 0; i < extremes.Length; i++)
        {
            Raylib.DrawText($"{extremesPositions[i]}", 20, 20 * i, 25, Color.White);
        }


        for (int i = 0; i < gridPositions.Count; i++)
        {
            Raylib.DrawText($"{gridPositions[i]}", 20, 20 * i + 100, 25, Color.White);
        }


    }

    public void SetExtremes()
    {
        int smallestX = 100;
        int smallestXY = 100;

        int biggestX  = -1;
        int biggestXY = -1;

        for (int i = 0; i < gridPositions.Count; i++)
        {
            if (gridPositions[i].X < smallestX) {smallestX = (int)gridPositions[i].X; smallestXY = (int)gridPositions[i].Y; }

            if (gridPositions[i].X > biggestX)  {biggestX = (int)gridPositions[i].X; biggestXY = (int)gridPositions[i].Y;    }
        }

        extremes[0]  = smallestX;
        extremes[1] = biggestX;

        extremesPositions[0] = new Vector2(smallestX, smallestXY);
        extremesPositions[1] = new Vector2(biggestX, biggestXY);


        int smallestY = 100;
        int smallestYX = 100;

        int biggestY  = -1;
        int biggestYX = -1;

        for (int i = 0; i < gridPositions.Count; i++)
        {
            if (gridPositions[i].Y < smallestY) {smallestY = (int)gridPositions[i].Y; smallestYX = (int)gridPositions[i].X; }
            if (gridPositions[i].Y > biggestY)  {biggestY  = (int)gridPositions[i].Y; biggestYX = (int)gridPositions[i].X;  }
        }

        extremes[2] = smallestY;
        extremes[3] = biggestY;


        extremesPositions[2] = new Vector2(smallestYX, smallestY);
        extremesPositions[3] = new Vector2(biggestYX, biggestY  );

    }

    public void Move(Vector2 movement)
    {
        SetExtremes();
        if(!CanMoveDown()) movement.Y = 0;

        for (int i = 0; i < gridPositions.Count; i++)
        {
            Vector2 newCoord = gridPositions[i];


            newCoord += movement;

            int x = (int)gridPositions[i].X;
            int y = (int)gridPositions[i].Y;

            game.grid.grid[y][x].isOccupied = false;
            game.grid.grid[y][x].myPiece = null;

            gridPositions[i] = newCoord;
            game.grid.grid[(int)newCoord.Y][(int)newCoord.X].isOccupied = true;
            
        }
    }

    public bool CanMoveDown()
    {
        for (int i = 0; i < gridPositions.Count; i++)
        {
            int x = (int)gridPositions[i].X;
            if (game.grid.grid[extremes[3] + 1][x].isBorder || (game.grid.grid[extremes[3] + 1][x].isOccupied && game.grid.grid[extremes[3] + 1][x].myPiece != this)) return false;
        }

        return true;
    }


    public bool CanMoveSide()
    {
        

        return true;
    }


}

public class TPiece : TetrisPiece
{
    public TPiece(Game game) : base(game)
    {
        colour = Color.Purple;
        gridPositions = new List<Vector2> { new Vector2(5, 0), new Vector2(4, 1), new Vector2(5, 1), new Vector2(6, 1) };
    }
}

public class IPiece : TetrisPiece
{
    public IPiece(Game game) : base(game)
    {
        colour = Color.SkyBlue;
        gridPositions = new List<Vector2>() { new Vector2(5, 0), new Vector2(5, 1), new Vector2(5, 2), new Vector2(5, 3) };
    }
}