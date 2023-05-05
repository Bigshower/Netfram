using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace Spaceinvaders
{
    public class Transform
    {
        public Vector2 position;
        public Vector2 direction;
        public float speed;
        public float size;

        public Transform(Vector2 position, Vector2 direction, float speed, float size)
        {
            this.position = position;
            this.direction = direction;
            this.speed = speed;
            this.size = size;
        }
    }
}


namespace Spaceinvaders
{
    internal class Bullet
    {
        public Vector2 position;
        public Vector2 velocity;

        public Bullet(Vector2 position, Vector2 velocity)
        {
            this.position = position;
            this.velocity = velocity / 0.5f;
        }

        public void Update()
        {
            position += velocity;
        }

        public void Draw()
        {
            Raylib.DrawCircle((int)position.X, (int)position.Y, 5, Raylib_cs.Color.RED);
        }
        
            }
        }
