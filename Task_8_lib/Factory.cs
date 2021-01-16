using System;
using System.Threading.Tasks;


namespace Task_8_lib
{
    public class Factory
    {
        
        #region delegates

        public delegate void ViewUpdateHandler(Conveyor conveyor, ConveyorState state);

        #endregion

        #region events

        public event ViewUpdateHandler UpdateView;

        #endregion

        #region properties

        private Conveyor Conveyor { get; } = new Conveyor();
        
        private Loader Loader { get; } = new Loader();
        
        private IMechanic Mechanic { get; } = new Mechanic();
        
        #endregion

        #region methods

        public async void Start()
        {
            Conveyor.AddMaterials += HandleAddMaterials;
            Conveyor.Repair += HandleRepair;
            Conveyor.Update += HandleUpdate;
            await Task.Run(() => Conveyor.StartWorking());
        }

        private void HandleAddMaterials(Conveyor caller)
        {
            Loader.AddMoreMaterials(caller);
        }

        private void HandleRepair(Conveyor caller)
        {
           Mechanic.Repair(caller);
        }

        private void HandleUpdate(Conveyor caller, ConveyorState state)
        {
            UpdateView?.Invoke(caller, state);
        }

        #endregion
    }
}