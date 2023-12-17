using LogicAPI.Server.Components;

namespace CirnosCircuits {
	public class PulseExtender : LogicComponent {
		protected override void DoLogicUpdate() {
			//TODO
		}
	}

	public class EdgeDetector : LogicComponent {
		private bool prevTick, currTick;
		protected override void Initialize() {
			prevTick = false;
		}

		protected override void DoLogicUpdate() {
			currTick = Inputs[0].On;
			Outputs[0].On = !(prevTick ^ currTick);
			prevTick = currTick;
		}
    } // Implemented, Not Tested
}
