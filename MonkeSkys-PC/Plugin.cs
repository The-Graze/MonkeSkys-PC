using Utilla;
using System;
using BepInEx;
using System.IO;
using UnityEngine;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using UnityEngine.Networking;
using System.Collections.Generic;
namespace MonkeSkys_PC
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    class Plugin : BaseUnityPlugin
    {
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "BepInEx", "Plugins", PluginInfo.Name.ToString(), "Sky");
        List<string> imagesPublic = new List<string>();
        List<string> imageNames = new List<string>();
        Texture2D tex;

        public void Awake()
        {
            Events.GameInitialized += OnGameInitialized;
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            GetImage();
            LoadImage();
        }
        void GetImage()
        {
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }
            string[] images = Directory.GetFiles(imagePath);
            string[] imagename = new string[images.Length];
            for (int i = 0; i < imagename.Length; i++)
            {
                imagename[i] = Path.GetFileName(images[i]);
                imageNames.Add(imagename[i]);
                imagesPublic.Add(Path.GetDirectoryName(imagePath + imagename[i]));

            }
        }
        void LoadImage()
        {
            tex = new Texture2D(1, 1);
            tex.filterMode = FilterMode.Point;
            byte[] bytes = File.ReadAllBytes(imagesPublic[0]);
            tex.LoadImage(bytes);
            tex.Apply();
            foreach (Texture t in BetterDayNightManager.instance.beachDayNightSkyboxTextures)
            {
                int i = Array.IndexOf(BetterDayNightManager.instance.beachDayNightSkyboxTextures, t);
                BetterDayNightManager.instance.beachDayNightSkyboxTextures[i] = tex;
            }
            foreach (Texture t in BetterDayNightManager.instance.cloudsDayNightSkyboxTextures)
            {
                int i = Array.IndexOf(BetterDayNightManager.instance.cloudsDayNightSkyboxTextures, t);
                BetterDayNightManager.instance.cloudsDayNightSkyboxTextures[i] = tex;
            }
            foreach (Texture t in BetterDayNightManager.instance.dayNightSkyboxTextures)
            {
                int i = Array.IndexOf(BetterDayNightManager.instance.dayNightSkyboxTextures, t);
                BetterDayNightManager.instance.dayNightSkyboxTextures[i] = tex;
            }
            foreach (Texture t in BetterDayNightManager.instance.dayNightWeatherSkyboxTextures)
            {
                int i = Array.IndexOf(BetterDayNightManager.instance.dayNightWeatherSkyboxTextures, t);
                BetterDayNightManager.instance.dayNightWeatherSkyboxTextures[i] = tex;
            }
            BetterDayNightManager.instance.SetOverrideIndex(0);
        }
    }
}