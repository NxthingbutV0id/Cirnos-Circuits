using System;
using LogicAPI.Server.Components;

namespace CirnosCircuits {
    public class ByteAdder: LogicComponent {
        private IOHandler ioHandler;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() {
            bool carryIn = Inputs[16].On;

            byte a = ioHandler.GetInputAsU8();
            byte b = ioHandler.GetInputAsU8(8);
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
            bool carryIn = Inputs[32].On;

            short a = ioHandler.GetInputAsI16();
            short b = ioHandler.GetInputAsI16(16);
            int result = carryIn ? a + b + 1 : a + b;
            
            ioHandler.OutputNumber(result);
        }
    }
    
    public class ByteSubtract: LogicComponent {
        private IOHandler ioHandler;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }
        
        protected override void DoLogicUpdate() {
            bool borrowIn = Inputs[16].On;

            sbyte a = ioHandler.GetInputAsI8();
            sbyte b = ioHandler.GetInputAsI8(8);
            int result = borrowIn ? a - b - 1 : a - b;
            
            ioHandler.OutputNumber(result);
        }
    }
    
    public class WordSubtract: LogicComponent {
        private IOHandler ioHandler;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() {
            bool borrowIn = Inputs[32].On;

            short a = ioHandler.GetInputAsI16();
            short b = ioHandler.GetInputAsI16(16);
            int result = borrowIn ? a - b - 1 : a - b;
            
            ioHandler.OutputNumber(result);
        }
    }

    public class ByteDivider: LogicComponent {
        private IOHandler ioHandler;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() {
            bool signed = Inputs[16].On;

            if (signed) {
                sbyte divisor = ioHandler.GetInputAsI8();
                sbyte dividend = ioHandler.GetInputAsI8(8);

                (sbyte quotient, sbyte remainder) = Math.DivRem(dividend, divisor);
                
                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 8);
            } else {
                byte divisor = ioHandler.GetInputAsU8();
                byte dividend = ioHandler.GetInputAsU8(8);

                (byte quotient, byte remainder) = Math.DivRem(dividend, divisor);
                
                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 8);
            }
        }
    }
    
    public class WordDivider: LogicComponent {
        private IOHandler ioHandler;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() {
            bool signed = Inputs[32].On;

            if (signed) {
                short divisor = ioHandler.GetInputAsI16();
                short dividend = ioHandler.GetInputAsI16(16);

                (short quotient, short remainder) = Math.DivRem(dividend, divisor);
                
                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 16);
            } else {
                ushort divisor = ioHandler.GetInputAsU16();
                ushort dividend = ioHandler.GetInputAsU16(16);

                (ushort quotient, ushort remainder) = Math.DivRem(dividend, divisor);
                
                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 16);
            }
        }
    }
    
    public class DWordDivider: LogicComponent {
        private IOHandler ioHandler;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() {
            bool signed = Inputs[64].On;

            if (signed) {
                int divisor = ioHandler.GetInputAsI32();
                int dividend = ioHandler.GetInputAsI32(32);

                (int quotient, int remainder) = Math.DivRem(dividend, divisor);
                
                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 32);
            } else {
                uint divisor = ioHandler.GetInputAsU32();
                uint dividend = ioHandler.GetInputAsU32(32);

                (uint quotient, uint remainder) = Math.DivRem(dividend, divisor);
                
                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 32);
            }
        }
    }
    
    public class QWordDivider: LogicComponent {
        private IOHandler ioHandler;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() {
            bool signed = Inputs[128].On;

            if (signed) {
                long divisor = ioHandler.GetInputAsI64();
                long dividend = ioHandler.GetInputAsI64(64);

                (long quotient, long remainder) = Math.DivRem(dividend, divisor);
                
                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 64);
            } else {
                ulong divisor = ioHandler.GetInputAsU64();
                ulong dividend = ioHandler.GetInputAsU64(64);

                (ulong quotient, ulong remainder) = Math.DivRem(dividend, divisor);
                
                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 64);
            }
        }
    }
}