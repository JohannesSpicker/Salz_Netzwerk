using System.Drawing;

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

        public abstract void NextSimulationStep();
        public abstract void Render(Graphics g);
    }
}
