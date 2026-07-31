// Copyright © Connor deBoer (MQG) 2026, All Rights Reserved

namespace HickeryDickery.Player
{
    public interface ITimeTraveler
    {
        public History Start { get; }
        bool IsAtStart();
        public void RecordHistory();
        public void TravelThroughHistory();
    }
}