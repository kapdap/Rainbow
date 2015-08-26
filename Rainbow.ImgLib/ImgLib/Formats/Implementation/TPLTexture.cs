﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rainbow.ImgLib.Formats.Implementation
{
    public class TPLTexture : TextureContainer
    {
        internal static readonly string NAME = "TPL Texture";

        public override string Name
        {
            get { return NAME; }
        }
    }
}