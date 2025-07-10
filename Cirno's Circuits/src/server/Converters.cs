using System;
using System.Collections.Generic;
using System.Linq;
using LogicAPI.Server.Components;

namespace CirnosCircuits {
	public static class Segment {
		private static readonly byte[] SegmentCode = {
			126, 48, 109, 121, 51, 91, 95, 112, 127, 123, 119, 31, 78, 61, 79, 71
		};
		
		public static byte GetSegmentCode(int digit) {
			return SegmentCode[digit & 0xF];
		}
	}
	
	public class BCDToBinary: LogicComponent {
	    private IOHandler ioHandler;
	    protected override void Initialize() {
		    ioHandler = new IOHandler(Inputs, Outputs);
	    }

		protected override void DoLogicUpdate() {
			ulong result = 0ul;
			byte[] temp = new byte[10];
			
			for (int i = 0; i < 10; i++) {
				temp[i] = ioHandler.GetInputAs<byte>(8 * i);
			}

			int power = 0;
			for (int i = 0; i < 10; i++) {
				ulong num = (ulong)temp[i] & 0xf;
				if (num > 9) num = 9;
				result += num * Pow(10, power);

				num = (ulong)(temp[i] >> 4) & 0xf;
				if (num > 9) num = 9;
				result += num * Pow(10, power + 1);

				power += 2;
			}

			ioHandler.OutputNumber(result);
		}

		private static ulong Pow(int n, int p) {
			return (ulong)Math.Pow(n, p);
		}
    } // Completed

	public class HexToSevenSeg : LogicComponent {
		private IOHandler ioHandler;
		protected override void Initialize() {
			ioHandler = new IOHandler(Inputs, Outputs);
		}

		protected override void DoLogicUpdate() {
			ioHandler.ClearOutputs();
			var blank = Inputs[4].On;
			var input = ioHandler.GetInputAs<byte>() & 0xF;
			if (blank && input == 0) {
				ioHandler.OutputNumber(128); // Ripple Blank
				return;
			}
			var output = Segment.GetSegmentCode(input);
			ioHandler.OutputNumber(output);
		}
	}
	
	public class HexTo5x7Matrix : LogicComponent {
		private IOHandler ioHandler;
		
		private readonly ulong[] matrixCodes = {
			0b01110100011001110101110011000101110, // 0
			0b00100011000010000100001000010001110, // 1
			0b01110100010001000100010001000011111, // 2
			0b01110100010001000110000101000101110, // 3
			0b00010001100101010010111110001000010, // 4
			0b11111100001111000001000011000101110, // 5
			0b00110010001000011110100011000101110, // 6
			0b11111000010001000100001000010000100, // 7
			0b01110100011000101110100011000101110, // 8
			0b01110100011000101111000010001001100, // 9
		};
		
		protected override void Initialize() {
			ioHandler = new IOHandler(Inputs, Outputs);
		}

		protected override void DoLogicUpdate() {
			ioHandler.ClearOutputs();
			var blank = Inputs[4].On;
			var input = ioHandler.GetInputAs<byte>() & 0xF;
			if (blank && input == 0) {
				ioHandler.OutputNumber(1ul << 35); // Ripple Blank
				return;
			}
			var output = matrixCodes[input];
			ioHandler.OutputNumber(output);
		}
	}

	public class ByteToBCD : LogicComponent {
		private IOHandler ioHandler;
		protected override void Initialize() {
			ioHandler = new IOHandler(Inputs, Outputs);
		}
		
		protected override void DoLogicUpdate() {
			ioHandler.ClearOutputs();
			var input = ioHandler.GetInputAs<byte>();
			
			var hundreds = input / 100;
			var tens = (input % 100) / 10;
			var ones = input % 10;
			
			var output = (ushort)((hundreds << 8) | (tens << 4) | ones);
			ioHandler.OutputNumber(output);
		}
	}
	
	public class WordToBCD : LogicComponent {
		private IOHandler ioHandler;
		protected override void Initialize() {
			ioHandler = new IOHandler(Inputs, Outputs);
		}
		
		protected override void DoLogicUpdate() {
			ioHandler.ClearOutputs();
			var input = ioHandler.GetInputAs<ushort>();
			
			var tenThousands = input / 10000;
			var thousands = (input % 10000) / 1000;
			var hundreds = (input % 1000) / 100;
			var tens = (input % 100) / 10;
			var ones = input % 10;
			
			var output = (uint)((tenThousands << 16) | (thousands << 12) | (hundreds << 8) | (tens << 4) | ones);
			ioHandler.OutputNumber(output);
		}
	}
	
	public class DWordToBCD : LogicComponent {
		private IOHandler ioHandler;
		protected override void Initialize() {
			ioHandler = new IOHandler(Inputs, Outputs);
		}
		
		protected override void DoLogicUpdate() {
			ioHandler.ClearOutputs();
			var input = ioHandler.GetInputAs<uint>();
			
			var billions = (ulong)(input / 1_000_000_000);
			var hundredMillions = (input % 1_000_000_000) / 100_000_000;
			var tenMillions = (input % 100_000_000) / 10_000_000;
			var millions = (input % 10_000_000) / 1_000_000;
			var hundredThousands = (input % 1_000_000) / 100_000;
			var tenThousands = (input % 100_000) / 10_000;
			var thousands = (input % 10_000) / 1_000;
			var hundreds = (input % 1_000) / 100;
			var tens = (input % 100) / 10;
			var ones = input % 10;
			
			var output =
				(billions << 32) | 
				(hundredMillions << 28) | 
				(tenMillions << 24) | 
				(millions << 20) | 
				(hundredThousands << 16) | 
				(tenThousands << 12) | 
				(thousands << 8) | 
				(hundreds << 4) | 
				tens | 
				ones;
			ioHandler.OutputNumber(output);
		}
	}
	
	public class QWordToBCD : LogicComponent {
		private IOHandler ioHandler;
		protected override void Initialize() {
			ioHandler = new IOHandler(Inputs, Outputs);
		}
		
		protected override void DoLogicUpdate() {
			ioHandler.ClearOutputs();
			var input = ioHandler.GetInputAs<ulong>();
			var output = new byte[10];
			var digits = new int[20];
			for (int i = 0; i < 20; i++) {
				digits[i] = (int)(input % 10);
				input /= 10;
				if (input == 0) { break; }
			}
			for (int i = 0; i < 20; i++) {
				output[i >> 1] |= (byte)(digits[i] << ((i & 1) << 2));
			}
			for (int i = 0; i < 10; i++) {
				ioHandler.OutputNumber(output[i], i << 3);
			}
		}
	}
	
	public class GBScreenIndexToXY : LogicComponent {
		private IOHandler ioHandler;
		
		protected override void Initialize() {
			ioHandler = new IOHandler(Inputs, Outputs);
		}

		protected override void DoLogicUpdate() {
			ioHandler.ClearOutputs();
			var index = ioHandler.GetInputAs<short>();
			var x = index % 160;
			var y = index / 160;
			var output = (ushort)((y << 8) | x);
			ioHandler.OutputNumber(output);
		}
	}
}
