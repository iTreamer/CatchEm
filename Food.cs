using static Raylib_cs.Raylib;
using Raylib_cs;
using System.Numerics;

namespace game
{
    public class Food
    {
        Random rand = new Random();
        private float posX;
        private float posY;

        public Image strawberryIMG = new Image();
        public Texture2D strawberryT = new Texture2D();

        public Sound success = new Sound();
        public Vector2 pos = new Vector2();

        public Food(float posX, float posY)
        {
            this.posX = posX;
            this.posY = posY;
            LoadAssets();
        }

        public float GetPosX
        {
            get{return posX;}
        }
        public float SetPosX
        {
            set
            {
                posX = value;
            }
        }
        public float GetPosY
        {
            get{return posY;}
        }
        public float SetPosY
        {
            set
            {
                posY = value;
            }
        }
        

        public void LoadAssets()
        {
            strawberryIMG = Raylib.LoadImage("./assets/strawberry.png");
            strawberryT = Raylib.LoadTextureFromImage(strawberryIMG);
            success = Raylib.LoadSound("./assets/success.wav");
        }

        public void Update()
        {
            DrawTexture(strawberryT, (int)posX, (int)posY, Color.RAYWHITE);
            pos.X = posX;
            pos.Y = posY;
        }

        

    }
}