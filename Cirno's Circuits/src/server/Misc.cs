using LogicAPI.Server.Components;
using System;

namespace CirnosCircuits {
	public class Decoder : LogicComponent {
		protected override void DoLogicUpdate() {
			var utils = new Utils(Inputs, Outputs);
			utils.ClearOutputs();

			int index = (int) utils.GrabValueFromInput();

			Outputs[index].On = true;
		}
	}

	public class Multiplexer : LogicComponent {
		protected override void DoLogicUpdate() {
			var utils = new Utils(Inputs, Outputs);

			int offset = (int) Math.Floor(Math.Log2(Inputs.Count));

			int selecter = (int) utils.GrabValueFromInput(Inputs.Count - 1 - offset);

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
				a = utils.GrabValueFromInput(0, bitres);
				b = utils.GrabValueFromInput(bitres, bitres << 1);
			} else {
				a = utils.GrabValueFromInput(0, bitres);
				b = utils.GrabValueFromInput(bitres, bitres << 1);
			}

			Outputs[0].On = a > b;
			Outputs[1].On = a == b;
			Outputs[2].On = a < b;
		}
	}
}
