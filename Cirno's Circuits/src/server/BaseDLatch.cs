using LogicAPI.Server.Components;

namespace CirnosCircuits {
    public abstract class BaseDLatch: LogicComponent {
        protected abstract int Bits { get; }
        private bool[] data;
        private IOHandler ioHandler;
        
        protected override void Initialize() 
        {
            data = new bool[Bits];
            ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() 
        {
            bool writeEnable = Inputs[Bits].On;

            if (data == null) return;

            if (writeEnable) {
                data = ioHandler.GrabBoolArrayFromInput(0, Bits);
            }

            ioHandler.OutputBoolArray(data);
        }
    }
}