using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace MyMultiPlayerGame.Game
{
    abstract class GameObject
    {
        public GameObject(Game game)
        {
            this.Game = game;
        }

        public Game Game { get; protected set; }
        public float X { get; set; }
        public float Y { get; set; }
        public string Name { get; protected set; }

        public abstract void NextSimulationStep(Game myGame);
        public abstract void Render(Graphics g);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SquareDistance(float x1, float y1, float x2, float y2) => (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SetPosition(float newX, float newY)
        {
            X = newX;
            Y = newY;
        }

        public void Move(float xTarget, float yTarget, float moveDistance)
        {
            float sqDist = SquareDistance(X, Y, xTarget, yTarget);

            if (sqDist < moveDistance * moveDistance)//early out for short movements
            {
                SetPosition(xTarget, yTarget);
                return;
            }

            float dist = (float)Math.Sqrt(sqDist);

            float xDirection = (xTarget - X) / dist;//normalized x direction
            float yDirection = (yTarget - Y) / dist;//normalized y direction

            X += xDirection * moveDistance;
            Y += yDirection * moveDistance;
        }
    }
}
