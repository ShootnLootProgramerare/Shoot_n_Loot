using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class Player : GameObject
    {
        private const float accelerationMult = .7f, friction = .87f;

        public override Rectangle Hitbox { get { return new Rectangle(base.Hitbox.X + base.Hitbox.Width / 4, base.Hitbox.Y + (int)(base.Hitbox.Height * .75f), base.Hitbox.Width / 2, (int)(base.Hitbox.Height * .25f)); } }

        public Inventory Inventory { get; set; }

        Weapon weapon;
        bool inventoryVisible;

        public Player()
        {
            Sprite = new Sprite(TextureManager.playerRight, new Vector2(500), new Vector2(100), 4, new Point(50, 50), 0);
            Inventory = new Inventory(10, 4, 10);
            Inventory.Add(new Item(3, 2, 1, new Sprite(TextureManager.enemy1, Vector2.Zero, new Vector2(10))));
            weapon = new Weapon();
            weapon.AddPart(new WeaponPart(WeaponPart.PartType.Mag, 1, 1, 10, false, 1, 1, new Weapon.AmmoType[] { Weapon.AmmoType.Medium }));
        }

        public override void Update()
        {
            Move();
            Animate();
            Shoot();
            UpdateInventory();
            weapon.Update();
        }

        void Move()
        {
            Vector2 acceleration = Vector2.Zero;
            if (Input.newKs.IsKeyDown(Keys.A)) acceleration.X -= 1;
            if (Input.newKs.IsKeyDown(Keys.D)) acceleration.X += 1;
            if (Input.newKs.IsKeyDown(Keys.W)) acceleration.Y -= 1;
            if (Input.newKs.IsKeyDown(Keys.S)) acceleration.Y += 1;
            if(acceleration != Vector2.Zero) acceleration.Normalize();
            Velocity += acceleration * accelerationMult;
            Velocity *= friction;
            if (Math.Abs(Velocity.X) < .05) Velocity = new Vector2(0, Velocity.Y);
            if (Math.Abs(Velocity.Y) < .05) Velocity = new Vector2(Velocity.X, 0);

            Move(true);
        }

        void Animate()
        {
            if (Velocity.LengthSquared() > .3f)
            {
                Sprite.AnimationSpeed = 9f / 60;
                if (Math.Abs(Velocity.X) > Math.Abs(Velocity.Y))
                {
                    //left and right movement
                    if (Velocity.X > 0) Sprite.SetTexture(TextureManager.playerRight, 4, new Point(50, 50));
                    else if (Velocity.X < 0) Sprite.SetTexture(TextureManager.playerLeft, 4, new Point(50, 50));
                }
                else
                {
                    if (Velocity.Y > 0) Sprite.SetTexture(TextureManager.playerDown, 4, new Point(50, 50));
                    else if (Velocity.Y < 0) Sprite.SetTexture(TextureManager.playerUp, 4, new Point(50, 50));
                }
            }
            else
            {
                Sprite.AnimationSpeed = 0;
                Sprite.Frame = 0;
            }
        }

        void Shoot()
        {
            if (Input.LeftClickWasJustPressed())
            {
                //check ammo etc
                if (true)
                {
                    Vector2 v = Input.MousePosition - Center;
                    weapon.TryShoot(Center, (float)Math.Atan2(v.Y, v.X), Game1.gameScene);
                }
            }
            if (Input.KeyWasJustPressed(Keys.R)) weapon.StartReload(Inventory);
        }

        void UpdateInventory()
        {
            if (Input.KeyWasJustPressed(Keys.I)) inventoryVisible = !inventoryVisible;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (inventoryVisible) Inventory.Draw(spriteBatch, new Point(Game1.ScreenSize.X / 2, Game1.ScreenSize.Y / 2));

            spriteBatch.DrawString(TextureManager.font, "Ammo: " + weapon.Ammo.ToString(), Camera.Position + Camera.Origin * new Vector2(-1, 1) * .8f - TextureManager.font.MeasureString("Ammo: " + weapon.Ammo.ToString()), Color.Black);

            base.Draw(spriteBatch);
        }
    }
}
