using LogicAPI.Server.Components;
using LogicWorld;
using LogicWorld.ClientCode;

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
			dynamic a = 0, b = 0;
			bool signed = Inputs[Inputs.Count - 1].On;
			int bitres = (Inputs.Count - 1) >> 1;
			switch (bitres, signed) {
				case (8, true):
					a = utils.InputValue<sbyte>(0, bitres);
					b = utils.InputValue<sbyte>(bitres, bitres << 1);
					break;
				case (8, false):
					a = utils.InputValue<byte>(0, bitres);
					b = utils.InputValue<byte>(bitres, bitres << 1);
					break;
				case (16, true):
					a = utils.InputValue<short>(0, bitres);
					b = utils.InputValue<short>(bitres, bitres << 1);
					break;
				case (16, false):
					a = utils.InputValue<ushort>(0, bitres);
					b = utils.InputValue<ushort>(bitres, bitres << 1);
					break;
				case (32, true):
					a = utils.InputValue<int>(0, bitres);
					b = utils.InputValue<int>(bitres, bitres << 1);
					break;
				case (32, false):
					a = utils.InputValue<uint>(0, bitres);
					b = utils.InputValue<uint>(bitres, bitres << 1);
					break;
				case (64, true):
					a = utils.InputValue<long>(0, bitres);
					b = utils.InputValue<long>(bitres, bitres << 1);
					break;
				case (64, false):
					a = utils.InputValue<ulong>(0, bitres);
					b = utils.InputValue<ulong>(bitres, bitres << 1);
					break;
			}

			Outputs[0].On = a > b;
			Outputs[1].On = a == b;
			Outputs[2].On = a < b;
		}
	}
}
