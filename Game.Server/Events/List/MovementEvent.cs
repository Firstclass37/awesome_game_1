using Game.Server.Models.Maps;
using System;

namespace Game.Server.Events.List
{
    public class MovementEvent
    {
        public int MovementId { get; set; }

        public int ObjectId { get; set; }

        public Type ObjectType { get; set; }

        public Coordiante To { get; set; }
    }
}