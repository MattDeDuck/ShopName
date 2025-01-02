using PotionCraft.Core.Cursor;
using PotionCraft.ObjectBased.InteractiveItem;
using PotionCraft.ObjectBased.RecipeMap.Buttons;
using System;
using TooltipSystem;
using UnityEngine;

namespace ShopName.Utilities
{
    public class NewButton : InteractiveItem, IPrimaryCursorEventsHandler, ICustomCursorVisualStateOnUse, ICustomCursorVisualStateOnHover, IHoverable
    {
        public SpriteRenderer iconRenderer;
        public BoxCollider2D collider;
        public GameObject spriteSlot;
        public FollowIndicatorButton followButton;

        public float OffAlpha => 0.5f;

        public bool IsHovered { get => followButton.IsHovered; set => SetHovered(value); }

        public void OnPrimaryCursorClick()
        {

        }

        public bool OnPrimaryCursorRelease()
        {
            _onRelease?.Invoke();
            return true;
        }

        public CursorVisualState GetCursorVisualStateOnUse() => CursorVisualState.Pressed;
        public CursorVisualState GetCursorVisualStateOnHover() => CursorVisualState.Pressed;

        public void SetHovered(bool hovered)
        {
            followButton?.SetHovered(hovered);
        }

        public Action _onRelease;
        public Func<TooltipContent> _getTooltip;

        public override TooltipContent GetTooltipContent()
        {
            return _getTooltip?.Invoke();
        }
    }
}