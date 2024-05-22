using LogicAPI.Server.Components;

namespace CirnosCircuits 
{
    public class ByteComparator : LogicComponent 
    {
        private IOHandler _ioHandler;

        protected override void Initialize()
        {
            _ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() 
        {
            var signed = Inputs[16].On;

            if (signed)
            {
                var a = _ioHandler.GetInputAsI8();
                var b = _ioHandler.GetInputAsI8(8);
                
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            } 
            else 
            {
                var a = _ioHandler.GetInputAsU8();
                var b = _ioHandler.GetInputAsU8(8);
                
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            }
        }
    }
    
    public class WordComparator : LogicComponent 
    {
        private IOHandler _ioHandler;

        protected override void Initialize()
        {
            _ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() 
        {
            var signed = Inputs[32].On;

            if (signed)
            {
                var a = _ioHandler.GetInputAsI16();
                var b = _ioHandler.GetInputAsI16(16);
                
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            } 
            else 
            {
                var a = _ioHandler.GetInputAsU16();
                var b = _ioHandler.GetInputAsU16(16);
                
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            }
        }
    }
    
    public class DWordComparator : LogicComponent 
    {
        private IOHandler _ioHandler;

        protected override void Initialize()
        {
            _ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() 
        {
            var signed = Inputs[64].On;

            if (signed)
            {
                var a = _ioHandler.GetInputAsI32();
                var b = _ioHandler.GetInputAsI32(32);
                
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            } 
            else 
            {
                var a = _ioHandler.GetInputAsU32();
                var b = _ioHandler.GetInputAsU32(32);
                
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            }
        }
    }
    
    public class QWordComparator : LogicComponent 
    {
        private IOHandler _ioHandler;

        protected override void Initialize()
        {
            _ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() 
        {
            var signed = Inputs[128].On;

            if (signed)
            {
                var a = _ioHandler.GetInputAsI64();
                var b = _ioHandler.GetInputAsI64(64);
                
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            } 
            else 
            {
                var a = _ioHandler.GetInputAsU64(0);
                var b = _ioHandler.GetInputAsU64(64);
                
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            }
        }
    }
}
