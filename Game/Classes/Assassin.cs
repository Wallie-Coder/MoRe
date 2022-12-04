using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Engine;


namespace MoRe
{
    internal class Assassin : Player
    {
        public Assassin(Vector2 location, float scale) : base(location, scale, "Player\\Assassin\\AssassinPlayer")
        {
            currentClass = characterType.assassin;

            this.MaxHealth = 50;
            this.Health = MaxHealth;
            this.PowerMultiplier = 2;
            this.MoveSpeed = 4;
            this.AttackSpeed = 20;

            specialAbilityCooldown = 20 * 1000;
            specialAbilityDuration = 10 * 1000;
        }
        public override void SpecialAbility()
        {
            PowerMultiplier *= 2;
            base.SpecialAbility();
        }
        protected override void ResetSpecialAbility()
        {
            PowerMultiplier /= 2;
            base.ResetSpecialAbility();
        }
    }
}
