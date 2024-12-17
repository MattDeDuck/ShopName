using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Newtonsoft.Json.Linq;
using PotionCraft.ObjectBased.ScalesSystem;
using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;


namespace ShopName
{
    [BepInPlugin(PLUGIN_GUID, "PotionCraftShopName", "1.2.1.0")]
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

        public static bool alreadyMade = false;

        public void Awake()
        {
            Log = this.Logger;

            // Create the texture and sprite
            shopNameBigTexture = Functions.LoadTextureFromFile(pluginLoc + "/shopbgBIG.png");
            shopNameBigSprite = Sprite.Create(shopNameBigTexture, new Rect(0, 0, shopNameBigTexture.width, shopNameBigTexture.height), new Vector2(0.5f, 0.5f));

            // Grab the shop name from the JSON file
            JObject shpNameObj = JObject.Parse(File.ReadAllText(pluginLoc + "/shopname.json"));
            t_shopNameText = (string)shpNameObj["Shop Name"];

            Harmony.CreateAndPatchAll(typeof(Plugin));
            Harmony.CreateAndPatchAll(typeof(Functions));
        }

        [HarmonyPostfix, HarmonyPatch(typeof(ScalesCup), "Awake")]
        public static void GoTo_Postfix()
        {
            if (GameObject.Find("Room Meeting(Clone)") != null)
            {
                if (!alreadyMade)
                {
                    if (GameObject.Find("CalendarButton") != null)
                    {
                        Log.LogInfo("Creating Shop Name Object...");
                        Functions.CreateText();
                    }
                    else
                    {
                        Log.LogInfo("null object");
                    }

                    alreadyMade = true;
                }
                else
                {
                    // Already created
                }
            }
            else
            {
                Log.LogInfo($"It doesn't work!");
            }
        }
    }
}
