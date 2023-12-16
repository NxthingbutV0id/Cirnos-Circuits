using LogicAPI.Server.Components;

namespace CirnosCircuits {
	public class Relay : LogicComponent { //Implemented, Not Tested
		private IInputPeg[] inputsA, inputsB;
		private bool wasOpen;
		private int size;

		protected override void Initialize() {
			size = (Inputs.Count - 1) >> 1; // a N bit relay has 2N+1 pins

			inputsA = new IInputPeg[size];
			inputsB = new IInputPeg[size];

			for (int i = 0; i < size; i++) {
				inputsA[i] = Inputs[i];
				inputsB[i] = Inputs[i + size];
			}
		}

		protected override void DoLogicUpdate() {
			bool isOpen = Inputs[Inputs.Count - 1].On;

			if (wasOpen != isOpen) {
				if (isOpen) {
					for (int i = 0; i < size; i++) {
						inputsA[i].AddPhasicLinkWith(inputsB[i]);
					}
				} else {
					for (int i = 0; i < size; i++) {
						inputsA[i].RemovePhasicLinkWith(inputsB[i]);
					}
				}
				wasOpen = isOpen;
			}
		}

		public override bool InputAtIndexShouldTriggerComponentLogicUpdates(int inputIndex) {
			return inputIndex == (size << 1);
		}
	}
}
