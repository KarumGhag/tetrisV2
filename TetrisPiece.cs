using System.Numerics;
using GameClass;
using Raylib_cs;

namespace TetrisPieceClass;

public class TetrisPiece
{
    public Game game;
    public Color colour;
    public List<Vector2> gridPositions = new List<Vector2>();

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
        }   
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