using System.Threading;

namespace Task_8_lib
{
    internal class Mechanic : IMechanic
    {
        private object Locker { get; } = new object();
        public bool IsFree { get; set; } = true;
        public void Repair(Conveyor conveyor)
        {
            lock (Locker)
            {
                IsFree = false;
                conveyor.UpdateState(ConveyorState.InRepairing);
                Thread.Sleep(2000);
                IsFree = true;
            }
        }
    }
}