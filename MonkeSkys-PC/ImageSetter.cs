﻿using System.IO;
using UnityEngine;

namespace UnityImageSetter
{
    public class ImageSetter
    {
        /// <summary>
        /// Converts an image file to a Texture2D
        /// </summary>
        /// <param name="fileLocation">The file location of the image</param>
        /// <param name="filterMode">The filter more, this can be Point, Bilinear or Trilinear</param>
        /// <param name="width">The amount of pixels for the width</param>
        /// <param name="height">The amount of pixels for the height</param>
        /// <returns>Texture2D</returns>
        public static Texture2D LoadImage(string fileLocation, FilterMode filterMode, int width, int height)
        {
            filterMode = FilterMode.Point;
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
            tex.filterMode = filterMode;

            byte[] bytes = File.ReadAllBytes(fileLocation);
            tex.LoadImage(bytes);
            tex.Apply();

            return tex;
        }
    }
}