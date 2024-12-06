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

            short a = ioHandler.GetInputAs<short>();
            short b = ioHandler.GetInputAs<short>(16);
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
            ioHandler.ClearOutputs();
            bool borrowIn = Inputs[16].On;

            sbyte a = ioHandler.GetInputAs<sbyte>();
            sbyte b = ioHandler.GetInputAs<sbyte>(8);
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
            ioHandler.ClearOutputs();
            bool borrowIn = Inputs[32].On;

            short a = ioHandler.GetInputAs<short>();
            short b = ioHandler.GetInputAs<short>(16);
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
            ioHandler.ClearOutputs();
            bool signed = Inputs[16].On;

            if (signed) {
                sbyte divisor = ioHandler.GetInputAs<sbyte>();
                sbyte dividend = ioHandler.GetInputAs<sbyte>(8);

                (sbyte quotient, sbyte remainder) = Math.DivRem(dividend, divisor);
                
                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 8);
            } else {
                byte divisor = ioHandler.GetInputAs<byte>();
                byte dividend = ioHandler.GetInputAs<byte>(8);

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
            ioHandler.ClearOutputs();
            bool signed = Inputs[32].On;

            if (signed) {
                short divisor = ioHandler.GetInputAs<short>();
                short dividend = ioHandler.GetInputAs<short>(16);

                (short quotient, short remainder) = Math.DivRem(dividend, divisor);
                
                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 16);
            } else {
                ushort divisor = ioHandler.GetInputAs<ushort>();
                ushort dividend = ioHandler.GetInputAs<ushort>(16);

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
            ioHandler.ClearOutputs();
            bool signed = Inputs[64].On;

            if (signed) {
                int divisor = ioHandler.GetInputAs<int>();
                int dividend = ioHandler.GetInputAs<int>(32);

                (int quotient, int remainder) = Math.DivRem(dividend, divisor);
                
                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 32);
            } else {
                uint divisor = ioHandler.GetInputAs<uint>();
                uint dividend = ioHandler.GetInputAs<uint>(32);

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
            ioHandler.ClearOutputs();
            bool signed = Inputs[128].On;

            if (signed) {
                long divisor = ioHandler.GetInputAs<long>();
                long dividend = ioHandler.GetInputAs<long>(64);

                (long quotient, long remainder) = Math.DivRem(dividend, divisor);
                
                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 64);
            } else {
                ulong divisor = ioHandler.GetInputAs<ulong>();
                ulong dividend = ioHandler.GetInputAs<ulong>(64);

                (ulong quotient, ulong remainder) = Math.DivRem(dividend, divisor);
                
                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 64);
            }
        }
    }
    
    public enum Operation {
        ADD, SUB,
        AND, OR, XOR,
        LSL, LSR, ASR
    }
    
    public class ByteArithmeticLogicUnit: LogicComponent {
        private IOHandler ioHandler;
        private readonly int A = 0;
        private readonly int B = 8;
        private readonly int Opcode = 17;
        private readonly int OverflowFlag = 0;
        private readonly int ZeroFlag = 1;
        private readonly int NegativeFlag = 2;
        private readonly int Cin = 16;
        private readonly int Output = 3;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() {
            Operation op = (Operation)ioHandler.GetInputAs<int>(Opcode);
            ioHandler.ClearOutputs();
            byte a = ioHandler.GetInputAs<byte>(A);
            byte b = ioHandler.GetInputAs<byte>(B);
            bool cin = Inputs[Cin].On;
            int o = 0;

            switch (op) {
                case Operation.ADD:
                    o = cin? a + b + 1 : a + b;
                    Outputs[OverflowFlag].On = o > 0xFF;
                    Outputs[ZeroFlag].On = o == 0;
                    Outputs[NegativeFlag].On = o < 0;
                    break;
                case Operation.SUB:
                    o = cin? a - b - 1 : a - b;
                    Outputs[OverflowFlag].On = o > 0x7F;
                    Outputs[ZeroFlag].On = o == 0;
                    Outputs[NegativeFlag].On = o < 0;
                    break;
                case Operation.AND:
                    o = a & b;
                    Outputs[OverflowFlag].On = o > 0xFF;
                    Outputs[ZeroFlag].On = o == 0;
                    Outputs[NegativeFlag].On = o < 0;
                    break;
                case Operation.OR:
                    o = a | b;
                    Outputs[OverflowFlag].On = o > 0xFF;
                    Outputs[ZeroFlag].On = o == 0;
                    Outputs[NegativeFlag].On = o < 0;
                    break;
                case Operation.XOR:
                    o = a ^ b;
                    Outputs[OverflowFlag].On = o > 0xFF;
                    Outputs[ZeroFlag].On = o == 0;
                    Outputs[NegativeFlag].On = o < 0;
                    break;
                case Operation.LSL:
                    o = a << b;
                    Outputs[OverflowFlag].On = (o & 0x100) != 0;
                    Outputs[ZeroFlag].On = o == 0;
                    Outputs[NegativeFlag].On = o < 0;
                    break;
                case Operation.LSR:
                    o = (int)((uint)a >> b);
                    Outputs[OverflowFlag].On = o > 0xFF;
                    Outputs[ZeroFlag].On = o == 0;
                    Outputs[NegativeFlag].On = o < 0;
                    break;
                case Operation.ASR:
                    o = a >> b;
                    Outputs[OverflowFlag].On = o > 0x7F;
                    Outputs[ZeroFlag].On = o == 0;
                    Outputs[NegativeFlag].On = o < 0;
                    break;
            }
            
            o &= 0xFF;
            ioHandler.OutputNumber(o, Output);
        }
    }
}