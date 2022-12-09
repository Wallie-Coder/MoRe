using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MoRe;

namespace Engine
{
    // item class, this item drops on the floor an can be picked up by the player.
    class Item : InAnimate
    {
        public enum itemTypes
        {
            passive,
            active,
            orbital,
            test
        }

        public string name { get; protected set; }
        //public int Damage { get; protected set; }
        public float MoveSpeed { get; protected set; }
        public bool Dash { get; protected set; }
        public bool shield { get; protected set; }
        public int Bounce { get; protected set; }
        public int Pierce { get; protected set; }

        public Item(Vector2 location, float scale, string assetName) : base(location, scale, "Item\\" + assetName)
        {

        }
    }

    class DamageUp : Item
    {
        public DamageUp(Vector2 location, float scale, string assetName = "DamageUp") : base(location, scale, assetName)
        {
            Health = 0;
            Damage = 2;
            MoveSpeed = 0;
            MaxHealth = 0;
            Dash = false;
            name = "damage up";
        }
    }

    class HealthUp : Item
    {
        public HealthUp(Vector2 location, float scale , string assetName = "HealthUp") : base(location, scale, assetName)
        {
            Health = 10;
            Damage = 0;
            MoveSpeed = 0;
            MaxHealth = 1;
            Dash = false;
            shield = false;
            name = "damage up";
        }
    }

    class DashRefill : Item
    {
        public DashRefill(Vector2 location, float scale, string assetName = "Dash") : base(location, scale, assetName)
        {
            Health = 0;
            Damage = 0;
            MoveSpeed = 0;
            MaxHealth = 0;
            Dash = true;
            shield = false;
            name = "damage up";
        }
    }
    class ShieldUp : Item
    {
        public ShieldUp(Vector2 location, float scale, string assetName = "ShieldUp") : base(location, scale, assetName)
        {
            Health = 0;
            Damage = 0;
            MoveSpeed = 0;
            MaxHealth = 0;
            Dash = false;
            shield = true;
            name = "damage up";
        }
    }
    class BounceUp : Item
    {
        public BounceUp(Vector2 location) : base(location, 1, "BounceUp")
        {
            Bounce = 1;
        }
    }
    class PierceUp : Item
    {
        public PierceUp(Vector2 location) : base(location, 1, "PierceUp")
        {
            Pierce = 1;
        }
    }
}
