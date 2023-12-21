using LogicAPI.Server.Components;
using System;

namespace CirnosCircuits {
	public class BCDToBinary : LogicComponent {
		protected override void DoLogicUpdate() {
			var utils = new Utils(Inputs, Outputs);
			ulong result = 0;

			for (int i = 0; i < 76; i += 4) {
				var input = utils.GrabValueFromInput(i, i + 4);
				if (input > 9) { input = 9; }

				result += input * Utils.Pow(10, i >> 2);
			}

			utils.OutputInteger(result);
		}
	} // Completed

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
			ulong input = utils.GrabValueFromInput(0, Inputs.Count - 1);

			if (signed) {
				negative = (long)input < 0;
				if (negative) { input = ~input + 1; }
			} else {
				negative = false;
			}

			int numberOfDigits, bitCount = Inputs.Count - 1;
			if (bitCount == 8) { numberOfDigits = 3; }		  // 255 
			else if (bitCount == 16) { numberOfDigits = 5; }  // 65536
			else if (bitCount == 32) { numberOfDigits = 10; } // 4294967296
			else { numberOfDigits = 20; }                     // 18446744073709551616

			bool[] segments;

			for (int i = numberOfDigits; i < 0; i--) {
				int digit = (int) (input / Utils.Pow(10, i)) % 10;
				(segments, rippleBlank, negative) = SevenSegDriver(digit, i, rippleBlank, negative);
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
	} // Not Implemented

	public class ByteToBCDFractional : LogicComponent {
		private readonly int BitRes = 8;

		protected override void DoLogicUpdate() {
			var utils = new Utils(Inputs, Outputs);
			byte input = (byte)(utils.GrabValueFromInput() & 0xFF);

			double number = input / Math.Pow(2, BitRes);
			int digit;

			for (int i = 0; i < BitRes; i++) {
				digit = (int)Math.Floor(number * Utils.Pow(10, i + 1)) % 10;
				utils.OutputInteger((i * 4, i * 4 + 4), digit);
			}
		}
	} // Not Implemented

	public class WordToBCDFractional : LogicComponent {
		private readonly int BitRes = 16;

		protected override void DoLogicUpdate() {
			var utils = new Utils(Inputs, Outputs);
			ushort input = (ushort)(utils.GrabValueFromInput() & 0xFFFF);

			double number = input / Math.Pow(2, BitRes);
			int digit;

			for (int i = 0; i < BitRes; i++) {
				digit = (int)Math.Floor(number * Utils.Pow(10, i + 1)) % 10;
				utils.OutputInteger((i * 4, i * 4 + 4), digit);
			}
		}
	} // Not Implemented

	public class DWordToBCDFractional : LogicComponent {
		private readonly int BitRes = 32;

		protected override void DoLogicUpdate() {
			var utils = new Utils(Inputs, Outputs);
			uint input = (uint)(utils.GrabValueFromInput() & 0xFFFFFFFF);

			double number = input / Math.Pow(2, BitRes);
			int digit;

			for (int i = 0; i < BitRes; i++) {
				digit = (int)Math.Floor(number * Utils.Pow(10, i + 1)) % 10;
				utils.OutputInteger((i * 4, i * 4 + 4), digit);
			}
		}
	} // Not Implemented

	public class QWordToBCDFractional : LogicComponent {
		private int DigitRes;

		protected override void Initialize() {
			DigitRes = Outputs.Count >> 2;
		}

		protected override void DoLogicUpdate() {
			var utils = new Utils(Inputs, Outputs);
			var input = utils.GrabValueFromInput();

			double number = input / Math.Pow(2, 64);
			int digit;

			for (int i = 0; i < DigitRes; i++) {
				digit = (int)Math.Floor(number * Utils.Pow(10, i + 1)) % 10;
				utils.OutputInteger((i * 4, i * 4 + 4), digit);
			}
		}
	} // Not Implemented
}
