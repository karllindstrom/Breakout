using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    public class Brick
    {
        public Microsoft.Xna.Framework.Rectangle Bounds {  get; set; }
        public Texture2D BrickTex {  get; set; }

        public bool IsDestroyed { get; set; }


        public Brick(Microsoft.Xna.Framework.Rectangle bounds, Texture2D brickTex) 
        {
            Bounds = bounds;
            BrickTex = brickTex;
            IsDestroyed = false;

        }
    }
}
