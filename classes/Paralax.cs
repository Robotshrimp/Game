using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom
{
    public class Parallax
    {
        static int BaseDepth = 10;
        public double ParallaxZoom(int Depth)
        {
            return (double)BaseDepth / Depth;
        }
    }
}
