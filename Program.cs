using Raylib_cs;
using System.Numerics;
using System.IO;
using System.Data;
#nullable disable

namespace game;

enum GameState
{
    start,
    running,
    paused,
    stop

}
class Program
{
    

    public static bool CheckCollision(Texture2D texture1, Vector2 position1, Texture2D texture2, Vector2 position2)
    {
        Rectangle rect1 = new Rectangle(position1.X, position1.Y, texture1.width, texture1.height);
        Rectangle rect2 = new Rectangle(position2.X, position2.Y, texture2.width, texture2.height);
        if (rect1.x + rect1.width >= rect2.x &&
        rect1.x <= rect2.x + rect2.width &&
        rect1.y + rect1.height >= rect2.y &&
        rect1.y <= rect2.y + rect2.height)
        {
            return true;
        }
        return false;
    }


    public static void Main(string[] args)
    {
        string filepath = "score.txt";
        int bestScore;
        using(StreamReader reader = new StreamReader(filepath))
        {
            bestScore = int.Parse(reader.ReadLine());
        }
        Raylib.InitWindow(800, 480, "best game ever");
        int score = 0;
        float timer = 0;
        int time;
        GameState gameState = GameState.start;
        Random rand = new Random();
        Raylib.InitAudioDevice();
        Raylib.SetWindowState(ConfigFlags.FLAG_VSYNC_HINT);
        Raylib.SetTargetFPS(144);
        Player player = new Player(Raylib.GetScreenWidth()/2, Raylib.GetScreenHeight()/2);
        Food food = new Food(rand.Next(0,800), rand.Next(0,480));
        
            while (!Raylib.WindowShouldClose())
            {
                
                time = 10-(int)timer;
                
                float dt = Raylib.GetFrameTime();
                timer += dt;
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.RAYWHITE);
                
                if(timer > 11 && gameState == GameState.running)
                {
                    gameState = GameState.stop;
                }

                
                
                if(gameState == GameState.start)
                {
                    Raylib.DrawText("Try to get the most strawberries in 10 seconds!\nPress enter to start", Raylib.GetScreenWidth() - 10 - Raylib.MeasureText("Try to get the most strawberries in 10 seconds!\nPress enter to start", 32), Raylib.GetScreenHeight()/2, 32, Color.BLACK);
                    Raylib.DrawText($"Best score: {bestScore}", Raylib.GetScreenWidth()/2+70 - Raylib.MeasureText($"Best score: {bestScore}", 32), 400, 32, Color.BLACK);
                    if(Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                    {
                        gameState = GameState.running;
                        timer = 0;
                    }
                }

                else if(gameState == GameState.running)
                {
                    
                    if(CheckCollision(player.dmutT, player.pos, food.strawberryT, food.pos) && timer > 0.2)
                    {
                        food.SetPosX = rand.Next(0, Raylib.GetScreenWidth() - 32);
                        food.SetPosY = rand.Next(0, Raylib.GetScreenHeight() - 32);
                        Raylib.PlaySoundMulti(food.success);
                        score++;
                    }
                    food.Update();
                    player.Update();
                    Raylib.DrawText($"Score: {score}", 330, 30, 35, Color.BLACK);
                    Raylib.DrawText($"Time: {time}", Raylib.GetScreenWidth()/2+45 - Raylib.MeasureText($"Time: {(int)time}", 35), 430, 35, Color.BLACK);
                }

                else if (gameState == GameState.stop)
                {
                    if(score > bestScore)
                        bestScore = score;
                    Raylib.DrawText($"Game over.\nScore: {score}\nBest score: {bestScore}\nTo retry hit enter", Raylib.GetScreenWidth()/2 - Raylib.MeasureText($"Game over.\nScore: {score}\nTo retry hit enter", 48)/2, Raylib.GetScreenHeight()/2-100, 48, Color.BLACK);
                    if(Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                    {
                        gameState = GameState.running;
                        timer = 0;
                        score = 0;
                        food.SetPosX = rand.Next(0, Raylib.GetScreenWidth() - 32);
                        food.SetPosY = rand.Next(0, Raylib.GetScreenHeight() - 32);
                        player.SetPosX = rand.Next(0, Raylib.GetScreenWidth() - 32);
                        player.SetPosY = rand.Next(0, Raylib.GetScreenHeight() - 32);
                        

                    }
                }

                Raylib.EndDrawing();
                
            }
            File.WriteAllText(filepath, bestScore.ToString());
            Raylib.CloseWindow();
    }

}
