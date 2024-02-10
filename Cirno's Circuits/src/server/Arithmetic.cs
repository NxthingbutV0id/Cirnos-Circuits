using LogicAPI.Server.Components;

namespace CirnosCircuits {
    public abstract class BaseAdder : LogicComponent {
        public abstract int Bits { get; }
        protected override void DoLogicUpdate() {
            var io = new IOHandler(Inputs, Outputs);

            ulong a, b;
            bool carryIn;

            a = io.GrabValueFromInput(0, Bits);
            b = io.GrabValueFromInput(Bits, Bits << 1);
            carryIn = Inputs[Bits << 1].On;

            ulong result = carryIn ? a + b + 1 : a + b;

            io.OutputInteger(result);
        }
    }

    public abstract class BaseSubtractor : LogicComponent {
        public abstract int Bits { get; }
        protected override void DoLogicUpdate() {
            var io = new IOHandler(Inputs, Outputs);

            long a, b;
            bool BorrowIn;

            a = io.GrabValueFromInput(0, Bits, true);
            b = io.GrabValueFromInput(Bits, Bits << 1, true);
            BorrowIn = Inputs[Bits << 1].On;

            long result = BorrowIn ? a - b - 1 : a - b;

            io.OutputInteger(result);
        }
    }
}
