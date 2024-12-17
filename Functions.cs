using BepInEx;
using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace ShopName
{
    class Functions : BaseUnityPlugin
    {
        static ManualLogSource Log => Plugin.Log;

        public static void CreateText()
        {
            GameObject shopName = CreateShopNameObject();
            GameObject mObject = FindMeshParentObject();
            GameObject shopNameText = Instantiate(mObject, shopName.transform);

            TextMeshPro sr = shopNameText.GetComponent<TextMeshPro>();
            sr.sortingLayerID = -1758066705;
            sr.alignment = TextAlignmentOptions.Center;
            sr.enableAutoSizing = true;
            sr.sortingOrder = 100;
            sr.fontSize = 8;
            sr.fontSizeMin = 3;
            sr.fontSizeMax = 8;
            sr.color = new Color32(57, 30, 20, 255);

            // Set size of rect text will fit to
            sr.rectTransform.sizeDelta = new Vector2(11f, 1f);
            sr.text = Plugin.t_shopNameText;
            sr.DeleteAllSubMeshes();
            sr.enabled = true;

            shopName.transform.localPosition = new Vector3(-2.4f, -6.3f, 0f);
        }

        public static GameObject FindMeshParentObject()
        {
            List<TMP_SubMesh> meshes = Resources.FindObjectsOfTypeAll<TMP_SubMesh>().ToList();
            GameObject meshParent = new();
            if (meshes != null)
            {
                List<TMP_SubMesh> sortedMesh = meshes.Where(s => s.name.Equals("TMP SubMesh [TextMeshPro/Sprite]")).ToList(); ;
                GameObject meshObject = sortedMesh[1].gameObject;
                meshParent = meshObject.transform.GetParent().gameObject;
            }
            return meshParent;
        }

        public static GameObject CreateShopNameObject()
        {
            var shopNameObject = new GameObject("Shop Name");
            var parentGO = GameObject.Find("Room Meeting(Clone)").transform;
            shopNameObject.transform.parent = parentGO;
            Transform snot = shopNameObject.transform;
            snot.localPosition = new Vector3(0f, 0f, 0f);

            SpriteRenderer sr = shopNameObject.AddComponent<SpriteRenderer>();
            //sr.sprite = Resources.FindObjectsOfTypeAll<Sprite>().Where(s => s.name == "Calendar Button Idle").First();
            sr.sprite = Plugin.shopNameBigSprite;
            sr.sortingLayerID = -1758066705;
            sr.enabled = true;

            Log.LogInfo("Shop Name object created");
            return shopNameObject;
        }

        public static void CreateShopNameObject(Sprite spriteSize)
        {
            // Give the GameObject a name
            var shopNameObject = new GameObject("Shop Name");

            // Give it a parent
            var parentGO = GameObject.Find("Room Meeting(Clone)").transform;
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

        public static Texture2D LoadTextureFromFile(string filePath)
        {
            var data = File.ReadAllBytes(filePath);

            // Do not create mip levels for this texture, use it as-is.
            var tex = new Texture2D(2, 2, TextureFormat.ARGB32, false, false)
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
