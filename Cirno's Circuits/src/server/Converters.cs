using System;
using System.Collections.Generic;
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
				temp[i] = ioHandler.GetInputAsU8(8 * i);
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
}
