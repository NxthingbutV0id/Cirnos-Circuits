using LogicAPI.Server.Components;

namespace CirnosCircuits {
    public abstract class Register : LogicComponent {
        protected abstract int Bits { get; }
        private IOHandler ioHandler;
        private bool[] data;
        private bool prevClk;
        private bool clk;
        private bool enable;
        private bool reset;
        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
            prevClk = false;
            enable = false;
            reset = false;
            data = new bool[Bits];
        }

        protected override void DoLogicUpdate() {
            ioHandler.ClearOutputs();
            clk = Inputs[Bits].On;
            enable = Inputs[Bits + 1].On;
            reset = Inputs[Bits + 2].On;
            
            bool risingEdge = !prevClk && clk;

            if (reset && risingEdge) {
                for (int i = 0; i < Bits; i++) {
                    data[i] = false;
                }
            }
            
            if (enable && risingEdge) {
                data = ioHandler.GrabBoolArrayFromInput(0, Bits);
            }

            prevClk = clk;
            ioHandler.OutputBoolArray(data);
        }
    }
    
    public class ByteRegister : Register {
        protected override int Bits => 8;
    }
    
    public class WordRegister : Register {
        protected override int Bits => 16;
    }
    
    public class DWordRegister : Register {
        protected override int Bits => 32;
    }
    
    public class QWordRegister : Register {
        protected override int Bits => 64;
    }

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
            reset = Inputs[Bits + 2].On;
            read1 = Inputs[Bits + 3].On;
            read2 = Inputs[Bits + 4].On;
            
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
    }
    
    public class WordDRR : DualReadRegister {
        protected override int Bits => 16;
    }
    
    public class DWordDRR : DualReadRegister {
        protected override int Bits => 32;
    }
    
    public class RAM256 : LogicComponent {
        private IOHandler ioHandler;
        private byte[] data;
        private bool prevClk;
        private bool clk;
        private bool writeEnable;
        private bool reset;
        private int address;
        
        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
            prevClk = false;
            writeEnable = false;
            reset = false;
            data = new byte[256];
        }

        protected override void DoLogicUpdate() {
            var inputData = ioHandler.GetInputAs<byte>();
            address = ioHandler.GetInputAs<int>(8) & 0xFF;
            clk = Inputs[16].On;
            writeEnable = Inputs[17].On;
            reset = Inputs[18].On;
            var risingEdge = !prevClk && clk;
            if (risingEdge) {
                if (reset) { for (var i = 0; i < 256; i++) { data[i] = 0; } }
                if (writeEnable) { data[address] = inputData; }
            }
            ioHandler.ClearOutputs();
            ioHandler.OutputNumber(data[address]);
            prevClk = clk;
        }
    }
}
