using LogicAPI.Server.Components;

namespace CirnosCircuits {
    public abstract class DeMux: LogicComponent {
        private IOHandler ioHandler;
        protected abstract int NumberOfOutputs { get; }

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() {
            int selector = ioHandler.GetInputAs<int>(1);
            ioHandler.ClearOutputs();
            Outputs[selector].On = Inputs[0].On;
        }
    }

    public class DeMux1Bit: DeMux {
        protected override int NumberOfOutputs => 2;
    }
    
    public class DeMux2Bit: DeMux {
        protected override int NumberOfOutputs => 4;
    }
    
    public class DeMux3Bit: DeMux {
        protected override int NumberOfOutputs => 8;
    }
    
    public class DeMux4Bit: DeMux {
        protected override int NumberOfOutputs => 16;
    }
    
    public class DeMux8Bit: DeMux {
        protected override int NumberOfOutputs => 256;
    }
    
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
