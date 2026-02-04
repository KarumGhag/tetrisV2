using Raylib_cs;
using System.Numerics;
using GridClass;
using TetrisPieceClass;
using System.Runtime.CompilerServices;

namespace GameClass;

public class Game
{
    public static int width = 800;
    public static int height = 1080;



    bool canSoftDrop = true;
    bool canMoveSide = true;
    public Grid grid = new Grid(21, 11);
    
    TetrisPiece? activePiece;


    bool dropOnNextFrame = false;

    List<Timer> timers = new List<Timer>();
    List<TetrisPiece> pieces = new List<TetrisPiece>();

    public void Run()
    {

        Raylib.InitWindow(width, height, "Game");

        grid.MakeGrid();

        TPiece tpiece = new TPiece(this);
        IPiece ipiece = new IPiece(this);
        pieces.Add(tpiece);
        pieces.Add(ipiece);
        
        activePiece = GeneratePiece();


        //Timers:

        Timer gravityTimer = new Timer(1, "GravityTimer", true);

        gravityTimer.active = true;
        gravityTimer.StartTimer();
        gravityTimer.TimerEnded += OnGravityTimerEnded;


        Timer softDropTimer = new Timer(0.1f, "SoftDropTimer");
        softDropTimer.TimerEnded += SoftTimerEnded;


        Timer sideMoveTimer = new Timer(0.1f, "SideMoveTimer");
        sideMoveTimer.TimerEnded += SideMoveTimer;


        Timer stopActiveTimer = new Timer(1f, "StopActiveTimer");
        stopActiveTimer.TimerEnded += StopActiveTimer;



        timers.Add(gravityTimer);
        timers.Add(softDropTimer);
        timers.Add(sideMoveTimer);
        timers.Add(stopActiveTimer);




        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.Black);
            grid.Update();


            // Console.WriteLine($"{activePiece.canMoveDown}");

            if (Raylib.IsKeyDown(KeyboardKey.W) && canSoftDrop) {activePiece.Move(new Vector2( 0, 1)); softDropTimer.StartTimer(); canSoftDrop = false; }
            if (Raylib.IsKeyDown(KeyboardKey.D) && canMoveSide) {activePiece.Move(new Vector2( 1, 0)); sideMoveTimer.StartTimer(); canMoveSide = false; }
            if (Raylib.IsKeyDown(KeyboardKey.A) && canMoveSide) {activePiece.Move(new Vector2(-1, 0)); sideMoveTimer.StartTimer(); canMoveSide = false; }

            if (activePiece.CanMoveDown())  { stopActiveTimer.timeLeft = stopActiveTimer.maxTime; stopActiveTimer.active = false; }
            if (!activePiece.CanMoveDown()) { stopActiveTimer.StartTimer(); }


            if (Raylib.IsKeyReleased(KeyboardKey.E)) {activePiece =  GeneratePiece();}


            

            foreach(Timer timer in timers)
            {
                timer.Update();
            }

            activePiece.Draw();

            if (dropOnNextFrame) {activePiece.Move(new Vector2(0, 1)); dropOnNextFrame = false; }

            Raylib.DrawText($"{stopActiveTimer.timeLeft}", 20, 200, 25, Color.White);
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }



    private void OnGravityTimerEnded() {dropOnNextFrame = true;}

    private void SoftTimerEnded() { canSoftDrop = true; }

    private void SideMoveTimer() { canMoveSide = true; }

    private void StopActiveTimer() { activePiece = GeneratePiece(); }



    private TetrisPiece GeneratePiece()
    {
        // create a new instance each spawn and use proper random range
        Random random = new Random();
        int idx = random.Next(0, 2); // 0 or 1

        return idx == 0 ? new TPiece(this) : (TetrisPiece)new IPiece(this);
    }
}

public class Timer
{

    public delegate void TimeEndSignal();
    public event TimeEndSignal? TimerEnded;


    public bool active;
    public float timeLeft;
    public float maxTime;
    public string name;

    public bool autoRestart;

    public Timer(float startTime, string name, bool autoRestart = false)
    {
        maxTime = startTime;
        active = false;
        this.name = name;
        this.autoRestart = autoRestart;
    }

    public void StartTimer()
    {
        timeLeft = maxTime;
        active = true;
    }
    public void Update()
    {
        if (!active) return;

        timeLeft -= Raylib.GetFrameTime();

        if (timeLeft < 0) { active = false; EndTimer(); }
    }
    public virtual void EndTimer()
    {
        
        TimerEnded?.Invoke();

        if (autoRestart) StartTimer();
    }
}
