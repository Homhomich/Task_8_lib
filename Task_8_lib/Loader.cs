using System.Threading;

namespace Task_8_lib
{
    public class Loader
    {
        internal void AddMoreMaterials(Conveyor conveyor)
        {
            conveyor.UpdateState(ConveyorState.AddMaterials);
            Thread.Sleep(1000);
        }
    }
}