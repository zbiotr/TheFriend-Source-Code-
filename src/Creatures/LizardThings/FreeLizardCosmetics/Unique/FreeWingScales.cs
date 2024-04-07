﻿using LizardCosmetics;
using TheFriend.Creatures.LizardThings.FreeLizardCosmetics.Dependencies;
using UnityEngine;

namespace TheFriend.Creatures.LizardThings.FreeLizardCosmetics.Unique;

public class FreeWingScales : FreedCosmeticTemplate
{ // Untested
    public FreeWingScales(WingScales template) : base(template)
    {
        ImMirrored = true;
    }
    public FreeWingScales(LizardGraphics lGraphics, int startSprite) : base(lGraphics, startSprite)
    {
        ImMirrored = true;
    }

    public override void InitiateSprites(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam)
    {
        base.InitiateSprites(sLeaser,rCam);
        ImColored = false;
        drawSizeBonus = new Vector2(0, drawSizeBonus.y);
    }
    public override Template ConstructAndAddBaseTemplate(LizardGraphics liz, int startsprite)
    {
        var newCosmetic = new WingScales(lGraphics, startSprite);
        liz.AddCosmetic(startSprite, newCosmetic);
        return newCosmetic;
    }
}