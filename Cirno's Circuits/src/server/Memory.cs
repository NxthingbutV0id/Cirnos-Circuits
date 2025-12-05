using LogicAPI.Server.Components;

namespace CirnosCircuits {
    public abstract class Register : LogicComponent {
        protected abstract int Bits { get; }
        private bool[] data;
        private bool prevClk;
        private bool clk;
        private bool enable;
        private bool reset;
        protected override void Initialize() {
            prevClk = false;
            enable = false;
            reset = false;
            data = new bool[Bits];
        }

        protected override void DoLogicUpdate() {
            clk = Inputs[Bits].On;
            enable = Inputs[Bits + 1].On;
            reset = Inputs[Bits + 2].On;

            if (!prevClk && clk && enable && !reset) {
                for (var i = 0; i < Bits; i++) {
                    data[i] = Inputs[i].On;
                }
            }
            
            if (reset) {
                for (var i = 0; i < Bits; i++) {
                    data[i] = false;
                }
            }
            
            for (var i = 0; i < Bits; i++) {
                Outputs[i].On = data[i];
            }

            prevClk = clk;
        }
    }
    
    public class BitRegister : LogicComponent {
        private bool data;
        private bool prevClk;
        private bool clk;
        private bool enable;
        private bool reset;
        protected override void Initialize() {
            prevClk = false;
            enable = false;
            data = false;
            reset = false;
        }

        protected override void DoLogicUpdate() {
            clk = Inputs[1].On;
            enable = Inputs[2].On;
            reset = Inputs[3].On;
            if (!prevClk && clk && enable) {
                data = Inputs[0].On;
            }

            if (reset) {
                data = false;
            }

            Outputs[0].On = data;
            prevClk = clk;
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
            
            if (enable && !prevClk && clk) {
                for (var i = 0; i < Bits; i++) {
                    data[i] = Inputs[i].On;
                }
            }

            if (read1) {
                for (var i = 0; i < Bits; i++) {
                    Outputs[i].On = data[i];
                }
            }
            
            if (read2) {
                for (var i = 0; i < Bits; i++) {
                    Outputs[i + Bits].On = data[i];
                }
            }
            
            if (reset) {
                for (var i = 0; i < Bits; i++) {
                    data[i] = false;
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
    
    public class DualReadRegisterFileBATPU : LogicComponent {
        private IOHandler ioHandler;
        private bool clk, prevClk, reset, writeEnable;
        private byte[] registers;
        private int rs1, rs2, rd;
        
        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
            prevClk = false;
            reset = false;
            writeEnable = false;
            registers = new byte[16];
            rs1 = 0;
            rs2 = 0;
            rd = 0;
        }

        protected override void DoLogicUpdate() {
            var dataIn = ioHandler.GetInputAs<byte>();
            rs1 = ioHandler.GetInputAs<byte>(8) & 0xf;
            rs2 = ioHandler.GetInputAs<byte>(12) & 0xf;
            rd = ioHandler.GetInputAs<byte>(16) & 0xf;
            clk = Inputs[20].On;
            writeEnable = Inputs[21].On;
            reset = Inputs[22].On;
            if (!prevClk && clk && writeEnable) {
                registers[rd] = dataIn;
            }
            
            ioHandler.ClearOutputs();
            ioHandler.OutputNumber(registers[rs1]);
            ioHandler.OutputNumber(registers[rs2], 8);
            
            if (reset) {
                Reset();
            }
        }
        
        private void Reset() {
            for (var i = 0; i < registers.Length; i++) {
                registers[i] = 0;
            }
        }
    }

    public class DualReadRegisterFileRISCV : LogicComponent {
        private IOHandler ioHandler;
        private bool clk, prevClk, reset, writeEnable;
        private int[] registers;
        private int rs1, rs2, rd;
        
        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
            prevClk = false;
            reset = false;
            writeEnable = false;
            registers = new int[32];
            rs1 = 0;
            rs2 = 0;
            rd = 0;
        }

        protected override void DoLogicUpdate() {
            var dataIn = ioHandler.GetInputAs<int>();
            rs1 = ioHandler.GetInputAs<byte>(32) & 0x1f;
            rs2 = ioHandler.GetInputAs<byte>(37) & 0x1f;
            rd = ioHandler.GetInputAs<byte>(42) & 0xf;
            clk = Inputs[47].On;
            writeEnable = Inputs[48].On;
            reset = Inputs[49].On;
            if (!prevClk && clk && writeEnable) {
                registers[rd] = dataIn;
            }
            
            ioHandler.ClearOutputs();
            ioHandler.OutputNumber(registers[rs1]);
            ioHandler.OutputNumber(registers[rs2], 32);
            
            if (reset) {
                Reset();
            }
        }
        
        private void Reset() {
            for (var i = 0; i < registers.Length; i++) {
                registers[i] = 0;
            }
        }
    }

    public class MultiByteRam : LogicComponent {
        private IOHandler ioHandler;
        private byte[] data;
        private bool prevClk, clk, writeEnable, readEnable, reset, chipEnable;
        private int address, dataType, clockIndex, writeIndex, resetIndex, chipEnableIndex, total;
        private const int dataBits = 32;
        private const int dataTypeBits = 2;
        private const int addressBits = 16;
        private const int Byte = 0;
        private const int Word = 1;
        private const int DWord = 2;
        
        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
            prevClk = false;
            writeEnable = false;
            reset = false;
            chipEnable = false;
            total = 1 << addressBits;
            data = new byte[total];
            clockIndex = dataBits + dataTypeBits + addressBits;
            writeIndex = clockIndex + 1;
            resetIndex = writeIndex + 1;
            chipEnableIndex = resetIndex + 1;
            dataType = 0;
        }

        protected override void DoLogicUpdate() {
            var inputData = ioHandler.GetInputAs<int>();
            address = ioHandler.GetInputAs<int>(32) & 0xffff;
            dataType = ioHandler.GetInputAs<int>(48) & 0x3;

            if (dataType == 3) {
                dataType = 2;
            }

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
            
            if (risingEdge && writeEnable) {
                switch (dataType) {
                    case Byte: data[address] = (byte)(inputData & 0xff); break;
                    case Word:
                        data[address] = (byte)((inputData >> 8) & 0xff);
                        data[address + 1] = (byte)(inputData & 0xff);
                        break;
                    case DWord:
                        data[address] = (byte)((inputData >> 24) & 0xff);
                        data[address + 1] = (byte)((inputData >> 16) & 0xff);
                        data[address + 2] = (byte)((inputData >> 8) & 0xff);
                        data[address + 3] = (byte)(inputData & 0xff);
                        break;
                }
            }

            if (readEnable) {
                ioHandler.ClearOutputs();
                var outputData = dataType switch {
                    Byte => data[address],
                    Word => (data[address] << 8) | data[address + 1],
                    DWord => (data[address] << 24) | (data[address + 1] << 16) | (data[address + 2] << 8) | data[address + 3],
                    _ => 0
                };
                ioHandler.OutputNumber(outputData);
            }

            if (reset) { for (var i = 0; i < total; i++) { data[i] = 0; } }
            
            prevClk = clk;
        }
    }
}
