﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using LizardCosmetics;
using TheFriend.Creatures.LizardThings.FreeLizardCosmetics.Dependencies;
using UnityEngine;

namespace TheFriend.Creatures.LizardThings.FreeLizardCosmetics.Unique;

public class FreeBodyStripes : FreedCosmeticTemplate
{ // Unfinished
    public FreeBodyStripes(BodyStripes template) : base(template)
    {
        ImMirrored = true;
    }
    public FreeBodyStripes(LizardGraphics lGraphics, int startSprite) : base(lGraphics, startSprite)
    {
        ImMirrored = true;
    }
    public override Template ConstructAndAddBaseTemplate(LizardGraphics liz, int startsprite)
    {
        var newCosmetic = new BodyStripes(lGraphics, startSprite);
        liz.AddCosmetic(startSprite, newCosmetic);
        return newCosmetic;
    }
}