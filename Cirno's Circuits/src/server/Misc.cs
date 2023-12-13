using LogicAPI.Server.Components;

namespace CirnosCircuits {
	public class Decoder : LogicComponent {
		protected override void DoLogicUpdate() {
			var utils = new Utils(Inputs, Outputs);
			utils.ClearOutputs();

			var index = utils.InputValue<int>();

			Outputs[index].On = true;
		}
	}

	public class Multiplexer : LogicComponent {
		protected override void DoLogicUpdate() {
			int pins = Inputs.Count;
			int leftCount = 0;
			var utils = new Utils(Inputs, Outputs);

			while (pins != 1) {
				pins >>= 1;
				leftCount++;
			}

			var selecter = utils.InputValue<int>(Inputs.Count - leftCount);

			Outputs[0].On = Inputs[selecter].On;
		}
	}

	public class ByteDLatch : LogicComponent {
		private bool[] data = new bool[1];
		private int enablePin;
		protected override void Initialize() {
			enablePin = Outputs.Count; // the index of the enable signal is the last input pin
			data = new bool[Outputs.Count];
		}

		protected override void DoLogicUpdate() {
			if (data != null) {
				if (Inputs[enablePin].On) {
					for (int i = 0; i < data.Length; i++) {
						data[i] = Inputs[i].On;
					}
				}

				for (int i = 0; i < Outputs.Count; i++) {
					Outputs[i].On = data[i];
				}
			}
		}
	}

	public class Comparator : LogicComponent {
		protected override void DoLogicUpdate() {
			var utils = new Utils(Inputs, Outputs);
			dynamic a, b;
			bool signed = Inputs[Inputs.Count - 1].On;
			int bitres = (Inputs.Count - 1) >> 1;

			if (signed) {
				a = utils.InputValue<long>(0, bitres);
				b = utils.InputValue<long>(bitres, bitres << 1);
			} else {
				a = utils.InputValue<ulong>(0, bitres);
				b = utils.InputValue<ulong>(bitres, bitres << 1);
			}

			Outputs[0].On = a > b;
			Outputs[1].On = a == b;
			Outputs[2].On = a < b;
		}
	}
}
