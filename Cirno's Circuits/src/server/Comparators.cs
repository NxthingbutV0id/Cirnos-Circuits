using LogicAPI.Server.Components;

namespace CirnosCircuits {
    public class ByteComparator: LogicComponent {
        private IOHandler ioHandler;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() {
            bool signed = Inputs[16].On;

            if (signed) {
                sbyte a = ioHandler.GetInputAsI8();
                sbyte b = ioHandler.GetInputAsI8(8);
                
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            } else {
                byte a = ioHandler.GetInputAsU8();
                byte b = ioHandler.GetInputAsU8(8);
                
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            }
        }
    }
    
    public class WordComparator: LogicComponent {
        private IOHandler ioHandler;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() {
            bool signed = Inputs[32].On;

            if (signed) {
                short a = ioHandler.GetInputAsI16();
                short b = ioHandler.GetInputAsI16(16);
                
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            } else {
                ushort a = ioHandler.GetInputAsU16();
                ushort b = ioHandler.GetInputAsU16(16);
                
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            }
        }
    }
    
    public class DWordComparator: LogicComponent {
        private IOHandler ioHandler;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() {
            bool signed = Inputs[64].On;

            if (signed) {
                int a = ioHandler.GetInputAsI32();
                int b = ioHandler.GetInputAsI32(32);
                
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            } else {
                uint a = ioHandler.GetInputAsU32();
                uint b = ioHandler.GetInputAsU32(32);
                
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            }
        }
    }
    
    public class QWordComparator: LogicComponent {
        private IOHandler ioHandler;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() {
            bool signed = Inputs[128].On;

            if (signed) {
                long a = ioHandler.GetInputAsI64();
                long b = ioHandler.GetInputAsI64(64);
                
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            } else {
                ulong a = ioHandler.GetInputAsU64();
                ulong b = ioHandler.GetInputAsU64(64);
                
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            }
        }
    }
}
