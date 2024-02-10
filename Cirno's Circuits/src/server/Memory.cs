using LogicAPI.Server.Components;

namespace CirnosCircuits {
    public class SRLatch : LogicComponent {
        private bool S, R, Q, Qbar;

        protected override void Initialize() {
            Q = false;
            Qbar = true;
        }

        protected override void DoLogicUpdate() {
            S = Inputs[0].On; R = Inputs[1].On;

            for (int i = 0; i < 3; ++i) {
                Qbar = !(S || Q);
                Q = !(R || Qbar);
            }

            Outputs[0].On = Q;
            Outputs[1].On = Qbar;
        }
    } // Completed

    public class JKFlipFlop : LogicComponent {
        private bool J, K, CLK, previousCLK;

        protected override void Initialize() {
            Outputs[0].On = false; //  Q
            Outputs[1].On = true;  // !Q
            previousCLK = false;
        }

        protected override void DoLogicUpdate() {
            J = Inputs[0].On;
            K = Inputs[1].On;
            CLK = Inputs[2].On;
            if (!previousCLK && CLK) {
                if (J && K) {
                    Outputs[0].On = !Outputs[0].On;
                    Outputs[1].On = !Outputs[1].On;
                    previousCLK = CLK;
                    return;
                }
                if (!J && K) {
                    Outputs[0].On = false;
                    Outputs[1].On = true;
                    previousCLK = CLK;
                    return;
                }
                if (J && !K) {
                    Outputs[0].On = true;
                    Outputs[1].On = false;
                    previousCLK = CLK;
                    return;
                }
            }
            previousCLK = CLK;
        }
    } // Completed

    public class ByteDLatch : BaseDLatch {
        public override int Bits => 8;
    } // Needs testing

    public class WordDLatch : BaseDLatch {
        public override int Bits => 16;
    } // Not Implemented

    public class DWordDLatch : BaseDLatch {
        public override int Bits => 32;
    } // Not Implemented

    public class QWordDLatch : BaseDLatch {
        public override int Bits => 64;
    } // Not Implemented

    public class DFlipFlop : BaseDFlipFlop {
        public override int Bits => 1;
    } // Needs testing

    public class ByteDFlipFlop : BaseDFlipFlop {
        public override int Bits => 8;
    } // Needs testing

    public class WordDFlipFlop : BaseDFlipFlop {
        public override int Bits => 16;
    } // Needs testing

    public class DWordDFlipFlop : BaseDFlipFlop {
        public override int Bits => 32;
    } // Not Implemented

    public class QWordDFlipFlop : BaseDFlipFlop {
        public override int Bits => 64;
    } // Not Implemented
}
