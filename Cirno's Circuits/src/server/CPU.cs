using System;
using LogicAPI.Server.Components;

namespace CirnosCircuits {
    public enum Opcode {
        NOP, HLT, ADD, SUB, NOR, AND, XOR, RSH, LDI, ADI, JMP, BRH, CAL, RET, LDR, STR
    }

    public class BatPU2 : LogicComponent {
        private IOHandler ioHandler;
        private ushort[] callStack;
        private int stackPointer;
        private byte[] registerFile;
        private ushort instruction;
        private ushort programCounter;
        private bool clk, prevClk;
        private bool carryFlag, zeroFlag;
        private int regA, regB, regC, condition, address;
        private byte immediate;
        private sbyte offset;
        private bool inWaitingState;
        private byte addressOut, dataOut;
        private bool halted, readEnable, writeEnable;
        
        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
            callStack = new ushort[16];
            stackPointer = 0;
            registerFile = new byte[16];
            instruction = 0;
            programCounter = 0;
            prevClk = false;
            carryFlag = false;
            zeroFlag = false;
            regA = 0;
            regB = 0;
            regC = 0;
            condition = 0;
            address = 0;
            immediate = 0;
            offset = 0;
            inWaitingState = false;
            addressOut = 0;
            dataOut = 0;
            halted = false;
            readEnable = false;
            writeEnable = false;
        }
        
        protected override void DoLogicUpdate() {
            // Pin Mapping:
            // Inputs[0-15] = Current Instruction (16 bits)
            // Inputs[16-23] = Data In (8 bits)
            // Inputs[24] = Clock
            // Inputs[25] = Reset
            //
            // Outputs[0-9] = Program Counter (10 bits)
            // Outputs[10-17] = Data Out (8 bits)
            // Outputs[18-25] = Address Out (8 bits)
            // Outputs[26] = Halt Signal
            // Outputs[27] = Read Enable 
            // Outputs[28] = Write Enable
            
            clk = Inputs[24].On;
            var reset = Inputs[25].On;
            var dataIn = ioHandler.GetInputAs<byte>(16);
            var risingEdge = !prevClk && clk;
            
            if (reset && risingEdge) {
                Reset();
                return;
            }
            
            if (risingEdge) {
                if (!inWaitingState) {
                    instruction = ioHandler.GetInputAs<ushort>();
                }
                
                Execute(dataIn);
            }
            prevClk = clk;
        }
        
        private void Reset() {
            for (var i = 0; i < registerFile.Length; i++) {
                registerFile[i] = 0;
            }
            callStack = new ushort[16];
            stackPointer = 0;
            registerFile = new byte[16];
            instruction = 0;
            programCounter = 0;
            prevClk = false;
            carryFlag = false;
            zeroFlag = false;
            regA = 0;
            regB = 0;
            regC = 0;
            condition = 0;
            address = 0;
            immediate = 0;
            offset = 0;
            inWaitingState = false;
            addressOut = 0;
            dataOut = 0;
            halted = false;
            readEnable = false;
            writeEnable = false;
            
            ioHandler.ClearOutputs();
        }
        
        private Opcode Decode() {
            var op = (instruction >> 12) & 0xF;
            regA = (instruction >> 8) & 0xF;
            regB = (instruction >> 4) & 0xF;
            regC = instruction & 0xF;
            condition = (instruction >> 10) & 0x3;
            address = instruction & 0x3FF;
            immediate = (byte)(instruction & 0xFF);
            offset = (sbyte)(instruction & 0xF);
            if ((offset & 0b1000) != 0) {
                offset = (sbyte)(offset | 0xF0); // Sign extend if negative
            }

            return op switch {
                0 => Opcode.NOP,
                1 => Opcode.HLT,
                2 => Opcode.ADD,
                3 => Opcode.SUB,
                4 => Opcode.NOR,
                5 => Opcode.AND,
                6 => Opcode.XOR,
                7 => Opcode.RSH,
                8 => Opcode.LDI,
                9 => Opcode.ADI,
                10 => Opcode.JMP,
                11 => Opcode.BRH,
                12 => Opcode.CAL,
                13 => Opcode.RET,
                14 => Opcode.LDR,
                15 => Opcode.STR,
                _ => Opcode.NOP // Never reached, but keeps the compiler happy
            };
        }

        private void Execute(byte dataIn) {
            var opcode = Decode();
            switch (opcode) {
                case Opcode.NOP: NOP(); break;
                case Opcode.HLT: HLT(); break;
                case Opcode.ADD: ADD(); break;
                case Opcode.SUB: SUB(); break;
                case Opcode.NOR: NOR(); break;
                case Opcode.AND: AND(); break;
                case Opcode.XOR: XOR(); break;
                case Opcode.RSH: RSH(); break;
                case Opcode.LDI: LDI(); break;
                case Opcode.ADI: ADI(); break;
                case Opcode.JMP: JMP(); break;
                case Opcode.BRH: BRH(); break;
                case Opcode.CAL: CAL(); break;
                case Opcode.RET: RET(); break;
                case Opcode.LDR: LDR(dataIn); break;
                case Opcode.STR: STR(); break;
            }
            OutputData();
        }

        private void OutputData() {
            ioHandler.ClearOutputs();
            programCounter = (ushort)(programCounter & 0x3FF); // Ensure PC is within bounds
            var value = programCounter | (ulong)dataOut << 10 | (ulong)addressOut << 18;
            ioHandler.OutputNumber(value);
            Outputs[26].On = halted;
            Outputs[27].On = readEnable;
            Outputs[28].On = writeEnable;
        }

        private void NOP() {
            programCounter++;
        }
        
        private void HLT() {
            halted = true;
        }
        
        private void ADD() {
            var a = ReadRegister(regA);
            var b = ReadRegister(regB);
            var temp = a + b;
            if (temp > 255) {
                carryFlag = true;
                temp &= 0xFF;
            } else {
                carryFlag = false;
            }
            zeroFlag = temp == 0;
            WriteRegister(regC, (byte)temp);
            programCounter++;
        }

        private void SUB() {
            var a = ReadRegister(regA);
            var b = ReadRegister(regB);
            var temp = a - b;
            carryFlag = temp >= 0; // Carry flag is true if no borrow occurred
            temp &= 0xFF;
            zeroFlag = temp == 0;
            WriteRegister(regC, (byte)temp);
            programCounter++;
        }
        
        private void NOR() {
            var a = ReadRegister(regA);
            var b = ReadRegister(regB);
            var result = (byte)~(a | b);
            zeroFlag = result == 0;
            WriteRegister(regC, result);
            programCounter++;
        }
        
        private void AND() {
            var a = ReadRegister(regA);
            var b = ReadRegister(regB);
            var result = (byte)(a & b);
            zeroFlag = result == 0;
            WriteRegister(regC, result);
            programCounter++;
        }
        
        private void XOR() {
            var a = ReadRegister(regA);
            var b = ReadRegister(regB);
            var result = (byte)(a ^ b);
            zeroFlag = result == 0;
            WriteRegister(regC, result);
            programCounter++;
        }
        
        private void RSH() {
            var a = ReadRegister(regA);
            var result = (byte)(a >> 1);
            WriteRegister(regC, result);
            programCounter++;
        }
        
        private void LDI() {
            WriteRegister(regC, immediate);
            programCounter++;
        }
        
        private void ADI() {
            var a = ReadRegister(regA);
            var temp = a + immediate;
            if (temp > 255) {
                carryFlag = true;
                temp &= 0xFF;
            } else {
                carryFlag = false;
            }
            zeroFlag = temp == 0;
            WriteRegister(regC, (byte)temp);
            programCounter++;
        }
        
        private void JMP() {
            programCounter = (ushort)address;
        }
        
        private void BRH() {
            var beq = condition == 0 && zeroFlag;
            var bne = condition == 1 && !zeroFlag;
            var bcs = condition == 2 && carryFlag;
            var bcc = condition == 3 && !carryFlag;
            
            if (beq || bne || bcs || bcc) {
                programCounter = (ushort)address;
            } else {
                programCounter++;
            }
        }
        
        private void CAL() {
            callStack[stackPointer] = (ushort)(programCounter + 1);
            stackPointer = (stackPointer + 1) & 0xf;
            programCounter = (ushort)address;
        }
        
        private void RET() {
            stackPointer = (stackPointer - 1) & 0xf;
            programCounter = callStack[stackPointer];
        }
        
        private void LDR(byte dataIn) {
            if (inWaitingState) {
                WriteRegister(regB, dataIn);
                inWaitingState = false;
                programCounter++;
            } else { // First time execution
                addressOut = (byte)((ReadRegister(regA) + offset) & 0xFF);
                dataOut = 0; // Make sure nothing is on the bus
                readEnable = true;
                writeEnable = false;
                inWaitingState = true;
            }
        }
        
        // TODO: I am not sure how to verify that this is correct but that's a problem for future me
        private void STR() {
            if (inWaitingState) {
                inWaitingState = false;
                programCounter++;
            } else {
                addressOut = (byte)((ReadRegister(regA) + offset) & 0xFF);
                dataOut = ReadRegister(regB);
                writeEnable = true;
                readEnable = false;
                inWaitingState = true;
            }
        }

        private byte ReadRegister(int regIndex) {
            return registerFile[regIndex];
        }

        private void WriteRegister(int regIndex, byte value) {
            if (regIndex == 0) { return; }
            registerFile[regIndex] = value;
        }
    }
}