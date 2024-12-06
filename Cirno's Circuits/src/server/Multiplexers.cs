using LogicAPI.Server.Components;

namespace CirnosCircuits {
    public abstract class BaseMultiplexer: LogicComponent {
        private IOHandler ioHandler;
        protected abstract int NumberOfInputs { get; }

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() {
            int selector = ioHandler.GetInputAs<int>(NumberOfInputs);
            Outputs[0].On = Inputs[selector].On;
        }
    }

    public class Multiplexer2Bit: BaseMultiplexer {
        protected override int NumberOfInputs => 2;
    }
    
    public class Multiplexer3Bit: BaseMultiplexer {
        protected override int NumberOfInputs => 4;
    }
    
    public class Multiplexer4Bit: BaseMultiplexer {
        protected override int NumberOfInputs => 8;
    }
    
    public class Multiplexer5Bit: BaseMultiplexer {
        protected override int NumberOfInputs => 16;
    }
}
