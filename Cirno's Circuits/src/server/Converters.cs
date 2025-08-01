﻿using System;
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
