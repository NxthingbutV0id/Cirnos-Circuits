using LogicAPI.Server.Components;

namespace CirnosCircuits {
    public abstract class Relay: LogicComponent {
        protected abstract int Bits { get; }
        private IInputPeg[] inputsA;
        private IInputPeg[] inputsB;
        private bool wasOpen;

        protected override void Initialize() {
            inputsA = new IInputPeg[Bits];
            inputsB = new IInputPeg[Bits];

            for (int i = 0; i < Bits; i++) {
                inputsA[i] = Inputs[i];
                inputsB[i] = Inputs[i + Bits];
            }
        }

        protected override void DoLogicUpdate() {
            bool isOpen = Inputs[Bits << 1].On;
            
            if (wasOpen == isOpen) return;
            if (inputsA == null || inputsB == null) return;
            
            if (isOpen) {
                for (int i = 0; i < Bits; i++) {
                    inputsA[i].AddPhasicLinkWith(inputsB[i]);
                }
            } else {
                for (int i = 0; i < Bits; i++) {
                    inputsA[i].RemovePhasicLinkWith(inputsB[i]);
                }
            }
            wasOpen = isOpen;
        }

        public override bool InputAtIndexShouldTriggerComponentLogicUpdates(int inputIndex) {
            return inputIndex == (Bits << 1);
        }
    }
    
    public class ByteRelay: Relay {
        protected override int Bits => 8;
    } // Needs Testing

    public class WordRelay: Relay {
        protected override int Bits => 16;
    } // Needs Testing

    public class DWordRelay: Relay {
        protected override int Bits => 32;
    } // Not Implemented

    public class QWordRelay: Relay {
        protected override int Bits => 64;
    } // Not Implemented
    
    public abstract class FastMux: LogicComponent {
        protected abstract int Bits { get; }
        private IInputPeg[] inputsA;
        private IInputPeg[] inputsB;
        private IInputPeg[] inputsC;
        private bool wasOpen;

        protected override void Initialize() {
            inputsA = new IInputPeg[Bits];
            inputsB = new IInputPeg[Bits];
            inputsC = new IInputPeg[Bits];

            for (int i = 0; i < Bits; i++) {
                inputsA[i] = Inputs[i];
                inputsB[i] = Inputs[i + Bits];
                inputsC[i] = Inputs[i + (Bits << 1)];
            }
        }

        protected override void DoLogicUpdate() {
            bool isOpen = Inputs[3 * Bits].On;
            
            if (wasOpen == isOpen) return;
            if (inputsA == null || inputsB == null || inputsC == null) return;
            
            if (isOpen) {
                for (int i = 0; i < Bits; i++) {
                    inputsA[i].RemovePhasicLinkWith(inputsC[i]);
                }
                for (int i = 0; i < Bits; i++) {
                    inputsA[i].AddPhasicLinkWith(inputsB[i]);
                }
            } else {
                for (int i = 0; i < Bits; i++) {
                    inputsA[i].RemovePhasicLinkWith(inputsB[i]);
                }
                for (int i = 0; i < Bits; i++) {
                    inputsA[i].AddPhasicLinkWith(inputsC[i]);
                }
            }
            wasOpen = isOpen;
        }

        public override bool InputAtIndexShouldTriggerComponentLogicUpdates(int inputIndex) {
            return inputIndex == 3 * Bits;
        }
    }
}