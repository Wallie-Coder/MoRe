using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Engine;

namespace MoRe
{
    internal class Healer : Player
    {
        public Healer(Vector2 location, float scale) : base(location, scale, "Player/Healer/HealerPlayer")
        {
            currentClass = characterType.healer;

            this.MaxHealth = 100;
            this.Health = MaxHealth;
            this.PowerMultiplier = 1;
            this.BaseSpeed = 3;
            this.AttackSpeed = 10;

            specialAbilityCooldown = 30 * 1000;
            specialAbilityDuration = 1 * 1000;
        }

        public override void SpecialAbility()
        {
            Heal(MaxHealth * 0.25f);
            base.SpecialAbility();
        }

        protected override void ResetSpecialAbility()
        {
            base.ResetSpecialAbility();
        }
    }
}
