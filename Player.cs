using static Raylib_cs.Raylib;
using Raylib_cs;
using System.Numerics;

namespace game
{
    public class Player
    {
        private float posX;
        private float posY;

        public float SetPosX
        {
            set{posX = value;}
        }
        public float SetPosY
        {
            set{posY = value;}
        }

        public Vector2 pos = new Vector2();
        public Image dmut = new Image();
        public Texture2D dmutT = new Texture2D();

        public void LoadAssets()
        {
            dmut = Raylib.LoadImage("./assets/ish.png");
            dmutT = Raylib.LoadTextureFromImage(dmut);
        }

        public Player(float posX, float posY)
        {
            this.posX = posX;
            this.posY = posY;
            LoadAssets();
        }



        public void CheckInFrame()
        {
            if(posX > GetScreenWidth())
            {
                posX = 0;
            }
            if(posX < 0 - dmutT.width)
            {
                posX = GetScreenWidth();
            }
            if(posY > GetScreenHeight())
            {
                posY = 0 - dmutT.height;
            }
            if(posY < 0 - dmutT.height)
            {
                posY = GetScreenHeight();
            }
        }

        public void Update()
        {
            float dt = GetFrameTime();
            float timer = 0;
            timer += dt;
            float speed = 850 + timer*20;
            if(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
            {
                posX += dt * speed;
            }
            if(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            {
                posX -= dt * speed;
            }
            if(Raylib.IsKeyDown(KeyboardKey.KEY_UP))
            {
                posY -= dt * speed;
            }
            if(Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
            {
                posY += dt * speed;
            }


            CheckInFrame();


            pos.X = posX;
            pos.Y = posY;

            DrawTexture(dmutT, (int)posX, (int)posY, Color.RAYWHITE);
            
        }
    }
}