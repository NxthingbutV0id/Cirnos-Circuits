using LogicAPI.Server.Components;

namespace CirnosCircuits 
{
    public class Decoder : LogicComponent 
    {
        private IOHandler _ioHandler;

        protected override void Initialize()
        {
            _ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() 
        {
            _ioHandler.ClearOutputs();
            var index = _ioHandler.GetInputAsI32();
            Outputs[index].On = true;
        }
    }
}
