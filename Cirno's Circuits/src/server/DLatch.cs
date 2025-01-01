using LogicAPI.Server.Components;

namespace CirnosCircuits {
    public abstract class DLatch: LogicComponent {
        protected abstract int Bits { get; }
        private bool[] data;
        private IOHandler ioHandler;
        
        protected override void Initialize() {
            data = new bool[Bits];
            ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() {
            bool writeEnable = Inputs[Bits].On;

            if (data == null) return;

            if (writeEnable) {
                data = ioHandler.GrabBoolArrayFromInput(0, Bits);
            }

            ioHandler.OutputBoolArray(data);
        }
    }
    
    public class ByteDLatch: DLatch {
        protected override int Bits => 8;
    }

    public class WordDLatch: DLatch {
        protected override int Bits => 16;
    }

    public class DWordDLatch: DLatch {
        protected override int Bits => 32;
    } // Not Implemented

    public class QWordDLatch: DLatch {
        protected override int Bits => 64;
    } // Not Implemented
}