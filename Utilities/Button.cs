using BepInEx.Logging;
using System.Collections.Generic;
using System;
using TooltipSystem;
using UnityEngine;

namespace ShopName.Utilities
{
    class Button
    {
        static ManualLogSource Log => Plugin.Log;

        public static NewButton GetNewButton(Transform parent, Sprite spriteName, string name, Vector3 buttonPos, Action onRelease, Func<TooltipContent> getTooltip, Vector2 toolPos)
        {
            var gameObject = new GameObject(name)
            {
                layer = LayerMask.NameToLayer("UI")
            };

            var newButton = gameObject.AddComponent<NewButton>();
            newButton.enabled = true;
            newButton.spriteSlot = new GameObject("SpriteSlot");
            newButton.spriteSlot.transform.parent = gameObject.transform;
            newButton.spriteSlot.transform.localScale *= 1.5f;

            newButton.tooltipContentProvider = gameObject.AddComponent<TooltipContentProvider>();

            var sprite = spriteName;

            var spriteRenderer = newButton.spriteSlot.AddComponent<SpriteRenderer>();
            newButton.iconRenderer = spriteRenderer;

            var buttonCollider = gameObject.AddComponent<BoxCollider2D>();
            newButton.collider = buttonCollider;
            buttonCollider.enabled = true;
            buttonCollider.isTrigger = true;
            buttonCollider.size = sprite.rect.size / sprite.pixelsPerUnit;
            gameObject.transform.parent = parent;
            spriteRenderer.sprite = sprite;
            spriteRenderer.color = Color.white;
            spriteRenderer.sortingLayerID = -1758066705;
            spriteRenderer.enabled = true;

            gameObject.transform.localPosition = buttonPos;

            newButton.tooltipContentProvider.fadingType = TooltipContentProviderFadingType.UIElement;
            newButton.tooltipContentProvider.positioningSettings = new List<TooltipPositioningSettings>()
                {
                    new TooltipPositioningSettings()
                    {
                        bindingPoint = TooltipBindingPoint.TransformPosition,
                        freezeX = true,
                        freezeY = true,
                        position = toolPos,
                        tooltipCorner = TooltipCorner.LeftTop
                    }
                };
            newButton.tooltipContentProvider.tooltipCollider = buttonCollider;

            newButton._onRelease = onRelease;
            newButton._getTooltip = getTooltip;

            return newButton;
        }

        public static GameObject CreateButtonHolder()
        {
            var buttonParentObject = new GameObject("Buttons");
            var parentGO = GameObject.Find("Room Meeting(Clone)").transform;
            buttonParentObject.transform.parent = parentGO;
            Transform snot = buttonParentObject.transform;
            snot.localPosition = new Vector3(0f, 0f, 0f);

            Log.LogInfo("Button holder created");
            return buttonParentObject;
        }

        public static void CreateTheButton()
        {
            GameObject buttonHolder = CreateButtonHolder();

            NewButton water = GetNewButton(buttonHolder.transform,
                                           Plugin.refreshButtonSprite,
                                           "RefreshShopNameButton", new Vector3(-8.75f, -6.53f, 0f),
                                           Functions.UpdateText,
                                           Functions.RefreshButton,
                                           new Vector2(-1.6f, 1.4f));
        }
    }
}