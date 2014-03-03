using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace FinalProject
{
    class Player : MobileEntity
    {
        private PlayerIndex playerNum;

        private enum PlayerState { PLAYER_ATTACK_UP, PLAYER_ATTACK_DOWN, PLAYER_ATTACK_LEFT, PLAYER_ATTACK_RIGHT, PLAYER_OTHER };

        private PlayerState curState;

        private AttackClose testAttack;

        private Vector2 playerCenter;

        public Vector2 PlayerCenter
        {
            get
            {
                return playerCenter;
            }
        }

        public PlayerIndex PlayerNum        //return player number for gamepad state checks, look into whether this is necessary if we're getting gamepad state in this object
        {
            get
            {
                return playerNum;
            }
        }

        public Player(Texture2D sprite, Vector2 position, PlayerIndex playerNum) : base(sprite, position)
        {
            this.playerNum = playerNum;
            curState = PlayerState.PLAYER_OTHER;

            this.speed = 5;
            this.testAttack = new SimpleAttack(new Rectangle(0, 0, 32, 16), this, 1.5f, 50);
            
            if(sprite != null)  //HACK
                this.playerCenter = new Vector2(sprite.Width / 2, sprite.Height / 2);
        }

        public void HandleInput()
        {
            KeyboardState kb = Keyboard.GetState();         //these state checks might be moved, but they should work just fine here
            GamePadState gp = GamePad.GetState(playerNum);
            MouseState ms = Mouse.GetState();

            velocity.X = velocity.Y = 0;

            if (kb.IsKeyDown(Keys.W))
                velocity.Y -= speed;
            if (kb.IsKeyDown(Keys.S))
                velocity.Y += speed;
            if (kb.IsKeyDown(Keys.D))
                velocity.X += speed;
            if (kb.IsKeyDown(Keys.A))
                velocity.X -= speed;
            
            //basic state handling for attack - hacky, change later
            if (kb.IsKeyDown(Keys.Space))
            {
                if (velocity.X > 0)
                    curState = PlayerState.PLAYER_ATTACK_RIGHT;
                else if (velocity.X < 0)
                    curState = PlayerState.PLAYER_ATTACK_LEFT;
                else if (velocity.Y > 0)
                    curState = PlayerState.PLAYER_ATTACK_DOWN;
                else if (velocity.Y < 0)
                    curState = PlayerState.PLAYER_ATTACK_UP;
                else
                    curState = PlayerState.PLAYER_ATTACK_RIGHT;
            }
            else
                curState = PlayerState.PLAYER_OTHER;
        }

        public override void Logic()
        {
            //Lots of hack here
            DebugText dt = DebugText.GetInstance();
            dt.WriteLine("Initial Velocity: " + velocity);
            dt.WriteLine("Initial Position: " + position);
            
            if (position.X - velocity.X < 0)
                position.X = 0;
            else if (position.X + velocity.X + sprite.Width >= GamePlayLogicManager.GetInstance().MapRect.Width)
                position.X = GamePlayLogicManager.GetInstance().MapRect.Width - sprite.Width;
            if (position.Y - velocity.Y < 0)
                position.Y = 0;
            else if (position.Y + velocity.Y + sprite.Height >= GamePlayLogicManager.GetInstance().MapRect.Height)
                position.Y = GamePlayLogicManager.GetInstance().MapRect.Height - sprite.Height;

            if(velocity != new Vector2(0, 0))   //small optimization - don't bother checking for collisions if the entity isn't moving
                HandleCollisions();

            collisionTree.UpdatePosition(this); //We'll need to do this for every moving collidable entity, consider putting in MobileEntity abstract class or something

            dt.WriteLine("Adjusted Velocity: " + velocity);

            

            //we'll probably want to move this somewhere else, either to MobileEntity or to a player abstract class
            //this idea should work for ranged attacks, too, though we should probably use an actual collidable object for those
            //the rectangle I'm using here, incidentally, is a 16x32 rectangle with the short side on whatever side the player is attacking from
            //the +/- 8s should make it centered (that is, since we're using a 32x32 sprite here, it should have eight pixels clear on either side)

            if (testAttack.TimeLeft == 0)   //if we can make an attack (that is, it's been an acceptable length since the last one)
            {
                if (curState != PlayerState.PLAYER_OTHER)   //does the player actually want to attack?
                {
                    switch (curState)
                    {
                        case (PlayerState.PLAYER_ATTACK_UP):
                            testAttack.Attack(new Vector2(position.X + boundingBox.Width / 2, position.Y + boundingBox.Height / 2), 90, 10);
                            //hitBox = new Rectangle((int)(this.position.X + 8), (int)(this.position.Y - 32), 16, 32);
                            break;
                        case (PlayerState.PLAYER_ATTACK_DOWN):
                            testAttack.Attack(new Vector2(position.X + boundingBox.Width / 2, position.Y + boundingBox.Height / 2), 270, 10);
                            //hitBox = new Rectangle((int)(this.position.X + 8), (int)(this.position.Y + 32), 16, 32);
                            break;
                        case (PlayerState.PLAYER_ATTACK_LEFT):
                            testAttack.Attack(new Vector2(position.X + boundingBox.Width / 2, position.Y + boundingBox.Height / 2), 180, 10);
                            //hitBox = new Rectangle((int)(this.position.X - 32), (int)(this.position.Y - 8), 32, 16);
                            break;
                        case (PlayerState.PLAYER_ATTACK_RIGHT):
                            testAttack.Attack(new Vector2(position.X + boundingBox.Width / 2, position.Y + boundingBox.Height / 2), 0, 10);
                            //hitBox = new Rectangle((int)(this.position.X + 32), (int)(this.position.Y - 8), 32, 16);
                            break;
                    }

                }
            }
            else //if there has been an attack recently, do nothing, just decrement the time left
                testAttack.DecrementTimeLeft();

        }

        public override void CheckStatus()
        {
            //Stuff like handling death and such will go here.
        }

        public override void Hit(int amount, int type)
        {
            
        }
    }
}
