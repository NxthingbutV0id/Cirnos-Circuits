using LogicAPI.Server.Components;

namespace CirnosCircuits {
    public abstract class BaseDFlipFlop: LogicComponent {
        protected abstract int Bits { get; }
        private bool prevClk;
        private bool clk;
        protected override void Initialize() {
            prevClk = false;
        }

        protected override void DoLogicUpdate() {
            clk = Inputs[Outputs.Count].On;

            if (!prevClk && clk) {
                for (int i = 0; i < Bits; i++) {
                    Outputs[i].On = Inputs[i].On;
                }
            }

            prevClk = clk;
        }
    }
    
    public class DFlipFlop: BaseDFlipFlop {
        protected override int Bits => 1;
    }

    public class ByteDFlipFlop: BaseDFlipFlop {
        protected override int Bits => 8;
    }

    public class WordDFlipFlop: BaseDFlipFlop {
        protected override int Bits => 16;
    }

    public class DWordDFlipFlop: BaseDFlipFlop {
        protected override int Bits => 32;
    }

    public class QWordDFlipFlop: BaseDFlipFlop {
        protected override int Bits => 64;
    } // Not Implemented
}
