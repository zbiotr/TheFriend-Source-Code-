﻿using System.Runtime.CompilerServices;
using Color = UnityEngine.Color;
using UnityEngine;

namespace TheFriend.CharacterThings.DelugeThings;

public static class DelugeCWT
{
    public class Deluge
    {
        public DataPearl pearl;
        public Vector2 tailtip;
        public Vector2 tailtip2;
        public bool PearlWasTaken;
        public Deluge(Player player)
        {
            
        }
    }
    public static readonly ConditionalWeakTable<Player, Deluge> CWT = new();
    public static Deluge GetDeluge(this Player player) => CWT.GetValue(player, _ => new(player));

    public static bool TryGetDeluge(this Player player, out Deluge data)
    {
        if (player.SlugCatClass == Plugin.DelugeName)
        {
            data = player.GetDeluge();
            return true;
        }
        data = null;
        return false;
    }
    
}