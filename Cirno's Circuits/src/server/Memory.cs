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

    public abstract class BaseRam : LogicComponent {
        private IOHandler ioHandler;
        private byte[] data;
        private bool prevClk;
        private bool clk;
        private bool writeEnable;
        private bool reset;
        private bool chipEnable;
        private int address;
        private int clockIndex, writeIndex, resetIndex, chipEnableIndex, total;
        
        protected abstract int addressBits { get; }
        
        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
            prevClk = false;
            writeEnable = false;
            reset = false;
            chipEnable = false;
            total = 1 << addressBits;
            data = new byte[total];
            clockIndex = 8 + addressBits;
            writeIndex = clockIndex + 1;
            resetIndex = writeIndex + 1;
            chipEnableIndex = resetIndex + 1;
        }

        protected override void DoLogicUpdate() {
            var inputData = ioHandler.GetInputAs<byte>();
            address = ioHandler.GetInputAs<int>(8) & (addressBits - 1);
            clk = Inputs[clockIndex].On;
            writeEnable = Inputs[writeIndex].On;
            reset = Inputs[resetIndex].On;
            chipEnable = Inputs[chipEnableIndex].On;
            var risingEdge = !prevClk && clk;
            
            if (!chipEnable) {
                ioHandler.ClearOutputs();
                prevClk = clk;
                return;
            }
            
            if (risingEdge) {
                if (reset) { for (var i = 0; i < total; i++) { data[i] = 0; } }
                if (writeEnable) { data[address] = inputData; }
            }
            
            ioHandler.ClearOutputs();
            ioHandler.OutputNumber(data[address]);
            prevClk = clk;
        }
    }
    
    public class RAM256 : BaseRam {
        protected override int addressBits => 8;
    }
    
    public class RAM1k : BaseRam {
        protected override int addressBits => 10;
    }

    public class RAM2k : BaseRam {
        protected override int addressBits => 11;
    }
    
    public class RAM4k : BaseRam {
        protected override int addressBits => 12;
    }
    
    public class RAM8k : BaseRam {
        protected override int addressBits => 13;
    }
    
    public class RAM16k : BaseRam {
        protected override int addressBits => 14;
    }
    
    public class RAM32k : BaseRam {
        protected override int addressBits => 15;
    }

    public class RAM64k : BaseRam {
        protected override int addressBits => 16;
    }
}
