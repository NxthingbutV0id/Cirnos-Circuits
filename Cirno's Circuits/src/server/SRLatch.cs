using LogicAPI.Server.Components;

namespace CirnosCircuits {
    public class SRLatch: LogicComponent {
        private bool set;
        private bool reset;
        private bool q;
        private bool qBar;

        protected override void Initialize() {
            q = false;
            qBar = true;
        }

        protected override void DoLogicUpdate() {
            set = Inputs[0].On; 
            reset = Inputs[1].On;

            for (int i = 0; i < 3; ++i) {
                qBar = !(set || q);
                q = !(reset || qBar);
            }

            Outputs[0].On = q;
            Outputs[1].On = qBar;
        }
    } // Completed
}