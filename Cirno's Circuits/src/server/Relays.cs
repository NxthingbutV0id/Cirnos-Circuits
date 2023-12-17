using LogicAPI.Server.Components;

namespace CirnosCircuits {
	public class ByteRelay : LogicComponent {
		private IInputPeg[] inputsA, inputsB;
		private bool wasOpen;
		private readonly int size = 8;

		protected override void Initialize() {

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
    } // Implemented, Not Tested

    public class WordRelay : LogicComponent {
        private IInputPeg[] inputsA, inputsB;
        private bool wasOpen;
        private readonly int size = 16;

        protected override void Initialize() {

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
    } // Not Implemented

    public class DWordRelay : LogicComponent {
        private IInputPeg[] inputsA, inputsB;
        private bool wasOpen;
        private readonly int size = 32;

        protected override void Initialize() {

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
    } // Not Implemented

    public class QWordRelay : LogicComponent {
        private IInputPeg[] inputsA, inputsB;
        private bool wasOpen;
        private readonly int size = 64;

        protected override void Initialize() {

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
    } // Not Implemented
}
