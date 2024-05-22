using LogicAPI.Server.Components;

namespace CirnosCircuits 
{
    public abstract class BaseMultiplexer : LogicComponent 
    {
        private IOHandler _ioHandler;
        protected abstract int Bits { get; }

        protected override void Initialize()
        {
            _ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() 
        {
            var selector = _ioHandler.GetInputAsI32(Bits);
            Outputs[0].On = Inputs[selector].On;
        }
    }

    public class Multiplexer2Bit : BaseMultiplexer
    {
        protected override int Bits => 2;
    }
    
    public class Multiplexer3Bit : BaseMultiplexer
    {
        protected override int Bits => 3;
    }
    
    public class Multiplexer4Bit : BaseMultiplexer
    {
        protected override int Bits => 4;
    }
    
    public class Multiplexer5Bit : BaseMultiplexer
    {
        protected override int Bits => 5;
    }
}
