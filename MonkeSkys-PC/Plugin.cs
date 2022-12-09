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
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
     class Plugin : BaseUnityPlugin
    {
        public static string imagePath;
        public static readonly List<string> imagesPublic = new List<string>();
        public static readonly List<string> imageNames = new List<string>();
        public static GameObject sky1;
        public static GameObject sky2;
        public static GameObject sky3;
        public static GameObject texload;

        public void Awake()
        {
            Events.GameInitialized += OnGameInitialized;
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            sky1 = GameObject.Find("Level/newsky/newsky (1)");
            sky2 = GameObject.Find("Level/city/CosmeticsRoomAnchor/rain/nightsky");
            sky3 = GameObject.Find("Level/newsky");
            texload = GameObject.CreatePrimitive(PrimitiveType.Plane);
            GetImage();
            LoadImage();
            sky1.GetComponent<Renderer>().material = texload.GetComponent<Renderer>().material;
            sky2.GetComponent<Renderer>().material = texload.GetComponent<Renderer>().material;
            sky3.GetComponent<Renderer>().material = texload.GetComponent<Renderer>().material;
            texload.SetActive(false);
        }
        void GetImage()
        {
            imagePath = Path.Combine(Directory.GetCurrentDirectory(), "BepInEx", "Plugins", PluginInfo.Name.ToString(), "Sky");
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
                imagesPublic.Add(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Sky\\" + imagename[i]);
                
            }
        }
        public static void LoadImage()
        {
            Texture2D tex = new Texture2D(1 ,1);
            tex.filterMode = FilterMode.Point;
            byte[] bytes = File.ReadAllBytes(imagesPublic[0]);
            tex.LoadImage(bytes);
            tex.Apply();
            texload.GetComponent<Renderer>().material.mainTexture = tex;
            texload.GetComponent<Renderer>().material.shader = Shader.Find("Mobile/Unlit (Supports Lightmap)");

        }
    }
}