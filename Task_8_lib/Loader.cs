using System.Threading;

namespace Task_8_lib
{
    public class Loader
    {
        public bool IsFree { get; private set; } = true;
        internal void AddMoreMaterials(Conveyor conveyor)
        {
            IsFree = false;
            conveyor.UpdateState(ConveyorState.AddMaterials);
            Thread.Sleep(1000);
            IsFree = true;
        }
    }
}