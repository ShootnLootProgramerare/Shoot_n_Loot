﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shoot__n_Loot.Base_Classes;
using Shoot__n_Loot.InvenoryStuff;
using Shoot__n_Loot.Scenes;
using Shoot__n_Loot.UI;
using Shoot__n_Loot.WeaponClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Shoot__n_Loot
{
    class Player : ObjectWithInventory
    {
        private const float accelerationMult = .7f, friction = .87f;

        public override Rectangle MapCollider { get { return new Rectangle(base.MapCollider.X + (int)(base.MapCollider.Width * .375f), base.MapCollider.Y + (int)(base.MapCollider.Height * .75f), (int)(base.MapCollider.Width / 4), (int)(base.MapCollider.Height * .25f)); } }

        //public Inventory Inventory { get; set; }

        public float Hunger { get; set; }

        public Weapon weapon;

        public float bleeding; //how much hp is removed each second
        public bool UsingMelee { get { return MeleeWeapon != null; } }

        public MeleeWeaponProperties MeleeWeapon { get; set; }
        int meleeAttackTimer;

        Item draggedItem;

        //HPBar hpBar;

        Bar healthBar, hungerBar;

        int chunkX, chunkY, tileX, tileY;

        int playSound = 0;
        int playWaterSound = 0;

        private Point CUSTOMIZINGINVENTORYOFFSET { get { return new Point(0, 100); } }

        public Player()
        {
            Vector2 position = new Vector2(112, 10) * Tile.size; //should be defined in the item map?
            Sprite = new Sprite(TextureManager.playerWalkNoWeapon[0], position, new Vector2(100), 4, new Point(100, 100), 0);
            inventory = new Inventory(this, new Point(0, 0), 5, 4, 5);
            weapon = new Weapon();
            this.MaxHealth = 100;
            CanDie = true;
            //hpBar = new HPBar(new Vector2(Camera.TotalOffset.X, Camera.TotalOffset.Y), 100);

            healthBar = new Bar(TextureManager.healthBar, TextureManager.fuelCan, new Rectangle(0, 0, 150, 75));
            hungerBar = new Bar(TextureManager.hungerBar, TextureManager.rifleBarrel, new Rectangle(0, 75, 150, 75));

            for (int i = 0; i < 10; i++)
            {
                inventory.Add(Items.RandomItem(this.Position));
            } 
                
        }

        void JohansSkitAkaAudioStuff()
        {
            tileX = (int)Center.X / Tile.size;
            chunkX = tileX / Chunk.size;
            tileX %= Chunk.size;

            tileY = (int)Center.Y / Tile.size;
            chunkY = tileY / Chunk.size;
            tileY %= Chunk.size;

            Tile t = Map.chunks[chunkX, chunkY].Tiles[tileX, tileY];

            playSound++;

            try
            {
                if (Map.chunks[chunkX, chunkY].Tiles[tileX + 1, tileY].Properties.TextureIndex == 1 ||
                    Map.chunks[chunkX, chunkY].Tiles[tileX - 1, tileY].Properties.TextureIndex == 1 ||
                    Map.chunks[chunkX, chunkY].Tiles[tileX, tileY + 1].Properties.TextureIndex == 1 ||
                    Map.chunks[chunkX, chunkY].Tiles[tileX, tileY - 1].Properties.TextureIndex == 1 ||
                    Map.chunks[chunkX, chunkY].Tiles[tileX + 1, tileY + 1].Properties.TextureIndex == 1 ||
                    Map.chunks[chunkX, chunkY].Tiles[tileX - 1, tileY + 1].Properties.TextureIndex == 1 ||
                    Map.chunks[chunkX, chunkY].Tiles[tileX + 1, tileY - 1].Properties.TextureIndex == 1 ||
                    Map.chunks[chunkX, chunkY].Tiles[tileX - 1, tileY - 1].Properties.TextureIndex == 1)
                {
                    playWaterSound++;
                    if (playWaterSound >= 16) { SoundManager.water.Play(0.4f, 0f, 0f); playWaterSound = 0; }
                }
            }
            catch
            {

            }

            if (playSound >= 32 && (Input.newKs.IsKeyDown(Keys.A) || Input.newKs.IsKeyDown(Keys.D) || Input.newKs.IsKeyDown(Keys.W) || Input.newKs.IsKeyDown(Keys.S)))
            {
                if (t.Properties.TextureIndex == 0) { SoundManager.playerWalkGrass.Play(); } // Grass sound
                if (t.Properties.TextureIndex == 3) { SoundManager.playerWalkFloor.Play(0.8f, 0f, 0f); } // Wall sound
                if (t.Properties.TextureIndex == 4) { SoundManager.playerWalkDirt.Play(); } // Dirt sound
                if (t.Properties.TextureIndex == 5) { SoundManager.playerWalkPath.Play(); } // Path sound
                if (t.Properties.TextureIndex == 6) { SoundManager.playerWalkBeach.Play(0.5f, 0f, 0f); } // Beach sound
                if (t.Properties.TextureIndex == 7) { SoundManager.playerWalkBridge.Play(); } // Bridge sound
                playSound = 0;
            }
        }

        public override void Update()
        {
            Hunger += .001f;
            Health -= bleeding;
            bleeding *= (bleeding > .01f) ? .95f : .9999f;

            JohansSkitAkaAudioStuff(); // :^)))) 

            if (!inventoryVisible)
            {
                if (!UsingMelee) Shoot();
                else MeleeWeaponUpdate();

                weapon.ShootingUpdate(inventory);
                Animate();
            }
            else if (inventoryVisible)
            {
                inventory.Update(new Point(0, 0));
                weapon.CustomizingUpdate();
                InventoryUpdate();
            }
            else
            {
            }

            Move();

            if (Input.KeyWasJustPressed(Keys.E))
            {
                SoundManager.inventory.Play();
                inventoryVisible = !inventoryVisible;
                inventory.HideAllItemMenus();
            }
        }

        void InventoryUpdate()
        {
            if (Input.newMs.RightButton == ButtonState.Pressed)
            {
                if (draggedItem == null)
                {
                    //get item if clicked slot
                    ItemSlot i = Inventory.SlotAtMousePos();
                    if (i != null && i.Item != null)
                    {
                        draggedItem = new Item(i.Item.Properties, Input.MousePosition);
                        i.Remove(1);
                        draggedItem.Position = Input.MousePosition;
                        Debug.WriteLine("an item was clicked");
                    }
                }
                else
                {
                    //track mouse with it
                    draggedItem.Position -= Input.DeltaPos - Velocity; // should have used a separate spritebatch for ui :((
                }
            }
            else
            {
                if (draggedItem != null)
                {
                    Debug.WriteLine("an item was released");
                    //put in slot under mouse or if none exists put back in standard slot
                    ItemSlot i = Inventory.SlotAtMousePos();
                    Debug.WriteLine("i == null = " + (i == null));
                    if (i != null)
                    {
                        Debug.WriteLine("i.Item == null = " + (i.Item == null) + "\nStackSize = " + i.StackSize);
                        if (i.CanContain(draggedItem)) i.Add(draggedItem);
                        else
                        {
                            Debug.WriteLine("this slot didnt fit the item");
                            inventory.Add(draggedItem);
                        }
                    }
                    else
                    {
                        draggedItem.Position = Position;
                        SceneManager.CurrentScene.AddObject(draggedItem);
                    } 
                    draggedItem = null;
                }
            }
        }

        void MeleeWeaponUpdate()
        {
            if (meleeAttackTimer > 0)
            {
                meleeAttackTimer--;
            }
            else if (Input.newMs.LeftButton == ButtonState.Pressed && meleeAttackTimer == 0)
            {
                Sprite.SetTexture(TextureManager.playerAttack[(int)VelDirection], 4, new Point(100, 100));
                Sprite.AnimationSpeed = .2f;
                meleeAttackTimer = -1;
            }
            else
            {
                if (Sprite.EndOfAnim)
                {
                    meleeAttackTimer = MeleeWeapon.AttackSpeed;
                    SetRegularSprite();
                    const float WEAPON_RANGE = 50;
                    foreach (GameObject g in SceneManager.gameScene.objects.Where(e => e.Type == "Enemy").Where(e => e.DistanceSquared(SceneManager.gameScene.player.Center) < WEAPON_RANGE * WEAPON_RANGE))
                    {
                        Debug.WriteLine("enemy took damage, hp = " + g.Health + ", dead = " + g.Dead);
                        g.Health -= MeleeWeapon.Damage;
                    }
                }
            }
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
            if (Velocity.LengthSquared() > 1f)
            {
                /*
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
                }*/
                Sprite.AnimationSpeed = 9f / 60;
                SetRegularSprite();
            }
            else
            {
                if (meleeAttackTimer != -1)
                {
                    Sprite.AnimationSpeed = 0;
                    Sprite.Frame = 0;
                }
            }
        }

        void SetRegularSprite()
        {
            if (UsingMelee) Sprite.SetTexture(TextureManager.playerWalkMelee[(int)VelDirection], 4, new Point(100, 100));
            else if (weapon.Parts == 0) Sprite.SetTexture(TextureManager.playerWalkNoWeapon[(int)VelDirection], 4, new Point(100, 100));
            else Sprite.SetTexture(TextureManager.playerWalkGun[(int)VelDirection], 4, new Point(100, 100));
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
            if (Input.KeyWasJustPressed(Keys.R)) weapon.StartReload(inventory);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (inventoryVisible)
            {
                inventory.Draw(spriteBatch);
                //weapon.Draw(spriteBatch);
                weapon.DrawCustomization(spriteBatch);

                if (draggedItem != null)
                {
                    draggedItem.Sprite.LayerDepth = 0;
                    draggedItem.Draw(spriteBatch);
                }
            }

            spriteBatch.DrawString(TextureManager.font, "Ammo: " + weapon.Ammo.ToString() + "\nHP: " + Health + "\nHunger: " + Hunger.ToString("0") + "\nBleeding: " + bleeding, Camera.Position + Camera.Origin * new Vector2(-1, 1) * .8f - TextureManager.font.MeasureString("Ammo: " + weapon.Ammo.ToString()), Color.Black);

            //hpBar.Draw(spriteBatch, base.Health / base.MaxHealth);

            healthBar.Draw(spriteBatch, Health / MaxHealth);
            hungerBar.Draw(spriteBatch, Hunger / 25f);

            base.Draw(spriteBatch);
        }
    }
}
