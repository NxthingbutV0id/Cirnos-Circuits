using LogicAPI.Server.Components;
using LogicWorld.ClientCode;

namespace CirnosCircuits {
	public class ByteRelay : LogicComponent {
		private readonly IInputPeg[] inputsA = new IInputPeg[8];
		private readonly IInputPeg[] inputsB = new IInputPeg[8];
		private bool wasOpen;

		protected override void Initialize() {
			for (int i = 0; i < 8; i++) {
				inputsA[i] = Inputs[i];
				inputsB[i] = Inputs[i + 8];
			}
		}

		protected override void DoLogicUpdate() {
			bool isOpen = Inputs[16].On;

			if (wasOpen != isOpen) {
				if (isOpen) {
					for (int i = 0; i < 8; i++) {
						inputsA[i].AddPhasicLinkWith(inputsB[i]);
					}
				} else {
					for (int i = 0; i < 8; i++) {
						inputsA[i].RemovePhasicLinkWith(inputsB[i]);
					}
				}
				wasOpen = isOpen;
			}
		}

		public override bool InputAtIndexShouldTriggerComponentLogicUpdates(int inputIndex) {
			return inputIndex == 16;
		}
	}

	public class WordRelay : LogicComponent {
		private readonly IInputPeg[] inputsA = new IInputPeg[16];
		private readonly IInputPeg[] inputsB = new IInputPeg[16];
		private bool wasOpen;

		protected override void Initialize() {
			for (int i = 0; i < 16; i++) {
				inputsA[i] = Inputs[i];
				inputsB[i] = Inputs[i + 16];
			}
		}

		protected override void DoLogicUpdate() {
			bool isOpen = Inputs[32].On;

			if (wasOpen != isOpen) {
				if (isOpen) {
					for (int i = 0; i < 16; i++) {
						inputsA[i].AddPhasicLinkWith(inputsB[i]);
					}
				} else {
					for (int i = 0; i < 16; i++) {
						inputsA[i].RemovePhasicLinkWith(inputsB[i]);
					}
				}
				wasOpen = isOpen;
			}
		}

		public override bool InputAtIndexShouldTriggerComponentLogicUpdates(int inputIndex) {
			return inputIndex == 32;
		}
	}

	public class DWordRelay : LogicComponent {
		private readonly IInputPeg[] inputsA = new IInputPeg[32];
		private readonly IInputPeg[] inputsB = new IInputPeg[32];
		private bool wasOpen;

		protected override void Initialize() {
			for (int i = 0; i < 32; i++) {
				inputsA[i] = Inputs[i];
				inputsB[i] = Inputs[i + 32];
			}
		}

		protected override void DoLogicUpdate() {
			bool isOpen = Inputs[64].On;

			if (wasOpen != isOpen) {
				if (isOpen) {
					for (int i = 0; i < 32; i++) {
						inputsA[i].AddPhasicLinkWith(inputsB[i]);
					}
				} else {
					for (int i = 0; i < 32; i++) {
						inputsA[i].RemovePhasicLinkWith(inputsB[i]);
					}
				}
				wasOpen = isOpen;
			}
		}

		public override bool InputAtIndexShouldTriggerComponentLogicUpdates(int inputIndex) {
			return inputIndex == 64;
		}
	}

	public class QWordRelay : LogicComponent {
		private readonly IInputPeg[] inputsA = new IInputPeg[64];
		private readonly IInputPeg[] inputsB = new IInputPeg[64];
		private bool wasOpen;

		protected override void Initialize() {
			for (int i = 0; i < 64; i++) {
				inputsA[i] = Inputs[i];
				inputsB[i] = Inputs[i + 64];
			}
		}

		protected override void DoLogicUpdate() {
			bool isOpen = Inputs[128].On;

			if (wasOpen != isOpen) {
				if (isOpen) {
					for (int i = 0; i < 64; i++) {
						inputsA[i].AddPhasicLinkWith(inputsB[i]);
					}
				} else {
					for (int i = 0; i < 64; i++) {
						inputsA[i].RemovePhasicLinkWith(inputsB[i]);
					}
				}
				wasOpen = isOpen;
			}
		}

		public override bool InputAtIndexShouldTriggerComponentLogicUpdates(int inputIndex) {
			return inputIndex == 128;
		}
	}
}
