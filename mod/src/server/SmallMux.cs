using LogicAPI.Server.Components;
using LogicAPI.Server;
using LogicLog;
using LogicWorld.Server.Circuitry;

namespace CirnosCircuits {
    public class SmallMux : LogicComponent {
        protected override void DoLogicUpdate() {
            var select = Inputs[2].On ? 1 : 0;
            Outputs[0].On = Inputs[select].On;
        }
    }
}