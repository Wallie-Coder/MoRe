using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MoRe;

namespace Engine
{
    internal class Player : Animated
    {
        protected enum characterType
        {
            assassin, healer, warrior
        }
        protected characterType currentClass;

        //All variables for weapons
        Weapon weapon1;
        Weapon weapon2;
        internal Weapon[] weaponList = new Weapon[2];
        Weapon currentWeapon;
        float weaponSwapCooldown;
        bool canSwapWeapon;

        //All variables for the normal ability
        float normalAbilityCooldownTimer;
        float normalAbilityCooldown;
        bool canNormalAbility;

        //All variables for the special ability
        float specialAbilityCooldownTimer;
        protected float specialAbilityCooldown;
        protected float specialAbilityDuration;
        float specialAbilityTimer;
        bool canSpecialAbility;
        bool specialAbilityActive;

        //Lists for collected Items and orbitals
        public List<Item> items;
        public List<Orbital> orbitals;

        public float SpecialAbilityCooldownTimer { get { return specialAbilityCooldownTimer; } protected set { specialAbilityCooldownTimer = value; } }
        public float NormalAbilityCooldownTimer { get { return normalAbilityCooldownTimer; } protected set { normalAbilityCooldownTimer = value; } }

        internal Player(Vector2 location, float scale, string assetName = "Player") : base(location, scale, assetName)
        {
            weapon2 = new Pistol(location, 1f);
            weapon1 = new Sniper(location, 1f);
            weaponList[0] = weapon1;
            weaponList[1] = weapon2;
            currentWeapon = weaponList[0];

            items = new List<Item>();
            orbitals = new List<Orbital>();

            //item = new damageUp(new Vector2(100, 100), 32, 32, "damageUpSprite");
            //item2 = new healthUp(new Vector2(200, 100), 32, 32, "damageUpSprite");

            orbitals.Add(new Orbital(location, 1, 1, 100, 10, "Projectiles\\BlueProjectile"));

            //initializes the normal ability. (The 10 stands for 10 seconds, the 1000 converts from seconds to milliseconds).
            normalAbilityCooldown = 10 * 1000;
            //normalAbilityCooldownTimer = normalAbilityCooldown;
            canNormalAbility = true;

            weaponSwapCooldown = 100;

            canSpecialAbility = true;

            this.Damage = 0;
            PowerMultiplier = 1;
            Health = 200;
            MaxHealth = Health;
            baseSpeed = 5;
            orientation = EntityOrientation.Down;

            this.Depth = 1f;

            this.StartMoving();
        }

        internal override void Update(GameTime gameTime)
        {
            currentWeapon.Update(gameTime);

            //Update the cooldown timers
            Cooldowns(gameTime);

            foreach (Orbital o in orbitals)
                o.updatePosition(location);

            base.Update(gameTime);

            foreach (Weapon w in weaponList)
                w.updatePosition(location + new Vector2(5f, 7.5f) * ObjectScale);

            //Update the inputs
            InputManager(gameTime);
        }

        internal override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);

            foreach (Orbital orbital in orbitals)
                orbital.Draw(batch);

            currentWeapon.Draw(batch);
        }

        //Changes the player's stats when picking up an item
        public void ChangeStats(Item item)
        {
            this.Health += item.Health;
            this.PowerMultiplier += item.Damage;
            this.MoveSpeed += item.MoveSpeed;
            this.MaxHealth += item.MaxHealth;
            if (item.Dash)
            {
                normalAbilityCooldownTimer = 0;
                canNormalAbility = true;
                return;
            }
            if (item.shield)
            {
                Orbital o = new Orbital(location, 1, 1, (orbitals.Count + 1) * 100, 10, "Projectiles\\BlueProjectile");
                if (orbitals.Count != 0)
                    o.degree = orbitals[0].degree;
                orbitals.Add(o);
            }
            items.Add(item);
        }

        //Swaps the characters weapons
        void SwapWeapon()
        {
            canSwapWeapon = false;
            Weapon tempWeapon;
            tempWeapon = weaponList[0];
            weaponList[0] = weaponList[1];
            weaponList[1] = tempWeapon;
            currentWeapon = weaponList[0];
            weaponSwapCooldown = 1000;
        }

        //Dash function for every character
        public virtual void NormalAbility()
        {
            location += Direction * 100;
            canNormalAbility = false;
            normalAbilityCooldownTimer = normalAbilityCooldown;
        }

        //Virtual special ability for each individual character to be overridden
        public virtual void SpecialAbility()
        {
            specialAbilityActive = true;
            canSpecialAbility = false;
            specialAbilityCooldownTimer = specialAbilityCooldown;
            specialAbilityTimer = specialAbilityDuration;
        }

        //Updates the cooldown timers and handles the special ability duration
        private void Cooldowns(GameTime gameTime)
        {

            if (!canNormalAbility)
            {
                normalAbilityCooldownTimer -= gameTime.ElapsedGameTime.Milliseconds;
                if (normalAbilityCooldownTimer <= 0)
                {
                    canNormalAbility = true;
                }
            }

            if (specialAbilityActive)
            {
                specialAbilityTimer -= gameTime.ElapsedGameTime.Milliseconds;
                if (specialAbilityTimer <= 0)
                {
                    ResetSpecialAbility();
                }
            }

            if (!specialAbilityActive && !canSpecialAbility)
            {
                specialAbilityCooldownTimer -= gameTime.ElapsedGameTime.Milliseconds;
                if (specialAbilityCooldownTimer <= 0)
                    canSpecialAbility = true;
            }

            if (!canSwapWeapon)
            {
                weaponSwapCooldown -= gameTime.ElapsedGameTime.Milliseconds;
                if (weaponSwapCooldown <= 0)
                {
                    canSwapWeapon = true;
                }
            }
        }

        //Resets the special abilities
        protected virtual void ResetSpecialAbility()
        {
            specialAbilityTimer = 0;
            specialAbilityCooldownTimer = specialAbilityCooldown;
            specialAbilityActive = false;
        }

        internal void setLocation(Vector2 location)
        {
            this.location = location;
            orientation = EntityOrientation.Down;
        }

        //Helps with the inputs
        protected void InputManager(GameTime gameTime)
        {
            if (InputHelper.IsKeyDown(Keys.Space))
            {
                currentWeapon.Shoot(gameTime, PowerMultiplier);
            }

            if (InputHelper.IsKeyDown(Keys.A))
            {
                Direction = new Vector2(-1, 0);
                if (MoveSpeed == 0)
                    StartMoving();
                orientation = EntityOrientation.Left;
            }
            if (InputHelper.IsKeyDown(Keys.D))
            {
                Direction = new Vector2(1, 0);
                if (MoveSpeed == 0)
                    StartMoving();
                orientation = EntityOrientation.Right;
            }
            if (InputHelper.IsKeyDown(Keys.W))
            {
                Direction = new Vector2(0, -1);
                if (MoveSpeed == 0)
                    StartMoving();
                orientation = EntityOrientation.Up;
            }
            if (InputHelper.IsKeyDown(Keys.S))
            {
                Direction = new Vector2(0, 1);
                if (MoveSpeed == 0)
                    StartMoving();
                orientation = EntityOrientation.Down;
            }
            if (InputHelper.IsKeyDown(Keys.A) && InputHelper.IsKeyDown(Keys.S))
            {
                Direction = new Vector2(-1, 1);
                if (MoveSpeed == 0)
                    StartMoving();
            }
            if (InputHelper.IsKeyDown(Keys.W) && InputHelper.IsKeyDown(Keys.A))
            {
                Direction = new Vector2(-1, -1);
                if (MoveSpeed == 0)
                    StartMoving();
            }
            if (InputHelper.IsKeyDown(Keys.S) && InputHelper.IsKeyDown(Keys.D))
            {
                Direction = new Vector2(1, 1);
                if (MoveSpeed == 0)
                    StartMoving();
            }
            if (InputHelper.IsKeyDown(Keys.D) && InputHelper.IsKeyDown(Keys.W))
            {
                Direction = new Vector2(1, -1);
                if (MoveSpeed == 0)
                    StartMoving();
            }
            if (!InputHelper.IsKeyDown(Keys.A) && !InputHelper.IsKeyDown(Keys.W) && !InputHelper.IsKeyDown(Keys.S) && !InputHelper.IsKeyDown(Keys.D))
                StopMoving();
            if (InputHelper.IsKeyJustPressed(Keys.E))
                if (canNormalAbility)
                    NormalAbility();
            if (InputHelper.IsKeyJustPressed(Keys.F))
                if (canSpecialAbility)
                    SpecialAbility();

            if (InputHelper.IsKeyJustPressed(Keys.Q))
            {
                if (canSwapWeapon)
                    SwapWeapon();
            }

        }        
    }
}
