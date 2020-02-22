using System;
using System.Drawing;

namespace MyMultiPlayerGame.Game
{
    class Soldier : GameObject
    {
        public enum SoldierType { AchyArcher, BlindBertha, CrudeCommoner }

        public SoldierType Type { get; private set; }
        public int HP { get; private set; }
        public float Speed { get; private set; }
        public float ViewRange { get; private set; }
        public float FireRange { get; private set; }
        public float FireFrequency { get; private set; }
        public int Damage { get; private set; }
        public int Player { get; private set; }
        public int EnemyPlayer { get => Player + 1 % 2; }//holds true for two players
        public float FireTicker { get; private set; }
        private bool ShotReady { get => FireTicker < 0f; }

        public Soldier(Game game, int player, SoldierType type)
            : base(game)
        {
            switch (type)
            {
                case SoldierType.AchyArcher:
                    this.Type = type;
                    this.Player = player;
                    this.HP = 5;
                    this.Speed = 4;
                    this.Damage = 3;
                    this.FireFrequency = 0.75f;
                    this.ViewRange = 400;
                    this.FireRange = 200;
                    this.FireTicker = this.FireFrequency;
                    break;
                case SoldierType.BlindBertha:
                    this.Type = type;
                    this.Player = player;
                    this.HP = 30;
                    this.Speed = 2;
                    this.Damage = 5;
                    this.FireFrequency = 1.5f;
                    this.ViewRange = 100;
                    this.FireRange = 50;
                    this.FireTicker = this.FireFrequency;
                    break;
                case SoldierType.CrudeCommoner:
                    this.Type = type;
                    this.Player = player;
                    this.HP = 10;
                    this.Speed = 5;
                    this.Damage = 3;
                    this.FireFrequency = 0.5f;
                    this.ViewRange = 200;
                    this.FireRange = 100;
                    this.FireTicker = this.FireFrequency;
                    break;
                default:
                    this.Type = type;
                    this.Player = player;
                    this.HP = 10;
                    this.Speed = 5;
                    this.Damage = 3;
                    this.FireFrequency = 0.5f;
                    this.ViewRange = 200;
                    this.FireRange = 100;
                    this.FireTicker = this.FireFrequency;
                    break;
            }
        }

        public override void NextSimulationStep(Game myGame)
        {
            FireTickTock();

            if ((this.Player == 0 && this.X + this.FireRange >= this.Game.boardWidth) ||
                (this.Player == 1 && this.X - this.FireRange <= 0))//enemy player in range
            {
                if (ShotReady)
                {
                    //shoot enemy player
                    if (this.Player == myGame.myPlayerNumber)
                        myGame.oppHealth -= this.Damage;
                    else
                        myGame.myHealth -= this.Damage;

                    ResetFireTicker();
                }

                return;
            }

            Tuple<Soldier, bool> target = Game.FindBestTarget(this);

            if (target.Item2)//target is in attack range
            {
                if (ShotReady)
                {
                    //shoot target
                    target.Item1.TakeDamage(Damage);
                    ResetFireTicker();
                }
            }
            else if (target.Item1 != this)//target is in view range
            {
                //go to target
                Move(target.Item1.X, target.Item1.Y, Speed);
            }
            else
            {
                //go to enemy player
                if (this.Player == 0)
                    this.X += this.Speed;
                else
                    this.X -= this.Speed;
            }
        }

        public override void Render(Graphics g)
        {
            const float radius = 10;

            switch (Type)
            {
                case SoldierType.AchyArcher:
                    g.FillPie(this.Player == 0 ? Brushes.Red : Brushes.Yellow, this.X - radius, this.Y - radius, radius * 2, radius * 2, 160, 180);
                    break;
                case SoldierType.BlindBertha:
                    g.FillRectangle(this.Player == 0 ? Brushes.Red : Brushes.Yellow, this.X - radius, this.Y - radius, radius * 2, radius * 2);
                    break;
                case SoldierType.CrudeCommoner:
                    g.FillEllipse(this.Player == 0 ? Brushes.Red : Brushes.Yellow, this.X - radius, this.Y - radius, radius * 2, radius * 2);
                    break;
                default:
                    g.FillEllipse(this.Player == 0 ? Brushes.Red : Brushes.Yellow, this.X - radius, this.Y - radius, radius * 2, radius * 2);
                    break;
            }
        }

        private void FireTickTock() => FireTicker -= 0.1f;
        private void ResetFireTicker() => FireTicker = FireFrequency;

        public void TakeDamage(int dmg)
        {
            HP -= dmg;

            if (HP <= 0)
                Die();
        }

        private void Die() => Game.PutIntoGarbage(this);
    }
}
