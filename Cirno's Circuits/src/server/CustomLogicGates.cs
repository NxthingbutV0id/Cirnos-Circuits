using LogicAPI.Server.Components;

namespace CirnosCircuits {
	public class SidewaysAND : LogicComponent {
		protected override void DoLogicUpdate() {
			Outputs[0].On = Inputs[0].On & Inputs[1].On;
		}
	}

	public class SidewaysOR : LogicComponent {
		protected override void DoLogicUpdate() {
			Outputs[0].On = Inputs[0].On | Inputs[1].On;
		}
	}

	public class SidewaysXOR : LogicComponent {
		protected override void DoLogicUpdate() {
			Outputs[0].On = Inputs[0].On ^ Inputs[1].On;
		}
	}

	public class SidewaysNAND : LogicComponent {
		protected override void DoLogicUpdate() {
			Outputs[0].On = !(Inputs[0].On & Inputs[1].On);
		}
	}

	public class SidewaysNOR : LogicComponent {
		protected override void DoLogicUpdate() {
			Outputs[0].On = !(Inputs[0].On | Inputs[1].On);
		}
	}

	public class SidewaysXNOR : LogicComponent {
		protected override void DoLogicUpdate() {
			Outputs[0].On = !(Inputs[0].On ^ Inputs[1].On);
		}
	}

	public class SRLatch : LogicComponent {
		protected override void Initialize() {
			Outputs[0].On = false; //Q
			Outputs[1].On = true;  //!Q
		}

		protected override void DoLogicUpdate() {
			Outputs[0].On = !(Inputs[0].On || Outputs[1].On); //  Q = S NOR !Q
			Outputs[1].On = !(Inputs[1].On || Outputs[0].On); // !Q = R NOR  Q
		}
	}

	public class JKFlipFlop : LogicComponent {
		private bool previousCLK;

		protected override void Initialize() {
			Outputs[0].On = false; //Q
			Outputs[1].On = true;  //!Q
			previousCLK = false;
		}

		protected override void DoLogicUpdate() { 
			bool J = Inputs[0].On;
			bool K = Inputs[1].On;
			bool CLK = Inputs[2].On;

			if (!previousCLK && CLK) { //Rising Edge Detection
				if (J && K) {
					Outputs[0].On = !Outputs[0].On;
					Outputs[1].On = !Outputs[1].On;
					return;
				}
				if (J && !K) {
					Outputs[0].On = true;
					Outputs[1].On = false;
                    return;
                }
				if (!J && K) {
					Outputs[0].On = false;
					Outputs[1].On = true;
                    return;
                }
			}
		}
	}
}
