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

    public class ByteDivider : LogicComponent {
        private IOHandler ioHandler;
        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }
        
        protected override void DoLogicUpdate() {
            ioHandler.ClearOutputs();
            if (Inputs[16].On) {
                sbyte quotient = 0, remainder = 0;
                sbyte a = ioHandler.GetInputAs<sbyte>();
                sbyte b = ioHandler.GetInputAs<sbyte>(8);
                
                if (b != 0) {
                    (quotient, remainder) = Math.DivRem(a, b);
                }
            
                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 8);
            } else {
                byte quotient = 0, remainder = 0xff;
                byte a = ioHandler.GetInputAs<byte>();
                byte b = ioHandler.GetInputAs<byte>(8);
                
                if (b != 0) {
                    (quotient, remainder) = Math.DivRem(a, b);
                }
            
                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 8);
            }
        }
    }
    
    public class WordDivider : LogicComponent {
        private IOHandler ioHandler;
        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }
        
        protected override void DoLogicUpdate() {
            ioHandler.ClearOutputs();
            if (Inputs[32].On) {
                short quotient = 0, remainder = 0;
                short a = ioHandler.GetInputAs<short>();
                short b = ioHandler.GetInputAs<short>(16);
                
                if (b != 0) {
                    (quotient, remainder) = Math.DivRem(a, b);
                }
            
                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 16);
            } else {
                ushort quotient = 0, remainder = 0xffff;
                ushort a = ioHandler.GetInputAs<ushort>();
                ushort b = ioHandler.GetInputAs<ushort>(16);
                
                if (b != 0) {
                    (quotient, remainder) = Math.DivRem(a, b);
                }
            
                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 16);
            }
        }
    }
    
    public class DWordDivider : LogicComponent {
        private IOHandler ioHandler;
        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }
        
        protected override void DoLogicUpdate() {
            ioHandler.ClearOutputs();
            if (Inputs[64].On) {
                int a = ioHandler.GetInputAs<int>();
                int b = ioHandler.GetInputAs<int>(32);
                (int quotient, int remainder) = b == 0 ? (0, 0) : Math.DivRem(a, b);
            
                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 32);
            } else {
                uint a = ioHandler.GetInputAs<uint>();
                uint b = ioHandler.GetInputAs<uint>(32);
                (uint quotient, uint remainder) = b == 0 ? (0, 0xffffffff) : Math.DivRem(a, b);
            
                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 32);
            }
        }
    }

    public class ByteMultiplier : LogicComponent {
        private IOHandler ioHandler;
        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }
        
        protected override void DoLogicUpdate() {
            ioHandler.ClearOutputs();
            if (Inputs[16].On) {
                sbyte a = ioHandler.GetInputAs<sbyte>();
                sbyte b = ioHandler.GetInputAs<sbyte>(8);
                short result = (short) (a * b);
            
                ioHandler.OutputNumber(result);
            } else {
                byte a = ioHandler.GetInputAs<byte>();
                byte b = ioHandler.GetInputAs<byte>(8);
                ushort result = (ushort) (a * b);
            
                ioHandler.OutputNumber(result);
            }
        }
    }
    
    public class WordMultiplier : LogicComponent {
        private IOHandler ioHandler;
        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }
        
        protected override void DoLogicUpdate() {
            ioHandler.ClearOutputs();
            if (Inputs[32].On) {
                short a = ioHandler.GetInputAs<short>();
                short b = ioHandler.GetInputAs<short>(16);
                int result = a * b;
            
                ioHandler.OutputNumber(result);
            } else {
                ushort a = ioHandler.GetInputAs<ushort>();
                ushort b = ioHandler.GetInputAs<ushort>(16);
                uint result = (uint) a * b;
            
                ioHandler.OutputNumber(result);
            }
        }
    }
    
    public class DWordMultiplier : LogicComponent {
        private IOHandler ioHandler;
        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }
        
        protected override void DoLogicUpdate() {
            ioHandler.ClearOutputs();
            if (Inputs[64].On) {
                int a = ioHandler.GetInputAs<int>();
                int b = ioHandler.GetInputAs<int>(32);
                long result = (long) a * b;
            
                ioHandler.OutputNumber(result);
            } else {
                uint a = ioHandler.GetInputAs<uint>();
                uint b = ioHandler.GetInputAs<uint>(32);
                ulong result = (ulong) a * b;
            
                ioHandler.OutputNumber(result);
            }
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