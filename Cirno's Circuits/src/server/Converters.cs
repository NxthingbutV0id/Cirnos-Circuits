using LogicAPI.Server.Components;
using LogicWorld.ClientCode;

namespace CirnosCircuits {
	public class BCDToBinary : LogicComponent {
		protected override void DoLogicUpdate() {
			var utils = new Utils(Inputs, Outputs);
			ulong result = 0;

			for (int i = 0; i < 19; i++) {
				var input = utils.InputValue<byte>(i << 2, i << 2 + 4);
				if (input > 9) { input = 9; }

				result += input * Pow(10, i);
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
}
