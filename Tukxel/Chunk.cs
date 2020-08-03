using System;
using System.Collections.Generic;
using System.Text;

namespace Tukxel
{
    struct Chunk
    {
        byte[,,,] UnCompressed;
        byte[] Compressed;

        public void Nullify()
        {
            UnCompressed = null;
            Compressed   = null;
        }

        public void Compress()
        {
            //uh logic stuff i guess
        }

        public void DeCompress()
        {
            //uh logic stuff i guess
        }

        public void Generate()
        {

        }

        public Mesh GenMesh()
        {
            Mesh mesh = new Mesh();

            return mesh;
        }
    }
}
