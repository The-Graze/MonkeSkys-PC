using BepInEx;
using System.IO;
using System.Linq;
using UnityEngine;

namespace MonkeSkys_PC
{
    [BepInPlugin(PluginInfo.Guid, PluginInfo.Name, PluginInfo.Version)]
    internal class Plugin : BaseUnityPlugin
    {
        private readonly string _imageFolder = Path.Combine(Paths.PluginPath, PluginInfo.Name, "Sky");
        private string _texturePath, _imageName;
        private Texture2D _tex;

        public void Awake()
        {
            GorillaTagger.OnPlayerSpawned(OnGameInitialized);
        }

        private void OnGameInitialized()
        {
            _texturePath = FindSkyImage();
            if (_texturePath != null)
            {
                _imageName = Path.GetFileName(_texturePath);
                LoadImage(_texturePath);
                Logger.LogMessage($"Loaded Sky image: {_imageName} as skybox");
            }

            Logger.LogMessage("No 'sky' image found in Sky folder.");
        }

        private string FindSkyImage()
        {
            if (!Directory.Exists(_imageFolder))
                Directory.CreateDirectory(_imageFolder);

            string[] validExtensions = [".png", ".jpg", ".jpeg", ".bmp"];
            var files = Directory.GetFiles(_imageFolder);

            return files.FirstOrDefault(file =>
                validExtensions.Contains(Path.GetExtension(file).ToLower()));
        }
        

        private void LoadImage(string path)
        {
            _tex = new Texture2D(1, 1)
            {
                filterMode = FilterMode.Point
            };

            var bytes = File.ReadAllBytes(path);
            _tex.LoadImage(bytes);
            _tex.Apply();

            ReplaceAllSkyboxTextures(_tex);
            BetterDayNightManager.instance.SetOverrideIndex(0);
        }

        private static void ReplaceAllSkyboxTextures(Texture2D newTexture)
        {
            Replace(BetterDayNightManager.instance.beachDayNightSkyboxTextures);
            Replace(BetterDayNightManager.instance.cloudsDayNightSkyboxTextures);
            Replace(BetterDayNightManager.instance.dayNightSkyboxTextures);
            Replace(BetterDayNightManager.instance.dayNightWeatherSkyboxTextures);
            return;

            void Replace(Texture2D[] textures)
            {
                for (var i = 0; i < textures.Length; i++)
                {
                    textures[i] = newTexture;
                }
            }
        }
    }
}
