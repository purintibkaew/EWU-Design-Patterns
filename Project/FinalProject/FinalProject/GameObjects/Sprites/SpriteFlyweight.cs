using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace FinalProject
{
    public class SpriteFlyweight : ISpriteFlyweight
    {
        private static IDictionary<String, Texture2D> spriteCache;
        private static Texture2D defaultSprite;
        private static ContentManager contentManager;


        public SpriteFlyweight() 
        {
            spriteCache = new Dictionary<String, Texture2D>();
            defaultSprite = new NullSprite();
            contentManager = GamePlayContentManager.GetInstance().GameContentManager;
        }

        public Texture2D GetSprite(String spriteFileName)
        {
            Texture2D sprite = defaultSprite;

            if (spriteFileName == null)
            {
                sprite = defaultSprite;
            }
            else if (spriteCache.ContainsKey(spriteFileName))
            {
                sprite = spriteCache[spriteFileName];
            }
            else
            {
                try
                {
                    Texture2D textureToStore = contentManager.Load<Texture2D>(spriteFileName);
                    spriteCache.Add(spriteFileName, textureToStore);
                    sprite = textureToStore;
                }
                catch(ContentLoadException)
                {
                    sprite = defaultSprite;
                }
            }

            return sprite;
        }
    }
}