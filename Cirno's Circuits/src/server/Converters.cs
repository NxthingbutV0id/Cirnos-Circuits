using LogicAPI.Server.Components;

namespace CirnosCircuits {
	public class BCDToBinary : LogicComponent {
		protected override void DoLogicUpdate() {
			var io = new IOHandler(Inputs, Outputs);
			ulong result = 0;

			for (int i = 0; i < 76; i += 4) {
				var input = io.GrabValueFromInput(i, i + 4);
				if (input > 9) { input = 9; }

				result += input * IOHandler.Pow(10, i >> 2);
			}

			io.OutputInteger(result);
		}
	} // Completed

    public class ByteToBCDFractional : BaseBCDFractional {
		public override int Bits => 8;
	} // Not Implemented

	public class WordToBCDFractional : BaseBCDFractional {
        public override int Bits => 16;
    } // Not Implemented

	public class DWordToBCDFractional : BaseBCDFractional {
        public override int Bits => 32;
    } // Not Implemented

	public class QWordToBCDFractional : BaseBCDFractional {
        public override int Bits => 64;
    } // Not Implemented
}
