using System;
using System.Collections.Generic;
using System.Linq;
using LogicAPI.Server.Components;

namespace CirnosCircuits {
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

	public class ByteToSevenSeg : LogicComponent {
		private IOHandler ioHandler;
		private readonly byte[] segmentCode = {
			0b01111110, // 0
			0b00110000, // 1
			0b01101101, // 2
			0b01111001, // 3
			0b00110011, // 4
			0b01011011, // 5
			0b01011111, // 6
			0b01110000, // 7
			0b01111111, // 8
			0b01111011  // 9
		};
		
		protected override void Initialize() {
			ioHandler = new IOHandler(Inputs, Outputs);
		}

		protected override void DoLogicUpdate() {
			byte result = ioHandler.GetInputAs<byte>();
			if (result == 0) {
				ioHandler.OutputNumber(segmentCode[0]);
				return;
			}

			int place = 0;
			int divisor = 1;
			while (true) {
				int digit = result / divisor;
				if (digit == 0) break;
				byte outDigit = segmentCode[digit % 10];
				ioHandler.OutputNumber(outDigit, 7 * place);
				divisor *= 10;
				place++;
			}
		}
	} // Not Implemented
	
	public class WordToSevenSeg : LogicComponent {
		private IOHandler ioHandler;
		private readonly byte[] segmentCode = {
			0b01111110, // 0
			0b00110000, // 1
			0b01101101, // 2
			0b01111001, // 3
			0b00110011, // 4
			0b01011011, // 5
			0b01011111, // 6
			0b01110000, // 7
			0b01111111, // 8
			0b01111011  // 9
		};
		
		protected override void Initialize() {
			ioHandler = new IOHandler(Inputs, Outputs);
		}

		protected override void DoLogicUpdate() {
			ushort result = ioHandler.GetInputAs<ushort>();
			if (result == 0) {
				ioHandler.OutputNumber(segmentCode[0]);
				return;
			}
			
			int place = 0;
			int divisor = 1;
			while (true) {
				int digit = result / divisor;
				if (digit == 0) break;
				byte outDigit = segmentCode[digit % 10];
				ioHandler.OutputNumber(outDigit, 7 * place);
				divisor *= 10;
				place++;
			}
		}
	} // Not Implemented
	
	public class DWordToSevenSeg : LogicComponent {
		private IOHandler ioHandler;
		private readonly byte[] segmentCode = {
			0b01111110, // 0
			0b00110000, // 1
			0b01101101, // 2
			0b01111001, // 3
			0b00110011, // 4
			0b01011011, // 5
			0b01011111, // 6
			0b01110000, // 7
			0b01111111, // 8
			0b01111011  // 9
		};
		
		protected override void Initialize() {
			ioHandler = new IOHandler(Inputs, Outputs);
		}

		protected override void DoLogicUpdate() {
			uint result = ioHandler.GetInputAs<uint>();
			if (result == 0) {
				ioHandler.OutputNumber(segmentCode[0]);
				return;
			}
			int place = 0;
			int divisor = 1;
			while (true) {
				long digit = result / divisor;
				if (digit == 0) break;
				byte outDigit = segmentCode[digit % 10];
				ioHandler.OutputNumber(outDigit, 7 * place);
				divisor *= 10;
				place++;
			}
		}
	} // Not Implemented
	
	public class QWordToSevenSeg : LogicComponent {
		private IOHandler ioHandler;
		private readonly byte[] segmentCode = {
			0b01111110, // 0
			0b00110000, // 1
			0b01101101, // 2
			0b01111001, // 3
			0b00110011, // 4
			0b01011011, // 5
			0b01011111, // 6
			0b01110000, // 7
			0b01111111, // 8
			0b01111011  // 9
		};
		
		protected override void Initialize() {
			ioHandler = new IOHandler(Inputs, Outputs);
		}

		protected override void DoLogicUpdate() {
			ulong result = ioHandler.GetInputAs<ulong>();
			if (result == 0) {
				ioHandler.OutputNumber(segmentCode[0]);
				return;
			}
			int place = 0;
			ulong divisor = 1;
			while (true) {
				ulong digit = result / divisor;
				if (digit == 0) break;
				byte outDigit = segmentCode[digit % 10];
				ioHandler.OutputNumber(outDigit, 7 * place);
				divisor *= 10;
				place++;
			}
		}
	} // Not Implemented
}
