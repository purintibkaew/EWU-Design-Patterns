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

        private GameUIElement playerUI;

        public enum PlayerKeyBind { PLAYER_MOVE_UP = 0, PLAYER_MOVE_DOWN = 1, PLAYER_MOVE_RIGHT = 2, PLAYER_MOVE_LEFT = 3, PLAYER_ATTACK = 4, PLAYER_PICKUP_ITEM = 5 };
        private PlayerInputBinding[] playerInputBindings = new PlayerInputBinding[Enum.GetNames(typeof(PlayerKeyBind)).Length];

        public PlayerInputBinding [] PlayerBindings
        {
            get
            {
                return playerInputBindings;
            }
        }

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

        public Player(Texture2D sprite, Vector2 position, PlayerIndex playerNum, Stats entStats, GameUIElement playerUI) : base(sprite, position, entStats)
        {
            this.playerNum = playerNum;
            curState = PlayerState.PLAYER_OTHER;

            this.playerUI = playerUI;

            this.testAttack = new SimpleAttack(new Rectangle(0, 0, 32, 16), this, 1.5f, 50);
            
            this.playerCenter = new Vector2(sprite.Width / 2, sprite.Height / 2);
        }

        public Player(PlayerIndex playerNum)
        {
            this.playerNum = playerNum;
        }

        public void HandleInput()
        {
            KeyboardState kb = Keyboard.GetState();         //these state checks might be moved, but they should work just fine here
            GamePadState gp = GamePad.GetState(playerNum);
            MouseState ms = Mouse.GetState();

            velocity.X = velocity.Y = 0;

            if(playerInputBindings[(int)PlayerKeyBind.PLAYER_MOVE_UP].IsInputDown(kb, gp))
                velocity.Y -= entStats.Speed;
            if (playerInputBindings[(int)PlayerKeyBind.PLAYER_MOVE_DOWN].IsInputDown(kb, gp))
                velocity.Y += entStats.Speed;
            if (playerInputBindings[(int)PlayerKeyBind.PLAYER_MOVE_RIGHT].IsInputDown(kb, gp))
                velocity.X += entStats.Speed;
            if (playerInputBindings[(int)PlayerKeyBind.PLAYER_MOVE_LEFT].IsInputDown(kb, gp))
                velocity.X -= entStats.Speed;
            if (playerInputBindings[(int)PlayerKeyBind.PLAYER_PICKUP_ITEM].IsInputDown(kb, gp))
                PickUpItem();

            //basic state handling for attack - hacky, change later
            if (playerInputBindings[(int)PlayerKeyBind.PLAYER_ATTACK].IsInputDown(kb, gp))
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
                            testAttack.Attack(new Vector2(position.X + boundingBox.Width / 2, position.Y + boundingBox.Height / 2), 90, entStats.Attack);
                            //hitBox = new Rectangle((int)(this.position.X + 8), (int)(this.position.Y - 32), 16, 32);
                            break;
                        case (PlayerState.PLAYER_ATTACK_DOWN):
                            testAttack.Attack(new Vector2(position.X + boundingBox.Width / 2, position.Y + boundingBox.Height / 2), 270, entStats.Attack);
                            //hitBox = new Rectangle((int)(this.position.X + 8), (int)(this.position.Y + 32), 16, 32);
                            break;
                        case (PlayerState.PLAYER_ATTACK_LEFT):
                            testAttack.Attack(new Vector2(position.X + boundingBox.Width / 2, position.Y + boundingBox.Height / 2), 180, entStats.Attack);
                            //hitBox = new Rectangle((int)(this.position.X - 32), (int)(this.position.Y - 8), 32, 16);
                            break;
                        case (PlayerState.PLAYER_ATTACK_RIGHT):
                            testAttack.Attack(new Vector2(position.X + boundingBox.Width / 2, position.Y + boundingBox.Height / 2), 0, entStats.Attack);
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

            playerUI.UIText = "HP: " + curHealth + "/" + entStats.MaxHP + "\nAttack: " + entStats.Attack;

            //on death
            if(curHealth <= 0)
            {
                //remove self from various managers
                GamePlayLogicManager.GetInstance().RemoveCollidable(this);
                GamePlayLogicManager.GetInstance().RemoveUpdatable(this);
                GamePlayLogicManager.GetInstance().Remove(this);
                GamePlayDrawManager.GetInstance().Remove(this, GamePlayDrawManager.DRAW_LIST_LEVEL.ENTITY);

                //disable entity
                this.entityIsActive = false;

                //update the player manager to check to see if there are any players left
                PlayerManager.GetInstance().PlayerKilled();

            }
        }

        public override void Hit(int amount, int type)
        {
            GamePlayDrawManager.GetInstance().UI.AddElementA(new GameUIElement(GamePlayDrawManager.GetInstance().UI, "*THUNK*", position, 10));

            curHealth -= amount;
        }

        private void PickUpItem()
        {
            boundingBox.Location = new Point((int)position.X, (int)position.Y); //KLUDGY - sprite bounding box is at 0, 0, ostensibly

            /*
            Collidable[] nearbyCollidables = collisionTree.GetItems(boundingBox);

            for (int i = 0; i < nearbyCollidables.Length; i++)
            {
                if (!nearbyCollidables[i].Equals(this))
                {
 
                }
            }
             * 
             */
        }
    }
    
    //a basic class for associating keys and buttons to let the player class easily tell whether something has been pressed without having to check bindings
    class PlayerInputBinding
    {
        private Keys keyboardKey;
        private Buttons controllerButton;

        public PlayerInputBinding(Keys keyboardKey, Buttons controllerButton)
        {
            this.keyboardKey = keyboardKey;
            this.controllerButton = controllerButton;
        }

        public bool IsInputDown(KeyboardState kb, GamePadState gp)
        {
            return  kb.IsKeyDown(keyboardKey) || gp.IsButtonDown(controllerButton); //if either the controller button or the keyboard key is pressed, we want to return true
        }
    }
}
