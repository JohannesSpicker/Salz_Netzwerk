using System.Drawing;

namespace MyMultiPlayerGame.Game
{
    class Soldier : GameObject
    {
        public int HP { get; private set; }
        public float Speed { get; private set; }
        public float ViewRange { get; private set; }
        public float FireRange { get; private set; }
        public float FireFrequency { get; private set; }
        public int Damage { get; private set; }
        public int Player { get; private set; }

        public Soldier(Game game, int player)
            : base(game)
        {
            this.Player = player;
            this.HP = 10;
            this.Speed = 5;
            this.Damage = 3;
            this.FireFrequency = 0.5f;
            this.ViewRange = 200;
            this.FireRange = 100;
        }

        public override void NextSimulationStep()
        {
            if ((this.Player == 0 && this.X + this.FireRange >= this.Game.boardWidth) ||
                (this.Player == 1 && this.X - this.FireRange <= 0))
            {
                // enemy player in range!

            }
            else
            {
                // move to opponent
                if (this.Player == 0)
                    this.X += this.Speed;
                else
                    this.X -= this.Speed;
            }
        }

        public override void Render(Graphics g)
        {
            const float radius = 10;

            g.FillEllipse(this.Player == 0 ? Brushes.Red : Brushes.Yellow, this.X - radius, this.Y - radius, radius * 2, radius * 2);
        }

    }
}
