using Raylib_cs;
using System.Numerics;
using GridClass;
using TetrisPieceClass;

namespace GameClass;

public class Game
{
    public static int width = 800;
    public static int height = 1080;


    public float time;

    public Grid grid = new Grid(21, 11);
    

    public void Run()
    {

        Raylib.InitWindow(width, height, "Game");

        grid.MakeGrid();

        TPiece tpiece = new TPiece(this);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.Black);

            grid.Update();
            tpiece.Draw();
            tpiece.SetExtremes();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}