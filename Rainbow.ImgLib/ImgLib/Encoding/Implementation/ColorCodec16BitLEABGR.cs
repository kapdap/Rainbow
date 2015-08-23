﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Rainbow.ImgLib.Encoding.Implementation
{
    public class ColorCodec16BitLEABGR : ColorCodec
    {
        public override Color[] DecodeColors(byte[] palette, int start, int size)
        {
            List<Color> pal = new List<Color>();

            BinaryReader reader = new BinaryReader(new MemoryStream(palette, start, size));
            for (int i = 0; i < size / 2; i++)
            {
                ushort data = reader.ReadUInt16();

                int red = data & 0x1F;
                data >>= 5;
                int green = data & 0x1F;
                data >>= 5;
                int blue = data & 0x1F;
                int alpha = data == 0 ? 0 : 255;

                pal.Add(Color.FromArgb(alpha, red * 8, green * 8, blue * 8));
            }

            return pal.ToArray();
        }

        public override byte[] EncodeColors(Color[] colors, int start, int length)
        {
            byte[] palette = new byte[colors.Length * 2];

            for (int i = start; i < colors.Length; i++)
            {
                ushort data = (ushort)(colors[i].A > 127 ? 0x8000 : 0);

                data |= (ushort)(((colors[i].B >> 3) << 10) | ((colors[i].G >> 3) << 5) | ((colors[i].R >> 3) & 0x1F));
                palette[(i - start) * 2] = (byte)(data & 0xFF);
                palette[(i - start) * 2 + 1] = (byte)(data >> 8);
            }
            return palette;
        }

        public override int BitDepth { get { return 16; } }
    }
}
