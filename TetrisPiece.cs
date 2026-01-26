using System.Numerics;
using GameClass;
using GridClass;
using Raylib_cs;

namespace TetrisPieceClass;

public class TetrisPiece
{
    public Game game;
    public Color colour;
    public List<Vector2> gridPositions = new List<Vector2>();

    int[] extremes = [0, 0, 0, 0];

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
            Raylib.DrawText($"{extremes[i]}", 20, 20 * i, 25, Color.White);
        }


        for (int i = 0; i < gridPositions.Count; i++)
        {
            Raylib.DrawText($"{gridPositions[i]}", 20, 20 * i + 80, 25, Color.White);
        }


    }

    public void SetExtremes()
    {
        int smallestX = 100;
        int biggestX  = -1;

        for (int i = 0; i < gridPositions.Count; i++)
        {
            if (gridPositions[i].X < smallestX) smallestX = (int)gridPositions[i].X;
            if (gridPositions[i].X > biggestX)  biggestX = (int)gridPositions[i].X;
        }

        extremes[0]  = smallestX;
        extremes[1] = biggestX;


        int smallestY = 100;
        int biggestY  = -1;

        for (int i = 0; i < gridPositions.Count; i++)
        {
            if (gridPositions[i].Y < smallestY) smallestY = (int)gridPositions[i].Y;
            if (gridPositions[i].Y > biggestY)  biggestY  = (int)gridPositions[i].Y;
        }

        extremes[2] = smallestY;
        extremes[3] = biggestY;

        for (int i = 0; i < extremes.Length; i++)
        {
            Raylib.DrawText($"{extremes[i]}", 20, 20 * i, 25, Color.White);
        }


        for (int i = 0; i < gridPositions.Count; i++)
        {
            Raylib.DrawText($"{gridPositions[i]}", 20, 20 * i + 80, 25, Color.White);
        }
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

    public void NewMove(Vector2 movement)
    {
        SetExtremes();

        
    }

    public bool newCanMoveDown()
    {
        int x = (int)gridPositions[3].X;
        if (game.grid.grid[extremes[3] + 1][x].isBorder || game.grid.grid[extremes[3] + 1][x].isOccupied) return false;
        return true;
    }

    public bool CanMoveDown()
    {
        for (int i = 0; i < gridPositions.Count; i++)
        {
            int x = (int)gridPositions[i].X;
            if (game.grid.grid[extremes[3] + 1][x].isBorder || game.grid.grid[extremes[3] + 1][x].isOccupied) return false;
        }

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