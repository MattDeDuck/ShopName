﻿using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Newtonsoft.Json.Linq;
using PotionCraft.SceneLoader;
using PotionCraft.ManagersSystem.Game;
using PotionCraft.ManagersSystem.Room;
using System;
using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;


namespace ShopName
{
    [BepInPlugin(PLUGIN_GUID, "PotionCraftShopName", "1.1.2.0")]
    [BepInProcess("Potion Craft.exe")]
    public class Plugin : BaseUnityPlugin
    {
        public const string PLUGIN_GUID = "com.mattdeduck.potioncraftshopname";
        public static ManualLogSource Log { get; set; }
        public static string pluginLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static Texture2D shopNameBigTexture;
        public static Sprite shopNameBigSprite;

        public static string t_shopNameText;
        public static TextMeshPro shopNameText;

        public void Awake()
        {
            Log = this.Logger;

            // Create the texture and sprite
            shopNameBigTexture = LoadTextureFromFile(pluginLoc + "/shopbgBIG.png");
            shopNameBigSprite = Sprite.Create(shopNameBigTexture, new Rect(0, 0, shopNameBigTexture.width, shopNameBigTexture.height), new Vector2(0.5f, 0.5f));

            // Grab the shop name from the JSON file
            JObject shpNameObj = JObject.Parse(File.ReadAllText(pluginLoc + "/shopname.json"));
            t_shopNameText = (string)shpNameObj["Shop Name"];

            Harmony.CreateAndPatchAll(typeof(Plugin));
        }

        [HarmonyPostfix, HarmonyPatch(typeof(GameManager), "Start")]
        public static void Start_Postfix()
        {
            ObjectsLoader.AddLast("SaveLoadManager.SaveNewGameState", () => CreateShopPlate());
        }

        public static void CreateShopPlate()
        {
            CreateShopNameObject(shopNameBigSprite);
            AddShopNameText();
        }

        public static void CreateShopNameObject(Sprite spriteSize)
        {
            // Give the GameObject a name
            var shopNameObject = new GameObject("Shop Name");

            // Give it a parent
            var parentGO = GameObject.Find("Room Meeting").transform;
            shopNameObject.transform.parent = parentGO;

            // Give it a sprite
            var sr = shopNameObject.AddComponent<SpriteRenderer>();
            sr.sprite = spriteSize;

            // Give it a sorting sorting group
            var sg = shopNameObject.AddComponent<SortingGroup>();
            sg.sortingLayerID = -1758066705;
            sg.sortingLayerName = "GuiBackground";

            // Give it a layer
            shopNameObject.layer = LayerMask.NameToLayer("UI");

            // Change the position
            shopNameObject.transform.localPosition = new Vector3(-3f, -6.3f, 0f);

            // Make it active
            shopNameObject.SetActive(true);

            Log.LogInfo("Shop Name object created");
        }

        public static void AddShopNameText()
        {
            // Create a new GameObject for the text
            GameObject textHolder = new();

            // Give it the shop name background as a parent
            GameObject parent = GameObject.Find("Shop Name");
            if (parent is not null)
            {
                textHolder.transform.SetParent(parent.transform);
            }

            // Give the text object a name
            textHolder.name = "ShopNameText";

            // Move it to the centre of the shop name background object
            textHolder.transform.Translate(parent.transform.position + new Vector3(0, -0.02f));
            textHolder.layer = 5;

            // Add the TextMeshPro component to the text object
            shopNameText = textHolder.AddComponent<TextMeshPro>();

            // Customise the TextMeshPro settings
            shopNameText.alignment = TextAlignmentOptions.Center;
            shopNameText.enableAutoSizing = true;
            shopNameText.sortingLayerID = -1650695527;
            shopNameText.sortingOrder = 100;
            shopNameText.fontSize = 8;
            shopNameText.fontSizeMin = 3;
            shopNameText.fontSizeMax = 8;
            shopNameText.color = new Color32(57, 30, 20, 255);

            // Set size of rect text will fit to
            shopNameText.rectTransform.sizeDelta = new Vector2(11f, 1f);

            shopNameText.text = t_shopNameText;

            // Set it to active
            textHolder.SetActive(true);

            Log.LogInfo("Shop Name Text object created");
        }

        // Loading textures from file
        public static Texture2D LoadTextureFromFile(string filePath)
        {
            var data = File.ReadAllBytes(filePath);

            // Do not create mip levels for this texture, use it as-is.
            var tex = new Texture2D(0, 0, TextureFormat.ARGB32, false, false)
            {
                filterMode = FilterMode.Bilinear,
            };

            if (!tex.LoadImage(data))
            {
                throw new Exception($"Failed to load image from file at \"{filePath}\".");
            }

            return tex;
        }
    }
}
