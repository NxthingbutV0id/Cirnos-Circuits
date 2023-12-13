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
}
