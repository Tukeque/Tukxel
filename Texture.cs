using System;
using System.Collections.Generic;
using System.IO;

using OpenTK.Graphics.OpenGL4;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

class Texture
{
    public int Handle;

    public void Use()
    {
        GL.BindTexture(TextureTarget.Texture2D, Handle);
    }

    public Texture(string Path)
    {
        Handle = GL.GenTexture();
        Use();

        Image<Rgba32> image = (Image<Rgba32>)Image.Load(Path);

        //ImageSharp loads from the top-left pixel, whereas OpenGL loads from the bottom-left, causing the texture to be flipped vertically.
        //This will correct that, making the texture display properly.
        image.Mutate(x => x.Flip(FlipMode.Vertical));

        List<byte> pixels = new List<byte>();

        for (int height = 0; height < image.Height; height++)
        {
            Span<Rgba32> row = image.GetPixelRowSpan(height);
            Rgba32[] Rgba32Row = row.ToArray();

            foreach (Rgba32 pixel in Rgba32Row)
            {
                pixels.Add(pixel.R);
                pixels.Add(pixel.G);
                pixels.Add(pixel.B);
                pixels.Add(pixel.A);
            }
        }

        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels.ToArray());
        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
    }
}