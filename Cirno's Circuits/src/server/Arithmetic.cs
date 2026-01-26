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
                sbyte quotient, remainder;
                var a = ioHandler.GetInputAs<sbyte>();
                var b = ioHandler.GetInputAs<sbyte>(8);
                
                if (a == sbyte.MinValue && b == -1) {
                    (quotient, remainder) = (a, 0);
                } else if (b == 0) {
                    (quotient, remainder) = (0, -1);
                } else {
                    (quotient, remainder) = Math.DivRem(a, b);
                }

                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 8);
            } else {
                byte quotient = 0, remainder = 0xff;
                var a = ioHandler.GetInputAs<byte>();
                var b = ioHandler.GetInputAs<byte>(8);
                
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
                short quotient, remainder;
                var a = ioHandler.GetInputAs<short>();
                var b = ioHandler.GetInputAs<short>(16);
                
                if (a == short.MinValue && b == -1) {
                    (quotient, remainder) = (a, 0);
                } else if (b == 0) {
                    (quotient, remainder) = (0, -1);
                } else {
                    (quotient, remainder) = Math.DivRem(a, b);
                }
            
                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 16);
            } else {
                ushort quotient = 0, remainder = 0xffff;
                var a = ioHandler.GetInputAs<ushort>();
                var b = ioHandler.GetInputAs<ushort>(16);
                
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
                var a = ioHandler.GetInputAs<int>();
                var b = ioHandler.GetInputAs<int>(32);
                int quotient, remainder;
                if (a == int.MinValue && b == -1) {
                    (quotient, remainder) = (a, 0);
                } else {
                    (quotient, remainder) = b == 0 ? (0, -1) : Math.DivRem(a, b);
                }

                ioHandler.OutputNumber(quotient);
                ioHandler.OutputNumber(remainder, 32);
            } else {
                var a = ioHandler.GetInputAs<uint>();
                var b = ioHandler.GetInputAs<uint>(32);
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

    public class RISCVALU : LogicComponent {
        private IOHandler ioHandler;
        private const int AND    = 0b00000;
        private const int OR     = 0b00001;
        private const int ADD    = 0b00010;
        private const int XOR    = 0b00011;
        private const int SUB    = 0b00110;
        private const int SLT    = 0b00111;
        private const int SRL    = 0b01000;
        private const int SRA    = 0b01001;
        private const int SLTU   = 0b01010;
        private const int SLL    = 0b01011;
        private const int MUL    = 0b01100;
        private const int MULH   = 0b01101;
        private const int MULHU  = 0b01110;
        private const int MULHSU = 0b01111;
        private const int DIV    = 0b10000;
        private const int DIVU   = 0b10001;
        private const int REM    = 0b10010;
        private const int REMU   = 0b10011;
        
        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() {
            var a = ioHandler.GetInputAs<int>();
            var b = ioHandler.GetInputAs<int>(32);
            var opcode = ioHandler.GetInputAs<byte>(64) & 0b11111;
            var tempA = (long)a;
            var tempB = (long)(uint)b;
            var usA = (ulong)(uint)a;
            var usB = (ulong)(uint)b;
            var seA = (long)a;
            var seB = (long)b;
            var uA = (uint)a;
            var uB = (uint)b;
            
            var result = opcode switch {
                AND => a & b,                     
                OR => a | b,                     
                ADD => a + b,                     
                XOR => a ^ b,                     
                SUB => a - b,                     
                SLT => a < b ? 1 : 0,           
                SRL => a >>> (b & 0x1F), 
                SRA => a >> (b & 0x1F), 
                SLTU => uA < uB ? 1 : 0,
                SLL => a << (b & 0x1F),           
                MUL => a * b,                     
                MULH => (int)((seA * seB) >> 32), 
                MULHU => (int)((usA * usB) >> 32), 
                MULHSU => (int)((tempA * tempB) >> 32), 
                DIV => b == 0 ? -1 : a / b,       
                DIVU => b == 0 ? -1 : (int)(uA / uB), 
                REM => b == 0 ? a : a % b,        
                REMU => b == 0 ? (int)(uint)a : (int)(uA % uB), 
                _ => 0
            };
            
            ioHandler.ClearOutputs();
            ioHandler.OutputNumber(result);
        }
    }
}