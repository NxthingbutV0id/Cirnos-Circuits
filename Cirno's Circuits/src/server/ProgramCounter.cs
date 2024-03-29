﻿using LogicAPI.Server.Components;

namespace CirnosCircuits {
	public class ProgramCounter : LogicComponent { 
        private ulong counter;
		private bool CLK, prevCLK, JumpEnable;
		protected override void Initialize() {
			counter = 0;
			prevCLK = false;
		}

		protected override void DoLogicUpdate() {
			var io = new IOHandler(Inputs, Outputs);
			CLK = Inputs[Inputs.Count - 1].On;
			JumpEnable = Inputs[Inputs.Count - 2].On;

			if (!prevCLK && CLK) {
				if (JumpEnable) {
					SetCounter(io.GrabValueFromInput(0, 32));
				} else {
					if (counter == ulong.MaxValue) {
						counter = 0;
					} else {  
						counter++; 
					}
				}
			}
			prevCLK = CLK;
		}

		private void SetCounter(ulong input) => counter = input;
    } // Not Implemented
}
