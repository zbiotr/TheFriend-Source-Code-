﻿using LizardCosmetics;
using TheFriend.Creatures.LizardThings.FreeLizardCosmetics.Dependencies;

namespace TheFriend.Creatures.LizardThings.FreeLizardCosmetics.Common;

public class FreeShortBodyScales : FreedCosmeticTemplate
{ // Untested
    public FreeShortBodyScales(ShortBodyScales template) : base(template)
    {
        HeadColorForBase = true;
    }
    public FreeShortBodyScales(LizardGraphics lGraphics, int startSprite) : base(lGraphics, startSprite)
    {
        HeadColorForBase = true;
    }
    
    public override void InitiateSprites(RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam)
    {
        ImMirrored = (owner as ShortBodyScales)!.scalesPositions.Length.IsEven();
        base.InitiateSprites(sLeaser,rCam);
        RectifySizeBonusForDraw("XY");
    }
    public override Template ConstructBaseTemplate(LizardGraphics liz, int startsprite)
    {
        return new ShortBodyScales(lGraphics, startSprite);
    }
}