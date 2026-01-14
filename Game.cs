using Raylib_cs;
using System.Numerics;

namespace GameClass;

public class Game
{
    public static int width = 800;
    public static int height = 1080;


    public float time;

    public void Run()
    {

        Raylib.InitWindow(width, height, "Game");


        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.Black);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}