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
                sbyte a = ioHandler.GetInputAs<sbyte>();
                sbyte b = ioHandler.GetInputAs<sbyte>(8);
                
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            } else {
                byte a = ioHandler.GetInputAs<byte>();
                byte b = ioHandler.GetInputAs<byte>(8);
                
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
                short a = ioHandler.GetInputAs<short>();
                short b = ioHandler.GetInputAs<short>(16);
                
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            } else {
                ushort a = ioHandler.GetInputAs<ushort>();
                ushort b = ioHandler.GetInputAs<ushort>(16);
                
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
                int a = ioHandler.GetInputAs<int>();
                int b = ioHandler.GetInputAs<int>(32);
                
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            } else {
                uint a = ioHandler.GetInputAs<uint>();
                uint b = ioHandler.GetInputAs<uint>(32);
                
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
                long a = ioHandler.GetInputAs<long>();
                long b = ioHandler.GetInputAs<long>(64);
                
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            } else {
                ulong a = ioHandler.GetInputAs<ulong>();
                ulong b = ioHandler.GetInputAs<ulong>(64);
                
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            }
        }
    }
}
