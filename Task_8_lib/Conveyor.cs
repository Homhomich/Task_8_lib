using System;
using System.Threading;

namespace Task_8_lib
{
    public class Conveyor
    {
        #region delegates
        
        internal delegate void AddMaterialsHandler(Conveyor caller);

        internal delegate void RepairHandler(Conveyor caller);

        internal delegate void UpdateHandler(Conveyor caller, ConveyorState state);

        #endregion

        #region events

        internal event AddMaterialsHandler AddMaterials;
        internal event RepairHandler Repair;
        internal event UpdateHandler Update;

        #endregion

        #region properties

        private const double ProblemChance = 0.39350145;

        internal ConveyorState State { get; private set; } = ConveyorState.NeedMoreMaterials;

        private ConveyorState NextState
        {
            get
            {
                return State switch
                {
                    ConveyorState.NeedMoreMaterials => ConveyorState.AddMaterials,
                    ConveyorState.AddMaterials => ConveyorState.Working,
                    ConveyorState.Working => new Random().NextDouble() > ProblemChance
                        ? ConveyorState.Broken
                        : ConveyorState.Working,
                    ConveyorState.Broken => ConveyorState.InRepairing,
                    ConveyorState.InRepairing => ConveyorState.Working,
                    _ => ConveyorState.Broken
                };
            }
        }

        #endregion

        #region methods

        internal void StartWorking()
        {
            Thread.Sleep(3000);
            while (true)
            {
                var next = NextState;
                UpdateState(next);
                switch (State)
                {
                    case ConveyorState.NeedMoreMaterials:
                        Thread.Sleep(4000);
                        AddMaterials?.Invoke(this);
                        break;
                    case ConveyorState.Broken:
                        Thread.Sleep(3500);
                        Repair?.Invoke(this);
                        break;
                }
            }
        }

        internal void UpdateState(ConveyorState nextState)
        {
            State = nextState;
            Update?.Invoke(this, State);
        }

        #endregion
    }
}