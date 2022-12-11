using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Engine;

namespace MoRe
{
    internal class Warrior : Player
    {
        public Warrior(Vector2 location, float scale) : base(location, scale, "Player/Warrior/WarriorPlayer")
        {
            currentClass = characterType.warrior;

            this.MaxHealth = 125;
            this.Health = MaxHealth;
            this.PowerMultiplier = 1.5f;
            this.BaseSpeed = 2;
            this.AttackSpeed = 10;

            specialAbilityCooldown = 30 * 1000;
            specialAbilityDuration = 5 * 1000;
        }

        public override void SpecialAbility()
        {
            CanTakeDamage = false;
            base.SpecialAbility();
        }

        protected override void ResetSpecialAbility()
        {
            CanTakeDamage = true;
            base.ResetSpecialAbility();
        }
    }
}
