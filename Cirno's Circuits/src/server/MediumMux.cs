using LogicAPI.Server.Components;
using LogicAPI.Server;
using LogicLog;
using LogicWorld.Server.Circuitry;

namespace CirnosCircuits {
    public class MediumMux : LogicComponent {
        protected override void DoLogicUpdate() {
            int selecter = (boolToInt(Inputs[1].On) << 1) + boolToInt(Inputs[0].On);
            Outputs[0].On = Inputs[selecter + 2].On;
        }

        private int boolToInt(bool input) {
            return input ? 1 : 0;
        }
    }
}