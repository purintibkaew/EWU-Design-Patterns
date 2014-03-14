using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalProject
{
    class GameEndTrigger : Updatable
    {
        private Rectangle boundingBox;

        private PlayerManager pm;

        private StateManager sm;

        public GameEndTrigger(Rectangle boundingBox)
        {
            this.boundingBox = boundingBox;
            pm = PlayerManager.GetInstance();
            sm = StateManager.GetInstance();
        }

        public void CheckStatus()
        {
            List<Player> curPlayers = pm.GetActivePlayers();

            foreach (Player p in curPlayers)
            {
                if (p.BoundingBox.Intersects(boundingBox) || p.BoundingBox.Contains(boundingBox) || boundingBox.Contains(p.BoundingBox))
                {
                    sm.NextState = sm.getState(StateManager.States.GameWonState);
                }
            }
        }
    }
}
