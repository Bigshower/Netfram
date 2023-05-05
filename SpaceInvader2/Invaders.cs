using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Spaceinvaders
{
    internal class invaders
    {
        public static int numEnemies = 25;

        const int screenWidth = 800;
        const int screenHeight = 1000;

        Player player;
        List<Bullet> bullets;
        List<Enemy> enemies;
        private Vector2 position;

        public bool moveRight = true;
        public static bool shouldChangeDirection = false;
        public bool moveDown = false;
        public float speed = 0.02f;



        bool gameOver = false;

        enum GameState { Playing, Win, Lose };
        GameState gameState = GameState.Playing;

        void init()
        {
            Raylib.InitWindow(screenWidth, screenHeight, "Space Invaders");

            float playerSpeed = 120;
            int playerSize = 40;
            Vector2 playerStart = new Vector2(screenWidth / 2, screenHeight - playerSize * 2);

            player = new Player(playerStart, new Vector2(0, 0), playerSpeed, playerSize);

            bullets = new List<Bullet>();

            enemies = new List<Enemy>();

            for (int i = 0; i < numEnemies; i++)
            {
                int row = i / 5;
                int col = i % 5;

                Vector2 position = new Vector2(col * 100 + 100, row * 100 + 100);
                Enemy enemy = new Enemy(position, new Vector2(1, 0), 50, "enemy.png", new List<Player> { player });

                enemies.Add(enemy);

            }

            Raylib.SetTargetFPS(2500);
        }

        public void GameLoop()
        {
            init();

            while (!Raylib.WindowShouldClose())
            {


                
                if (!gameOver)
                {
                    UpdateEnemies();
                    player.Update(enemies);
                    foreach (Enemy enemy in enemies.ToList())
                    {
                        enemy.Update(player);
                    }
                }

                
                Raylib.BeginDrawing();


                
                if (gameOver)
                {
                    drawGameOver();
                }
                else
                {
                    drawGame();
                }

                
                if (player.health <= 0)
                {
                    gameState = GameState.Lose;
                    gameOver = true;
                }
                else if (enemies.Count == 0)
                {
                    gameState = GameState.Win;
                    gameOver = true;
                }

                Raylib.EndDrawing();
            }
        }




        void drawGameOver()
        {
            if (gameState == GameState.Lose)
            {
                Raylib.ClearBackground(Raylib_cs.Color.RED);
                Raylib.DrawText("Game Over!", 300, 500, 50, Raylib_cs.Color.BLACK);
            }
            else if (gameState == GameState.Win)
            {
                Raylib.ClearBackground(Raylib_cs.Color.LIME);
                Raylib.DrawText("You Win!", 300, 500, 50, Raylib_cs.Color.BLACK);
            }
            Raylib.DrawText("Press ENTER to start again", 270, 600, 30, Raylib_cs.Color.BLACK);


            
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                
                init();
                gameOver = false;
            }
        }

        void drawGame()
        {
            Raylib.ClearBackground(Raylib_cs.Color.WHITE);
            player.Draw();
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw();
            }
            player.DrawScore();
        }

        void UpdateEnemies()
        {
            bool wallHit = false;
            float enemySpeed = speed;

            
            foreach (Enemy enemy in enemies)
            {
                enemy.position.X += enemySpeed * enemy.transform.direction.X;

                if (enemy.position.X - enemy.transform.size / 2 <= 0 || enemy.position.X + enemy.transform.size / 2 >= screenWidth)
                {
                    wallHit = true;
                }
            }

            
            if (wallHit)
            {
                foreach (Enemy enemy in enemies)
                {
                    enemy.transform.direction.X *= -1.0f;
                    enemy.position.Y += 10;
                }
            }

            void UpdateEnemies()
            {
                bool wallHit = false;
                float enemySpeed = 0.5f;


                foreach (Enemy enemy in enemies)
                {
                    enemy.position.X += enemySpeed * enemy.transform.direction.X;


                    if (enemy.position.X - enemy.transform.size / 2 <= 0 || enemy.position.X + enemy.transform.size / 2 >= screenWidth)
                    {
                        wallHit = true;
                    }
                }


                if (wallHit)
                {
                    foreach (Enemy enemy in enemies)
                    {
                        enemy.transform.direction.X *= -1.0f;
                        enemy.position.Y += 10;
                    }
                }


                if (enemies.Count > 0 && moveRight && enemies.Last().position.X >= screenWidth - 20)
                {
                    moveRight = false;
                    shouldChangeDirection = true;
                    position.Y += 10;
                }

                else if (enemies.Count > 0 && !moveRight && enemies.First().position.X <= 20)
                {
                    moveRight = true;
                    shouldChangeDirection = true;
                    position.Y += 10;
                }


                if (shouldChangeDirection)
                {
                    speed *= -1;
                    shouldChangeDirection = false;
                    moveDown = true;
                    foreach (Enemy enemy in enemies)
                    {
                        enemy.transform.direction.X *= -1.0f;
                        enemy.position.Y += 10;
                    }
                }
            }
        }
    }
}
