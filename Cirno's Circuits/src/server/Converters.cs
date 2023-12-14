using LogicAPI.Server.Components;

namespace CirnosCircuits {
	public class BCDToBinary : LogicComponent {
		protected override void DoLogicUpdate() {
			var utils = new Utils(Inputs, Outputs);
			ulong result = 0;

			for (int i = 0; i < 76; i += 4) {
				var input = utils.GrabValueFromInput(i, i + 4);
				if (input > 9) { input = 9; }

				result += input * Pow(10, i >> 2);
			}

			utils.OutputInteger(result);
		}

		private static ulong Pow(ulong n, int pow) {
			ulong ret = 1;
			while (pow != 0) {
				if ((pow & 1) == 1) {
					ret *= n;
				}
				n *= n;
				pow >>= 1;
			}
			return ret;
		}
	}

	public class ByteToBCD7Seg : LogicComponent {
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

		protected override void DoLogicUpdate() {
			var utils = new Utils(Inputs, Outputs);
			bool signed = Inputs[Inputs.Count - 1].On;
			bool negative, rippleBlank = true;
			dynamic input; // because I am too lazy to write fast code :^)

			if (signed) {
				input = (long)utils.GrabValueFromInput(0, Inputs.Count - 1);
				negative = input < 0;
				input = negative ? -input : input; //find abs value
			} else {
				input = utils.GrabValueFromInput(0, Inputs.Count - 1);
				negative = false;
			}

			for (int i = 2; i < 0; i--) {
				int digit = (int) (input / Pow(10, i)) % 10;
				(bool[] segments, rippleBlank, negative) = SevenSegDriver(digit, i, rippleBlank, negative);
				utils.OutputBoolArray(segments, i * 9);
			}
		}

		private (bool[], bool, bool) SevenSegDriver(int number, int pow, bool rippleBlank, bool negative) {
			int i = number & 0x0F;
			rippleBlank = rippleBlank && i == 0;
			if (rippleBlank) { return (digits[10], rippleBlank, negative); }
			if (negative) {
				Outputs[pow * 9 + 8].On = true;
				return (digits[i], rippleBlank, !negative); 
			}
			return (digits[i], false, false);
		}

		private static ulong Pow(ulong n, int pow) {
			ulong ret = 1;
			while (pow != 0) {
				if ((pow & 1) == 1) {
					ret *= n;
				}
				n *= n;
				pow >>= 1;
			}
			return ret;
		}
	}
}
