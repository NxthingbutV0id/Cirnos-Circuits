using LogicAPI.Server.Components;
using System;

namespace CirnosCircuits {
	public class Decoder : LogicComponent {
		protected override void DoLogicUpdate() {
			var utils = new Utils(Inputs, Outputs);
			utils.ClearOutputs();

			int index = (int)utils.GrabValueFromInput();

			Outputs[index].On = true;
		}
	} // Completed

	public class Multiplexer : LogicComponent {
		protected override void DoLogicUpdate() {
			var utils = new Utils(Inputs, Outputs);

			int offset = 1 << (int)Math.Floor(Math.Log2(Inputs.Count));

			int selecter = (int)utils.GrabValueFromInput(offset);

			Outputs[0].On = Inputs[selecter].On;
		}
	} // Completed

	public class DLatch : LogicComponent { //Completed
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
	} // Completed

	public class DFlipFlop : LogicComponent { 
		private bool[] data = new bool[1];
		private bool prevCLK, CLK;
		protected override void Initialize() {
			prevCLK = false;
			data = new bool[Outputs.Count];
		}

		protected override void DoLogicUpdate() {
            CLK = Inputs[Outputs.Count].On;
            if (data != null) {
				if (!prevCLK && CLK) {
					for (int i = 0; i < data.Length; i++) {
						data[i] = Inputs[i].On;
					}
					for (int i = 0; i < Outputs.Count; i++) {
						Outputs[i].On = data[i];
					}
				}
				prevCLK = CLK;
			}
		}
	} // Completed

	public class Comparator : LogicComponent { 
		protected override void DoLogicUpdate() {
			var utils = new Utils(Inputs, Outputs);
			ulong a, b;
			bool signed = Inputs[Inputs.Count - 1].On;
			int bitres = (Inputs.Count - 1) >> 1;

			a = utils.GrabValueFromInput(0, bitres);
			b = utils.GrabValueFromInput(bitres, bitres << 1);

			if (signed) {
                Outputs[0].On = (long)a > (long)b;
                Outputs[1].On = (long)a == (long)b;
                Outputs[2].On = (long)a < (long)b;
            } else {
                Outputs[0].On = a > b;
                Outputs[1].On = a == b;
                Outputs[2].On = a < b;
            }
        }
	} // Completed

	public class SRLatch : LogicComponent { 
		private bool S, R, Q, Qbar;

		protected override void Initialize() {
			Q = false;
			Qbar = true;
		}

		protected override void DoLogicUpdate() {
			S = Inputs[0].On; R = Inputs[1].On;

			for (int i = 0; i < 3; ++i) {
				Qbar = !(S || Q);
				Q = !(R || Qbar);
			}

			Outputs[0].On = Q;
			Outputs[1].On = Qbar;
		}
	} // Completed

	public class JKFlipFlop : LogicComponent { 
		private bool J, K, CLK, previousCLK;

		protected override void Initialize() {
			Outputs[0].On = false; //  Q
			Outputs[1].On = true;  // !Q
			previousCLK = false;
		}

		protected override void DoLogicUpdate() {
			J = Inputs[0].On;
			K = Inputs[1].On;
			CLK = Inputs[2].On;
			if (!previousCLK && CLK) {
				if (J && K) {
					Outputs[0].On = !Outputs[0].On;
					Outputs[1].On = !Outputs[1].On;
                    previousCLK = CLK;
                    return;
                }
                if (!J && K) {
                    Outputs[0].On = false;
                    Outputs[1].On = true;
                    previousCLK = CLK;
                    return;
                }
                if (J && !K) {
                    Outputs[0].On = true;
                    Outputs[1].On = false;
                    previousCLK = CLK;
                    return;
                }
            }
			previousCLK = CLK;
		}
	} // Completed

	public class SidewaysAND : LogicComponent { 
		protected override void DoLogicUpdate() {
			Outputs[0].On = Inputs[0].On & Inputs[1].On;
		}
	} // Completed

	public class SidewaysOR : LogicComponent {
		protected override void DoLogicUpdate() {
			Outputs[0].On = Inputs[0].On | Inputs[1].On;
		}
	} // Completed

	public class SidewaysXOR : LogicComponent {
		protected override void DoLogicUpdate() {
			Outputs[0].On = Inputs[0].On ^ Inputs[1].On;
		}
	} // Completed

	public class SidewaysNAND : LogicComponent {
		protected override void DoLogicUpdate() {
			Outputs[0].On = !(Inputs[0].On & Inputs[1].On);
		}
	} // Completed

	public class SidewaysNOR : LogicComponent {
		protected override void DoLogicUpdate() {
			Outputs[0].On = !(Inputs[0].On | Inputs[1].On);
		}
	} // Completed

	public class SidewaysXNOR : LogicComponent {
		protected override void DoLogicUpdate() {
			Outputs[0].On = !(Inputs[0].On ^ Inputs[1].On);
		}
	} // Completed
}
