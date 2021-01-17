using System.Collections.Generic;
using System.Threading.Tasks;

/*
Конвейер с деталями – смоделировать работу конвейера производства деталей.
Реализовать классы – Конвейер, Погрузчик, интерфейс – Механик.
События – в конвейере закончились материалы – погрузчик загружает новую партию,
конвейер сломался (с некоторой долей вероятности) – механик чинит конвейер.
 */

namespace Task_8_lib
{
    public class MainManager
    {
        
        #region delegates

        public delegate void ViewUpdateHandler(int pos, ConveyorState state, bool isLoaderFree, bool isMechanicFree);
        public delegate void ConveyorAddHandler(Conveyor conveyor);

        #endregion

        #region events

        public event ViewUpdateHandler UpdateView;
        public event ConveyorAddHandler ConveyorAdded;

        #endregion

        #region properties

        private List<Conveyor> Conveyors { get; } = new List<Conveyor>();
        private Loader Loader { get; } = new Loader();
        
        private IMechanic Mechanic { get; } = new Mechanic();
        
        #endregion

        #region methods

        public void Start()
        {
            AddNewConveyor();
        }

        public async void AddNewConveyor()
        {
            var conveyor = new Conveyor();
            conveyor.AddMaterials += HandleAddMaterials;
            conveyor.Repair += HandleRepair;
            conveyor.Update += HandleUpdate;
            Conveyors.Add(conveyor);
            ConveyorAdded?.Invoke(conveyor);
            await Task.Run(conveyor.StartWorking);
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
            var pos = Conveyors.FindIndex(conveyor => conveyor == caller);
            UpdateView?.Invoke(pos, state, Loader.IsFree, Mechanic.IsFree);
        }

        #endregion
    }
}