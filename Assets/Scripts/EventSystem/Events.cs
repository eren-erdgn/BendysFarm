using UnityEngine;

namespace EventSystem
{
    public static class Events
    {
        public static readonly EventActions OnPlayerWaterPlant = new EventActions();
        public static readonly  EventActions <bool> OnPlayerAtWaterWell = new EventActions<bool>();
        public static readonly EventActions OnPlayerGetWater = new EventActions();
        public static readonly EventActions <bool>OnPlayerCanHarvest = new EventActions<bool>();
        public static readonly EventActions <bool>OnPlayerCanSeed = new EventActions<bool>();
        public static readonly EventActions OnPlayerCountChanged = new EventActions();
        public static readonly EventActions <bool>OnPlayerAtClipboard = new EventActions<bool>();
        
    }
}
