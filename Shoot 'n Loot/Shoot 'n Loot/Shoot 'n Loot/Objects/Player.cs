using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shoot__n_Loot.Scenes;
using Shoot__n_Loot.WeaponClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class Player : GameObject
    {
        private const float accelerationMult = .7f, friction = .87f;

        public override Rectangle MapCollider { get { return new Rectangle(base.MapCollider.X + (int)(base.MapCollider.Width * .375f), base.MapCollider.Y + (int)(base.MapCollider.Height * .75f), (int)(base.MapCollider.Width / 4), (int)(base.MapCollider.Height * .25f)); } }

        public Inventory Inventory { get; set; }

        public float Hunger { get; set; }

        public Weapon weapon;

        public float bleeding; //how much hp is removed each second
        bool inventoryVisible;
        bool customizing;

        private Point CUSTOMIZINGINVENTORYOFFSET { get { return new Point(0, 100); } }

        public Player()
        {
            Sprite = new Sprite(TextureManager.playerRight, new Vector2(500), new Vector2(100), 4, new Point(100, 100), 0);
            Inventory = new Inventory(this, new Point(0, 0), 10, 4, 10);
            weapon = new Weapon();
            this.MaxHealth = 100;
            CanDie = true;

            for (int i = 0; i < 10; i++)
            {
                Inventory.Add(Items.RandomItem(this.Position));
            } 
                
        }

        public override void Update()
        {
            Hunger += .001f;
            Health -= bleeding;

            if (!inventoryVisible && !customizing)
            {
                Shoot();
                weapon.ShootingUpdate(Inventory);
                Animate();
            }
            else if (inventoryVisible)
            {
                Inventory.Update(new Point(0, 0));
            }
            else if (customizing)
            {
                weapon.CustomizingUpdate(Inventory, CUSTOMIZINGINVENTORYOFFSET);
                Sprite.Frame = 0;
                Sprite.AnimationSpeed = 0;
            }
            else
            {
            }

            Move();

            if (Input.KeyWasJustPressed(Keys.I) && !customizing)
            {
                inventoryVisible = !inventoryVisible;
                Inventory.HideAllItemMenus();
            }
            //if (Input.KeyWasJustPressed(Keys.U) && !inventoryVisible) customizing = !customizing;
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

            Hunger += Velocity.Length() * .0001f;
        }

        void Animate()
        {
            if (Velocity.LengthSquared() > .3f)
            {
                Sprite.AnimationSpeed = 9f / 60;
                if (Math.Abs(Velocity.X) > Math.Abs(Velocity.Y))
                {
                    //left and right movement
                    if (Velocity.X > 0) Sprite.SetTexture(TextureManager.playerRight, 4, new Point(100, 100));
                    else if (Velocity.X < 0) Sprite.SetTexture(TextureManager.playerLeft, 4, new Point(100, 100));
                }
                else
                {
                    if (Velocity.Y > 0) Sprite.SetTexture(TextureManager.playerDown, 4, new Point(100, 100));
                    else if (Velocity.Y < 0) Sprite.SetTexture(TextureManager.playerUp, 4, new Point(100, 100));
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
            if (Input.newMs.LeftButton == ButtonState.Pressed && (Input.oldMs.LeftButton == ButtonState.Released || weapon.IsAuto))
            {
                //check ammo etc
                if (true)
                {
                    Vector2 offset = new Vector2(0, -30);
                    Vector2 v = Input.MousePosition - Center - offset;
                    weapon.TryShoot(Center + offset, (float)Math.Atan2(v.Y, v.X), SceneManager.gameScene);
                }
            }
            if (Input.KeyWasJustPressed(Keys.R)) weapon.StartReload(Inventory);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (inventoryVisible) Inventory.Draw(spriteBatch);

            if (customizing)
            {
                weapon.DrawCustomization(spriteBatch);
                Inventory.Draw(spriteBatch);
            }

            spriteBatch.DrawString(TextureManager.font, "Ammo: " + weapon.Ammo.ToString() + "\nHP: " + Health + "\nHunger: " + Hunger.ToString("0") + "\nBleeding: " + bleeding, Camera.Position + Camera.Origin * new Vector2(-1, 1) * .8f - TextureManager.font.MeasureString("Ammo: " + weapon.Ammo.ToString()), Color.Black);

            base.Draw(spriteBatch);
        }
    }
}
