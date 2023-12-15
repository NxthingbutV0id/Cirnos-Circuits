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
    }

    public class Multiplexer : LogicComponent {
        protected override void DoLogicUpdate() {
            var utils = new Utils(Inputs, Outputs);

            int offset = 1 << (int)Math.Floor(Math.Log2(Inputs.Count));

            int selecter = (int)utils.GrabValueFromInput(offset);

            Outputs[0].On = Inputs[selecter].On;
        }
    }

    public class DLatch : LogicComponent {
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

    public class DFlipFlop : LogicComponent {
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
                a = (long) utils.GrabValueFromInput(0, bitres);
                b = (long) utils.GrabValueFromInput(bitres, bitres << 1);
            } else {
                a = utils.GrabValueFromInput(0, bitres);
                b = utils.GrabValueFromInput(bitres, bitres << 1);
            }

            Outputs[0].On = a > b;
            Outputs[1].On = a == b;
            Outputs[2].On = a < b;
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
        private bool J, K, CLK, previousCLK;

        protected override void Initialize() {
            Outputs[0].On = false; //Q
            Outputs[1].On = true;  //!Q
            previousCLK = false;
            J = Inputs[0].On;
            K = Inputs[1].On;
            CLK = Inputs[2].On;
        }

        protected override void DoLogicUpdate() {
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
