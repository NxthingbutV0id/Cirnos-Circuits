using System;
using LogicAPI.Server.Components;

namespace CirnosCircuits {
    public class ByteAdder: LogicComponent {
        private IOHandler ioHandler;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() {
            ioHandler.ClearOutputs();
            bool carryIn = Inputs[16].On;

            byte a = ioHandler.GetInputAs<byte>();
            byte b = ioHandler.GetInputAs<byte>(8);
            int result = carryIn ? a + b + 1 : a + b;
            
            ioHandler.OutputNumber(result);
        }
    }
    
    public class WordAdder: LogicComponent {
        private IOHandler ioHandler;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }
        
        protected override void DoLogicUpdate() {
            ioHandler.ClearOutputs();
            bool carryIn = Inputs[32].On;

            ushort a = ioHandler.GetInputAs<ushort>();
            ushort b = ioHandler.GetInputAs<ushort>(16);
            int result = carryIn ? a + b + 1 : a + b;
            
            ioHandler.OutputNumber(result);
        }
    }
    
    public class DWordAdder: LogicComponent {
        private IOHandler ioHandler;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }
        
        protected override void DoLogicUpdate() {
            ioHandler.ClearOutputs();
            bool carryIn = Inputs[64].On;

            uint a = ioHandler.GetInputAs<uint>();
            uint b = ioHandler.GetInputAs<uint>(32);
            ulong result = carryIn ? a + b + 1 : a + b;
            
            ioHandler.OutputNumber(result);
        }
    }

    public class ByteALU : LogicComponent {
        private IOHandler ioHandler;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }
        
        protected override void DoLogicUpdate() {
            byte ua = ioHandler.GetInputAs<byte>();
            sbyte sa = ioHandler.GetInputAs<sbyte>();
            byte b = ioHandler.GetInputAs<byte>(8);
            byte operation = ioHandler.GetInputAs<byte>(16);
            ushort result = 0;
            if (operation == 0) {
                result = (ushort) (ua + b);
            } else if (operation == 1) {
                result = (ushort) (ua - b);
            } else if (operation == 2) {
                result = (ushort) (ua & b);
            } else if (operation == 3) {
                result = (ushort) (ua | b);
            } else if (operation == 4) {
                result = (ushort) (ua ^ b);
            } else if (operation == 5) {
                result = (ushort) (ua << b);
            } else if (operation == 6) {
                result = (ushort) (ua >> b);
            } else if (operation == 7) {
                result = (ushort) (sa >> b);
            }

            ioHandler.OutputNumber(result);
        }
    }

    public class WordALU : LogicComponent {
        private IOHandler ioHandler;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() {
            ushort ua = ioHandler.GetInputAs<ushort>();
            short sa = ioHandler.GetInputAs<short>();
            ushort b = ioHandler.GetInputAs<ushort>(16);
            byte operation = ioHandler.GetInputAs<byte>(32);
            uint result = 0;
            if (operation == 0) {
                result = (uint) (ua + b);
            } else if (operation == 1) {
                result = (uint) (ua - b);
            } else if (operation == 2) {
                result = (uint) (ua & b);
            } else if (operation == 3) {
                result = (uint) (ua | b);
            } else if (operation == 4) {
                result = (uint) (ua ^ b);
            } else if (operation == 5) {
                result = (uint) (ua << b);
            } else if (operation == 6) {
                result = (uint) (ua >> b);
            } else if (operation == 7) {
                result = (uint) (sa >> b);
            }

            ioHandler.OutputNumber(result);
        }
    }

    public class DWordALU : LogicComponent {
        private IOHandler ioHandler;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() {
            uint ua = ioHandler.GetInputAs<uint>();
            int sa = ioHandler.GetInputAs<int>();
            uint b = ioHandler.GetInputAs<uint>(32);
            byte operation = ioHandler.GetInputAs<byte>(64);
            ulong result = 0;
            if (operation == 0) {
                result = ua + b;
            } else if (operation == 1) {
                result = ua - b;
            } else if (operation == 2) {
                result = ua & b;
            } else if (operation == 3) {
                result = ua | b;
            } else if (operation == 4) {
                result = ua ^ b;
            } else if (operation == 5) {
                result = ua << (int) b;
            } else if (operation == 6) {
                result = ua >> (int) b;
            } else if (operation == 7) {
                result = (ulong) sa >> (int) b;
            }

            ioHandler.OutputNumber(result);
        }
    }
}