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
			Outputs[0].On = false;
			currTick = Inputs[0].On;
			if (prevTick != currTick) {
				Outputs[0].On = true;
			}
			prevTick = currTick;
		}
	} // Implemented, Not Tested
}
