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

        private const double ProblemChance = 0.69350145;

        private ConveyorState State { get; set; } = ConveyorState.Working;
        
        private Random Random { get; set; } = new Random();

        #endregion

        #region methods
        
        private ConveyorState NextState
        {
            get
            {
                return State switch
                {
                    ConveyorState.AddMaterials => ConveyorState.Working,
                    ConveyorState.Working => Random.NextDouble() > ProblemChance
                        ? (Random.NextDouble() > 0.5 ? ConveyorState.Broken : ConveyorState.NeedMoreMaterials)
                        : ConveyorState.Working,
                    ConveyorState.InRepairing => ConveyorState.Working,
                    _ => ConveyorState.Broken
                };
            }
        }

        internal void StartWorking()
        {
            Thread.Sleep(3000);
            while (true)
            {
                var next = NextState;
                UpdateState(next);
                Thread.Sleep(1000);
                switch (State)
                {
                    case ConveyorState.NeedMoreMaterials:
                        AddMaterials?.Invoke(this);
                        break;
                    case ConveyorState.Broken:
                        Repair?.Invoke(this);
                        break;
                    case ConveyorState.Working:
                        Update?.Invoke(this, State);
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