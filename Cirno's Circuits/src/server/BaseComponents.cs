using LogicAPI.Server.Components;
using System;

namespace CirnosCircuits {
    public abstract class BaseDecoder : LogicComponent {
        public abstract int Bits { get; } //Unused, for a label
		protected override void DoLogicUpdate() {
			var io = new IOHandler(Inputs, Outputs);
			io.ClearOutputs();

			int index = (int)io.GrabValueFromInput();

			Outputs[index].On = true;
		}
	}

	public abstract class BaseMultiplexer : LogicComponent {
		public abstract int InputBits { get; }

		protected override void DoLogicUpdate() {
			var io = new IOHandler(Inputs, Outputs);

			int selecter = (int)io.GrabValueFromInput(InputBits);

			Outputs[0].On = Inputs[selecter].On;
		}
    }

    public abstract class BaseDLatch : LogicComponent {
		public abstract int Bits { get; }
		private bool[] data;
		protected override void Initialize() {
			data = new bool[Bits];
		}

		protected override void DoLogicUpdate() {
			bool writeEnable = Inputs[Bits].On;
			var io = new IOHandler(Inputs, Outputs);


            if (data != null) {
				if (writeEnable) {
					data = io.GrabBoolArrayFromInput(0, Bits);
				}

				io.OutputBoolArray(data);
			}
		}
    }

    public abstract class BaseDFlipFlop : LogicComponent {
        public abstract int Bits { get; }
		private bool prevCLK, CLK;
		protected override void Initialize() {
			prevCLK = false;
		}

		protected override void DoLogicUpdate() {
            CLK = Inputs[Outputs.Count].On;
			if (!prevCLK && CLK) {
				for (int i = 0; i < Bits; i++) {
					Outputs[i].On = Inputs[i].On;
				}
			}
			prevCLK = CLK;
		}
    }

    public abstract class BaseComparator : LogicComponent {
        public abstract int Bits { get; }
        protected override void DoLogicUpdate() {
			var io = new IOHandler(Inputs, Outputs);
			ulong a, b;
			bool signed = Inputs[Bits].On;

			a = io.GrabValueFromInput(0, Bits);
			b = io.GrabValueFromInput(Bits, Bits << 1);

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
    }

    public abstract class BaseBCD7Seg : LogicComponent {
        private readonly bool[][] digits = {
            new bool[] { true, true, true, true, true, true, false }, // 0
			new bool[] { false, true, true, false, false, false, false }, // 1
			new bool[] { true, true, false, true, true, false, true }, // 2
			new bool[] { true, true, true, true, false, false, true }, // 3
			new bool[] { false, true, true, false, false, true, true }, // 4
			new bool[] { true, false, true, true, false, true, true }, // 5
			new bool[] { true, false, true, true, true, true, true }, // 6
			new bool[] { true, true, true, false, false, false, false }, // 7
			new bool[] { true, true, true, true, true, true, true }, // 8
			new bool[] { true, true, true, true, false, true, true }, // 9
			new bool[] { false, false, false, false, false, false, false }, //blank
			};
        public abstract int Bits { get; }

        protected override void DoLogicUpdate() {
            var io = new IOHandler(Inputs, Outputs);
            bool rippleBlank = true;
            ulong input = io.GrabValueFromInput();

            int numberOfDigits = (int)Math.Ceiling(Math.Log10(1 << Bits)); // n = ceil(log_10(2^bits))

            bool[] segments;

            for (int i = numberOfDigits; i < 0; i--) { // Starts from MSD and moves down to apply ripple blanking correctly
                int digit = (int)(input / IOHandler.Pow(10, i)) % 10;
                (segments, rippleBlank) = SevenSegDriver(digit, rippleBlank);
                io.OutputBoolArray(segments, i * 9);
            }
        }

        private (bool[], bool) SevenSegDriver(int number, bool rippleBlank) {
            int digit = number & 0x0F;
            rippleBlank = rippleBlank && digit == 0;

            return rippleBlank ? (digits[10], rippleBlank) : (digits[digit], false);

        }
    }

    public abstract class BaseBCDFractional : LogicComponent {
        public abstract int Bits { get; }

        protected override void DoLogicUpdate() {
            var io = new IOHandler(Inputs, Outputs);
            byte input = (byte)(io.GrabValueFromInput() & 0xFF);

            double number = input / Math.Pow(2, Bits);
            int digit;

            for (int i = 0; i < Bits; i++) {
                digit = (int)Math.Floor(number * IOHandler.Pow(10, i + 1)) % 10;
                io.OutputInteger((i * 4, i * 4 + 4), digit);
            }
        }
    }

	public abstract class BaseRelay : LogicComponent {
		public abstract int Bits { get; }
        private IInputPeg[] inputsA, inputsB;
        private bool wasOpen;

        protected override void Initialize() {

            inputsA = new IInputPeg[Bits];
            inputsB = new IInputPeg[Bits];

            for (int i = 0; i < Bits; i++) {
                inputsA[i] = Inputs[i];
                inputsB[i] = Inputs[i + Bits];
            }
        }

        protected override void DoLogicUpdate() {
            bool isOpen = Inputs[Bits << 1].On;

            if (wasOpen != isOpen) {
                if (isOpen) {
                    for (int i = 0; i < Bits; i++) {
                        inputsA[i].AddPhasicLinkWith(inputsB[i]);
                    }
                } else {
                    for (int i = 0; i < Bits; i++) {
                        inputsA[i].RemovePhasicLinkWith(inputsB[i]);
                    }
                }
                wasOpen = isOpen;
            }
        }

        public override bool InputAtIndexShouldTriggerComponentLogicUpdates(int inputIndex) {
            return inputIndex == (Bits << 1);
        }
    }
}
