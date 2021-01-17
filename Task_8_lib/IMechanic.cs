namespace Task_8_lib
{
    internal interface IMechanic
    {
        public bool IsFree { get;  set; }
        void Repair(Conveyor conveyor);
    }
}