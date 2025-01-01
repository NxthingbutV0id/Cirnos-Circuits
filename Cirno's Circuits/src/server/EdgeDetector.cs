using LogicAPI.Server.Components;

namespace CirnosCircuits {
	public class EdgeDetector: LogicComponent {
		private bool prevTick;
		protected override void Initialize() {
			prevTick = false;
		}

		protected override void DoLogicUpdate() {
			Outputs[0].On = prevTick != Inputs[0].On;
			
			if (prevTick != Inputs[0].On) QueueLogicUpdate();
			
            prevTick = Inputs[0].On;
        }
	}
}
