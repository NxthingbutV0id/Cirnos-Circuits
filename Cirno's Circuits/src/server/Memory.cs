using LogicAPI.Server.Components;

namespace CirnosCircuits {
    public abstract class DualReadRegister : LogicComponent {
        protected abstract int Bits { get; }
        private IOHandler ioHandler;
        private bool[] data;
        private bool prevClk;
        private bool clk;
        private bool enable;
        private bool read1;
        private bool read2;
        private bool reset;
        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
            prevClk = false;
            enable = false;
            read1 = false;
            read2 = false;
            reset = false;
            data = new bool[Bits];
        }

        protected override void DoLogicUpdate() {
            ioHandler.ClearOutputs();
            clk = Inputs[Bits].On;
            enable = Inputs[Bits + 1].On;
            read1 = Inputs[Bits + 2].On;
            read2 = Inputs[Bits + 3].On;
            reset = Inputs[Bits + 4].On;
            
            bool risingEdge = !prevClk && clk;

            if (reset && risingEdge) {
                for (int i = 0; i < Bits; i++) {
                    data[i] = false;
                }
            }

            if (read1) {
                for (int i = 0; i < Bits; i++) {
                    Outputs[i].On = data[i];
                }
            }
            
            if (read2) {
                for (int i = 0; i < Bits; i++) {
                    Outputs[i + Bits].On = data[i];
                }
            }
            
            if (enable && risingEdge) {
                for (int i = 0; i < Bits; i++) {
                    data[i] = Inputs[i].On;
                }
            }

            prevClk = clk;
        }
    }

    public class ByteDRR : DualReadRegister {
        protected override int Bits => 8;
    } // Not Implemented
    
    public class WordDRR : DualReadRegister {
        protected override int Bits => 16;
    } // Not Implemented
    
    public class DWordDRR : DualReadRegister {
        protected override int Bits => 32;
    } // Not Implemented
}
