using LogicAPI.Server.Components;

namespace CirnosCircuits {
	public class ProgramCounter: LogicComponent { 
        private uint counter;
        private bool clk;
        private bool prevClk;
        private bool jumpEnable;
        private IOHandler ioHandler;

        protected override void Initialize() {
	        ioHandler = new IOHandler(Inputs, Outputs);
			counter = 0;
			prevClk = false;
		}

		protected override void DoLogicUpdate() {
			clk = Inputs[Inputs.Count - 1].On;
			jumpEnable = Inputs[Inputs.Count - 2].On;

			if (!prevClk && clk) {
				if (jumpEnable) {
					SetCounter(ioHandler.GetInputAsU32());
				} else {
					if (counter == uint.MaxValue) {
						counter = 0;
					} else {
						counter++;
					}
				}
			}
			prevClk = clk;
		}

		private void SetCounter(uint input) => counter = input;
    } // Not Implemented
}
