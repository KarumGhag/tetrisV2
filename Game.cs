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


    public float time;

    public bool canSoftDrop = true;
    public float softDropCoolDown = 0.1f;
    public float lastSoftDropTime = 0;

    public Grid grid = new Grid(21, 11);
    
    TetrisPiece? activePiece;


    public List<Timer> timers = new List<Timer>();

    public void Run()
    {

        Raylib.InitWindow(width, height, "Game");

        grid.MakeGrid();

        TPiece tpiece = new TPiece(this);
        IPiece ipiece = new IPiece(this);

        activePiece = tpiece;

        Timer gravityTimer = new Timer(1, "GravityTimer", true);


        gravityTimer.active = true;
        gravityTimer.StartTimer();
        gravityTimer.TimeEndCompleted += OnGravityTimerEnded;


        Timer softDropTimer = new Timer(0.1f, "SoftDropTimer");
        softDropTimer.TimeEndCompleted += SoftTimerEnded;


        timers.Add(gravityTimer);
        timers.Add(softDropTimer);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.Black);
            grid.Update();


            time += Raylib.GetFrameTime();

            foreach(Timer timer in timers)
            {
                timer.Update();
            }

            // if (Raylib.IsKeyDown(KeyboardKey.W) && time - lastSoftDropTime > softDropCoolDown) {activePiece.Move(new Vector2(0, 1)); lastSoftDropTime = time;}
            if (Raylib.IsKeyDown(KeyboardKey.W) && canSoftDrop) {activePiece.Move(new Vector2(0, 1)); softDropTimer.StartTimer(); canSoftDrop = false; }
            
            activePiece.Draw();



            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    private void OnGravityTimerEnded()
    {
        activePiece?.Move(new Vector2(0, 1));
    }

    private void SoftTimerEnded()
    {
        canSoftDrop = true;
    }
}

public class Timer
{

    public delegate void TimeEndSignal();
    public event TimeEndSignal? TimeEndCompleted;


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
        
        TimeEndCompleted?.Invoke();

        if (autoRestart) StartTimer();
    }
}
