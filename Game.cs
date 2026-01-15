using Raylib_cs;
using System.Numerics;
using GridClass;

namespace GameClass;

public class Game
{
    public static int width = 800;
    public static int height = 1080;


    public float time;

    Grid grid = new Grid(20, 10);
    

    public void Run()
    {

        Raylib.InitWindow(width, height, "Game");

        grid.MakeGrid();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.Black);

            grid.Update();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}