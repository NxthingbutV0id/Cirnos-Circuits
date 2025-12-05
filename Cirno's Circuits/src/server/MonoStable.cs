using LogicAPI.Server.Components;

namespace CirnosCircuits {
	// One tick pulse on rising edge
	public class RisingEdgeMono: LogicComponent {
		private bool prevTick;
		protected override void Initialize() {
			prevTick = false;
		}

		protected override void DoLogicUpdate() {
			Outputs[0].On = !prevTick && Inputs[0].On;
			
			if (!prevTick && Inputs[0].On) QueueLogicUpdate();
			
            prevTick = Inputs[0].On;
        }
	}
	
	// One tick pulse on falling edge
	public class FallingEdgeMono: LogicComponent {
		private bool prevTick;
		protected override void Initialize() {
			prevTick = false;
		}

		protected override void DoLogicUpdate() {
			Outputs[0].On = prevTick && !Inputs[0].On;
			
			if (prevTick && !Inputs[0].On) QueueLogicUpdate();
			
			prevTick = Inputs[0].On;
		}
	}
	
	// One tick pulse on any transition
	public class DualEdgeMono: LogicComponent {
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
	
	public class TFlipFlop: LogicComponent {
		private bool t;
		private bool prevTick;
		protected override void Initialize() {
			t = false;
			prevTick = false;
		}
		
		protected override void DoLogicUpdate() {
			Outputs[0].On = t;
			if (Inputs[0].On && !prevTick) {
				t = !t;
				QueueLogicUpdate();
			}
			
			prevTick = Inputs[0].On;
		}
	}
}
