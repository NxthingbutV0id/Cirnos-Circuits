using LogicAPI.Server.Components;

namespace CirnosCircuits {
    public class JKFlipFlop: LogicComponent {
        private bool j;
        private bool k;
        private bool clock;
        private bool prevClock;

        protected override void Initialize() {
            Outputs[0].On = false; //  Q
            Outputs[1].On = true;  // !Q
            prevClock = false;
        }

        protected override void DoLogicUpdate() {
            j = Inputs[0].On;
            k = Inputs[1].On;
            clock = Inputs[2].On;
            if (!prevClock && clock) {
                if (j && k) {
                    Outputs[0].On = !Outputs[0].On;
                    Outputs[1].On = !Outputs[1].On;
                    prevClock = clock;
                    return;
                }
                if (!j && k) {
                    Outputs[0].On = false;
                    Outputs[1].On = true;
                    prevClock = clock;
                    return;
                }
                if (j && !k) {
                    Outputs[0].On = true;
                    Outputs[1].On = false;
                    prevClock = clock;
                    return;
                }
            }
            prevClock = clock;
        }
    } // Completed
}