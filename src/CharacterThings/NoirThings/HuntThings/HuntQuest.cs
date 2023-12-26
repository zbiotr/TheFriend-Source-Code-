using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TheFriend.HudThings;
using TheFriend.WorldChanges;
using UnityEngine;
using static MoreSlugcats.MoreSlugcatsEnums.CreatureTemplateType;

namespace TheFriend.CharacterThings.NoirThings.HuntThings;

public abstract partial class HuntQuestThings
{
    public class ObservableTargetCollection<T> : ObservableCollection<T>
    {
        public readonly HuntQuest Owner;
        public ObservableTargetCollection(HuntQuest owner)
        {
            Owner = owner;
        }
    }

    public class HuntQuest
    {
        public readonly ObservableTargetCollection<CreatureTemplate.Type> Targets;

        public HuntQuest(List<CreatureTemplate.Type> targets) : this()
        {
            foreach (var target in targets) Targets.Add(target);
        }
        public HuntQuest()
        {
            Targets = new ObservableTargetCollection<CreatureTemplate.Type>(this);
        }

        public bool Completed => !Targets.Any();

        //-----

        public static CreatureSprite GetHuntSprite(CreatureTemplate.Type type)
        {
            CreatureSprite sprite;
            if (type == HuntCentipede || type == HuntCicada || type == HuntEggbug || type == HuntDLL)
            {
                sprite = new CreatureSprite(TypeTranslator(type));
                sprite.CreatureType = type;
            }
            else
            {
                sprite = new CreatureSprite(type);
            }

            if (type == HuntCicada) sprite.CreatureColor = new Color(0.55f, 0.85f, 0.95f);
            if (type == CreatureTemplate.Type.LanternMouse) sprite.CreatureColor = new Color(0.4f, 0.85f, 0.95f);
            if (type == HuntCentipede || type == CreatureTemplate.Type.Centipede || type == CreatureTemplate.Type.SmallCentipede)
                sprite.CreatureColor = Color.HSVToRGB(FamineWorld.defCentiColor, FamineWorld.defCentiSat, 1f);

            sprite.targetColor = sprite.CreatureColor;
            sprite.color = sprite.CreatureColor;
            return sprite;
        }
        public static CreatureTemplate.Type TypeTranslator(CreatureTemplate.Type type)
        {
            if (type == CreatureTemplate.Type.Centipede || type == CreatureTemplate.Type.Centiwing || type == AquaCenti)
                return HuntCentipede;
            if (type == CreatureTemplate.Type.CicadaA || type == CreatureTemplate.Type.CicadaB)
                return HuntCicada;
            if (type == CreatureTemplate.Type.EggBug || type == FireBug)
                return HuntEggbug;
            if (type == CreatureTemplate.Type.DaddyLongLegs || type == TerrorLongLegs)
                return HuntDLL;

            if (type == HuntCentipede)
                return CreatureTemplate.Type.Centipede;
            if (type == HuntCicada)
                return CreatureTemplate.Type.CicadaA;
            if (type == HuntEggbug)
                return CreatureTemplate.Type.EggBug;
            if (type == HuntDLL)
                return CreatureTemplate.Type.DaddyLongLegs;

            return type;
        }

        public static readonly CreatureTemplate.Type HuntCentipede = new CreatureTemplate.Type(nameof(HuntCentipede));
        public static readonly CreatureTemplate.Type HuntCicada = new CreatureTemplate.Type(nameof(HuntCicada));
        public static readonly CreatureTemplate.Type HuntEggbug = new CreatureTemplate.Type(nameof(HuntEggbug));
        public static readonly CreatureTemplate.Type HuntDLL = new CreatureTemplate.Type(nameof(HuntDLL));
    }
}