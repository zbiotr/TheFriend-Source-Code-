﻿using System.Linq;
using UnityEngine;
using TheFriend.SlugcatThings;

namespace TheFriend;

public class DragonRiding
{
    public class AbstractDragonRider : AbstractPhysicalObject.AbstractObjectStick
    {
        public AbstractPhysicalObject self
        {
            get => A;
            set => A = value;
        }
        public AbstractPhysicalObject obj
        {
            get => B;
            set => B = value;
        }
        public AbstractDragonRider(AbstractPhysicalObject self, AbstractPhysicalObject obj) : base(self, obj) { }
    }
    public static void DragonRiderSafety(Player self, Creature crit, Vector2 seat) // Values for the rider of mother lizard
    {
        if (!(crit as Lizard).GetLiz().IsRideable && (crit as Lizard).GetLiz() != null) { DragonRideReset(crit, self); return; }
        self.GetGeneral().rideStick ??= new AbstractDragonRider(self.abstractPhysicalObject, crit.abstractPhysicalObject);
        self.GetGeneral().isRidingLizard = true;
        self.GetGeneral().grabCounter = 15;
        self.bodyChunks[1].pos = seat;
        self.bodyChunks[0].pos = Vector2.Lerp(seat,seat + new Vector2(0,crit.firstChunk.rad),0.5f);
        self.CollideWithTerrain = false;
        self.CollideWithObjects = false;
        if (!self.abstractCreature.stuckObjects.Contains(self.GetGeneral()?.rideStick)) self.abstractCreature.stuckObjects.Add(self.GetGeneral()?.rideStick);
    }
    public static void DragonRidden(Creature crit, Player player) // Values for the lizard being ridden
    {
        var self = crit as Lizard;
        if (!self.GetLiz().IsRideable) { DragonRideReset(crit,player); return; }
        //self.GetLiz().IsBeingRidden = true;
    }
    public static void DragonRideReset(Creature crit, Player player) // Performed after riding stops
    {
        player.CollideWithTerrain = true;
        player.CollideWithObjects = true;
        player.GetGeneral().dragonSteed = null;
        if (crit is Lizard liz)
        {
            liz.GetLiz().boolseat0 = false;
            //liz.GetLiz().IsBeingRidden = false;
            liz.GetLiz().rider = null;
        }
        player.GetGeneral().isRidingLizard = false;
        if (player.GetGeneral()?.rideStick != null)
        {
            player.GetGeneral().rideStick.Deactivate();
            player.GetGeneral().rideStick = null;
            player.abstractCreature.stuckObjects.Remove(player.GetGeneral().rideStick);
        }
    }

    public static void DragonRideCommands(Lizard liz, Player rider)
    {
        var input = rider.GetGeneral().UnchangedInputForLizRide;

        // Drop it!
        if (input[0].y < 0)
        {
            if (!(input[1].y < 0))
            {
                for (int i = 2; i < input.Length - 1; i++)
                {
                    if (input[i].y < 0 && !(input[i + 1].y < 0))
                    {
                        liz.ReleaseGrasp(0);
                        liz.voice.MakeSound(LizardVoice.Emotion.Submission);
                        rider.Blink(12);
                        rider.room.PlaySound(SoundID.Vulture_Grab_Player, rider.firstChunk.pos,0.5f,1);
                        rider.room.AddObject(new ExplosionSpikes(rider.room, rider.bodyChunks[1].pos + new Vector2(0.0f, -rider.bodyChunks[1].rad), 8, 7f, 5f, 5.5f, 40f, new Color(1f, 1f, 1f, 0.5f)));
                    }
                }
            }
        }
    }
}
