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
    
    TetrisPiece activePiece;

    public void Run()
    {

        Raylib.InitWindow(width, height, "Game");

        grid.MakeGrid();

        TPiece tpiece = new TPiece(this);

        activePiece = tpiece;


        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.Black);
            grid.Update();


            time += Raylib.GetFrameTime();
            if (time > 1) {activePiece.Move(new Vector2(0, 1)); time = 0;}
            activePiece.Draw();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}