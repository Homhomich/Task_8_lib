using System.Threading;

namespace Task_8_lib
{
    internal class Mechanic : IMechanic
    {
        private object Locker { get; } = new object(); // TODO ???

        public void Repair(Conveyor conveyor)
        {
            lock (Locker)
            {
                conveyor.UpdateState(ConveyorState.InRepairing);
                Thread.Sleep(2000);
            }
        }
    }
}