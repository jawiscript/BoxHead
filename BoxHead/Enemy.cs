﻿class Enemy : MovableSprite
{
    public double Life { get; set; }
    public double Damage { get; set; }
    public bool IsAlive { get; set; }
    protected Image enemy;

    public Enemy(double life, double damage)
    {
        Life = life;
        Damage = damage;
        IsAlive = true;
        Speed = 1;
        enemy = new Image("", 60, 60);
    }

    public void GoToPlayer(Character character)
    {
        short xDiff = (short)(character.X - this.X);
        short YDiff = (short)(character.Y - this.Y);
        if (xDiff < 0 && YDiff < 0)
        { 
            X -= Speed;
            Y -= Speed;
        }
        else if (xDiff < 0 && YDiff == 0)
        {
            X -= Speed;
        }
        else if (xDiff < 0 && YDiff > 0)
        {
            X -= Speed;
            Y += Speed;
        }
        else if (xDiff > 0 && YDiff < 0)
        {
            X += Speed;
            Y -= Speed;
        }
        else if (xDiff > 0 && YDiff == 0)
        {
            X += Speed;
        }
        else if (xDiff > 0 && YDiff > 0)
        {
            X += Speed;
            Y += Speed;
        }
        else if (xDiff == 0 && YDiff < 0)
        {
            Y -= Speed;
        }
        else
        {
            Y += Speed;
        }
    }

    public void Attack(Character character)
    {
        if (CollidesWith(character))
            character.Life -= Damage;
    }
}
