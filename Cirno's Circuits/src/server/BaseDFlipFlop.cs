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
}
