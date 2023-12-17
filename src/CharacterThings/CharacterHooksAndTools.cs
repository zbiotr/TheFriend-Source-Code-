﻿using TheFriend.CharacterThings.BelieverThings;
using TheFriend.CharacterThings.DelugeThings;
using TheFriend.CharacterThings.FriendThings;
using TheFriend.PoacherThings;
using TheFriend.SlugcatThings;
using RWCustom;
using UnityEngine;

namespace TheFriend.CharacterThings;

public class CharacterHooksAndTools
{
    public static void Apply()
    {
        PoacherHooks.Apply();
        FriendHooks.Apply();
        NoirThings.NoirCatto.Apply();
        BelieverHooks.Apply();
        DelugeHooks.Apply();
        
        SlugcatGameplay.Apply();
        SlugcatGraphics.Apply();
        SensoryHolograms.Apply();
    }
    
    public static void Squint(PlayerGraphics self, RoomCamera.SpriteLeaser sLeaser)
    {
        var face = sLeaser.sprites[9];
        if (self.player.dead) return;
        if (self.player.GetGeneral().squint && !face.element.name.Contains("Stunned"))
            face.element =
                Futile.atlasManager.GetElementWithName(face.element.name.Remove(face.element.name.Length-2, 2) + "Stunned");
    }

    public static void HeadShiver(PlayerGraphics self, float intensity)
    {
        self.head.vel += Custom.RNV() * intensity;
    }
    public static void LookAtRain(PlayerGraphics self)
    {
        self.objectLooker.LookAtPoint(new Vector2(
            self.player.room.PixelWidth * UnityEngine.Random.value, 
            self.player.room.PixelHeight + 100f), 
            (1f - self.player.room.world.rainCycle.RainApproaching) * 0.6f);
    }
    
    public enum colormode
    {
        set,
        mult,
        add
    }
    public static Color ColorMaker(
        float hue, float sat, float val, 
        colormode hueMode, colormode satMode, colormode valMode, 
        Color origCol = new Color(), Vector3 origHSL = new Vector3())
    {   // This method is pretty much exclusively for easy Jolly Co-op autocoloring
        // Negative floats can be used to preserve the original value
        Vector3 color = Custom.ColorToVec3(Color.black);
        if (origCol != Color.black) color = Custom.RGB2HSL(origCol);
        if (origHSL != Vector3.zero) color = origHSL;
        float newhue = color.x;
        float newsat = color.y;
        float newval = color.z;
        
        color.x = hueMode switch
        {
            colormode.set => newhue = (hue < 0) ? color.x : hue,
            colormode.add => newhue += (hue < 0) ? 0 : hue,
            colormode.mult => newhue *= (hue < 0) ? 1 : hue,
            _ => newhue = 0
        };
        color.y = satMode switch
        {
            colormode.set => newsat = (sat < 0) ? color.y : sat,
            colormode.add => newsat += (sat < 0) ? 0 : sat,
            colormode.mult => newsat *= (sat < 0) ? 1 : sat,
            _ => newsat = 0
        };
        color.z = valMode switch
        {
            colormode.set => newval = (val < 0) ? color.z : val,
            colormode.add => newval += (val < 0) ? 0 : val,
            colormode.mult => newval *= (val < 0) ? 1 : val,
            _ => newval = 0
        };
        
        color.x = newhue;
        color.y = newsat;
        color.z = newval;

        if (color == Vector3.zero) return Color.magenta;
        return Custom.Vec3ToColor(color);
    }
}