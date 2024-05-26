using LogicAPI.Server.Components;

namespace CirnosCircuits {
    public class Decoder: LogicComponent {
        private IOHandler ioHandler;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() {
            ioHandler.ClearOutputs();
            int index = ioHandler.GetInputAsI32();
            Outputs[index].On = true;
        }
    }
}
