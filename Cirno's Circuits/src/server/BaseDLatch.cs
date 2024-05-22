using LogicAPI.Server.Components;

namespace CirnosCircuits
{
    public abstract class BaseDLatch : LogicComponent 
    {
        protected abstract int Bits { get; }
        private bool[] _data;
        private IOHandler _ioHandler;
        
        protected override void Initialize() 
        {
            _data = new bool[Bits];
            _ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() 
        {
            var writeEnable = Inputs[Bits].On;
            
            if (_data == null) 
                return;
                
            if (writeEnable) 
                _data = _ioHandler.GrabBoolArrayFromInput(0, Bits);

            _ioHandler.OutputBoolArray(_data);
        }
    }
}