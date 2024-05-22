using LogicAPI.Server.Components;

namespace CirnosCircuits
{
    public class ByteAdder : LogicComponent
    {
        private IOHandler _ioHandler;

        protected override void Initialize()
        {
            _ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() 
        {
            var carryIn = Inputs[16].On;

            var a = _ioHandler.GetInputAsU8();
            var b = _ioHandler.GetInputAsU8(8);
            var result = carryIn ? a + b + 1 : a + b;
            
            _ioHandler.OutputNumber(result);
        }
    }
    
    public class WordAdder : LogicComponent 
    {
        private IOHandler _ioHandler;

        protected override void Initialize()
        {
            _ioHandler = new IOHandler(Inputs, Outputs);
        }
        
        protected override void DoLogicUpdate() 
        {
            var carryIn = Inputs[32].On;

            var a = _ioHandler.GetInputAsI16();
            var b = _ioHandler.GetInputAsI16(16);
            var result = carryIn ? a + b + 1 : a + b;
            
            _ioHandler.OutputNumber(result);
        }
    }
    
    public class ByteSubtract : LogicComponent 
    {
        private IOHandler _ioHandler;

        protected override void Initialize()
        {
            _ioHandler = new IOHandler(Inputs, Outputs);
        }
        
        protected override void DoLogicUpdate() 
        {
            var borrowIn = Inputs[16].On;

            var a = _ioHandler.GetInputAsI8();
            var b = _ioHandler.GetInputAsI8(8);
            var result = borrowIn ? a - b - 1 : a - b;
            
            _ioHandler.OutputNumber(result);
        }
    }
    
    public class WordSubtract : LogicComponent 
    {
        private IOHandler _ioHandler;

        protected override void Initialize()
        {
            _ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() 
        {
            var borrowIn = Inputs[32].On;

            var a = _ioHandler.GetInputAsI16();
            var b = _ioHandler.GetInputAsI16(16);
            var result = borrowIn ? a - b - 1 : a - b;
            
            _ioHandler.OutputNumber(result);
        }
    }
};