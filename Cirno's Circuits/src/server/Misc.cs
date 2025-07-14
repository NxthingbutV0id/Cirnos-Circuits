using System;
using LogicAPI.Server.Components;

namespace CirnosCircuits {
    public class DecodeRom : LogicComponent {
        private IOHandler ioHandler;
        private readonly (byte, byte)[] masks = {
            (0b111001, 0b100001), // 100XX1
            (0b000111, 0b000100), // XXX100 
            (0b000111, 0b000110), // XXX110
            (0b111100, 0b110000), // 1100XX
            (0b111111, 0b100110), // 100110
            (0b101111, 0b100010), // 1X0010
            (0b111111, 0b000000), // 000000 
            (0b110000, 0b100000), // 10XXXX
            (0b000101, 0b000101), // XXX1X1
            (0b000111, 0b000000), // XXX000 
            (0b111111, 0b100010), // 100010
            (0b111111, 0b110010), // 110010
            (0b111100, 0b111000), // 1110XX
            (0b111111, 0b100110), // 100110
            (0b111000, 0b100000), // 100XXX  
            (0b111000, 0b101000), // 101XXX
            (0b111111, 0b110010), // 110010
            (0b111111, 0b111010), // 111010
            (0b111111, 0b101110), // 101110
            (0b101111, 0b100010), // 1X0010
            (0b111001, 0b101001), // 101XX1
            (0b111100, 0b101000), // 1010XX
            (0b100101, 0b000000), // 0XX0X0
            (0b111111, 0b001000), // 001000
            (0b101111, 0b000010), // 0X0010
            (0b111111, 0b011000), // 011000
            (0b101111, 0b001010), // 0X1010
            (0b111111, 0b010000), // 010000
            (0b111000, 0b011000), // 011XXX
            (0b111111, 0b001000), // 001000
            (0b110111, 0b010011), // 01X011 
            (0b000000, 0b000000), // XXXXXX 
            (0b000111, 0b000011), // XXX011
            (0b110111, 0b010000), // 01X000
            (0b000111, 0b000000), // XXX000
            (0b000000, 0b000000), // XXXXXX
            (0b000010, 0b000000), // XXXX0X 
            (0b111111, 0b010000), // 010000
            (0b110111, 0b000000), // 00X000
            (0b100100, 0b000000), // 0XX0XX
            (0b000111, 0b000100), // XXX100
            (0b000111, 0b000000), // XXX000
            (0b000111, 0b000100), // XXX100
            (0b000111, 0b000100), // XXX100
            (0b000110, 0b000110), // XXX11X
            (0b101111, 0b001010), // 0X1010
            (0b111000, 0b111000), // 111XXX
            (0b111000, 0b010000), // 010XXX
            (0b111000, 0b110000), // 110XXX
            (0b110100, 0b110000), // 11X0XX
            (0b011000, 0b011000), // X11XXX
            (0b111000, 0b111000), // 111XXX
            (0b111000, 0b001000), // 001XXX
            (0b110111, 0b010011), // 01X011
            (0b111000, 0b000000), // 000XXX
            (0b110000, 0b000000), // 00XXXX
            (0b111111, 0b100110), // 100110
            (0b111111, 0b100010), // 100010
            (0b011000, 0b011000), // X11XXX
            (0b100000, 0b000000), // 0XXXXX
            (0b100111, 0b000010), // 0XX010
            (0b111111, 0b011010), // 011010
            (0b000110, 0b000110), // XXX11X
            (0b000111, 0b000100), // XXX100
            (0b111000, 0b101000), // 101XXX
            (0b000000, 0b000000), // XXXXXX
            (0b111000, 0b001000), // 001XXX
            (0b111101, 0b001001), // 0010X1
            (0b100111, 0b000010), // 0XX010
            (0b111111, 0b101010), // 101010
            (0b111111, 0b101010), // 101010
            (0b110111, 0b010010), // 01X010
            (0b110000, 0b010000), // 01XXXX
            (0b111111, 0b001000), // 001000
            (0b000111, 0b000100), // XXX100
            (0b111111, 0b000000), // 000000
            (0b111111, 0b001000), // 001000
            (0b000011, 0b000001), // XXXX01
            (0b000011, 0b000000), // XXXX00
            (0b000000, 0b000000), // XXXXXX
            (0b000000, 0b000000), // XXXXXX
            (0b101111, 0b000000), // 0X0000
            (0b110111, 0b010011), // 01X011
            (0b100101, 0b000000), // 0XX0X0
            (0b111111, 0b011000), // 011000
            (0b000010, 0b000010), // XXXX1X
            (0b111000, 0b100000), // 100XXX
            (0b111111, 0b010010), // 010010
            (0b000111, 0b000100), // XXX100
            (0b100111, 0b000010), // 0XX010
            (0b000111, 0b000000), // XXX000
            (0b000010, 0b000010), // XXXX1X
            (0b000111, 0b000100), // XXX100
            (0b000110, 0b000110), // XXX11X
            (0b101111, 0b000000), // 0X0000
            (0b111111, 0b001000), // 001000
            (0b110111, 0b010011), // 01X011
            (0b110110, 0b110000), // 11X00X
            (0b110111, 0b000010), // 00X010
            (0b110111, 0b110011), // 11X011
            (0b111000, 0b110000), // 110XXX
            (0b011000, 0b011000), // X11XXX
            (0b110000, 0b000000), // 00XXXX
            (0b010000, 0b010000), // X1XXXX
            (0b111101, 0b001001), // 0010X1
            (0b111111, 0b000010), // 000010
            (0b111111, 0b000000), // 000000
            (0b111000, 0b100000), // 100XXX
            (0b000110, 0b000110), // XXX11X
            (0b000011, 0b000000), // XXXX00
            (0b000111, 0b000001), // XXX001
            (0b000111, 0b000011), // XXX011
            (0b000111, 0b000101), // XXX101
            (0b101111, 0b000010), // 0X0010
            (0b110111, 0b010000), // 01X000
            (0b111111, 0b001000), // 001000
            (0b110111, 0b010011), // 01X011
            (0b111111, 0b010011), // 010011
            (0b101111, 0b001010), // 0X1010
            (0b000111, 0b000100), // XXX100
            (0b111101, 0b001001), // 0010X1
            (0b111111, 0b010000), // 010000
            (0b111111, 0b001010), // 001010
            (0b100111, 0b000010), // 0XX010
            (0b111111, 0b101110), // 101110
            (0b110111, 0b000110), // 00X110
            (0b110111, 0b010110), // 01X110
            (0b110111, 0b110110), // 11X110
            (0b100000, 0b000000), // 0XXXXX
            (0b010000, 0b000000)  // X0XXXX
        };
        
        private readonly int[] group1Indexes = {
            1, 2, 9, 34, 40, 41, 42, 43, 47, 48, 50, 51, 54, 58, 59, 63, 64, 65, 66, 78, 86, 90, 92, 100, 101, 109
        };
        private readonly int[] group2Indexes = {
            7, 10, 11, 13, 14, 15, 16, 18, 28, 46, 52, 55, 57, 60, 68, 69, 71, 72, 98, 102, 103
        };
        private readonly int[] group3Indexes = {
            0, 3, 4, 5, 6, 12, 17, 19, 20, 21, 22, 23, 24, 25, 
            26, 27, 29, 30, 33, 37, 38, 39, 45, 49, 53, 56, 61, 
            67, 70, 73, 74, 75, 76, 81, 82, 83, 84, 87, 88, 89, 
            94, 95, 96, 97, 99, 104, 105, 106, 113, 114, 115, 116, 
            117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127
        };

        private readonly int[][] tStateIndexes = {
            new [] {
                3, 4, 5, 10, 11, 12, 13, 15, 18, 20, 21, 23, 24, 
                35, 47, 48, 49, 50, 51, 54, 56, 57, 61, 64, 65, 
                66, 67, 68, 69, 70, 71, 81, 82, 88, 120, 122, 125, 126, 127
            },
            new [] {
                16, 17, 58, 59, 60, 97, 98, 99, 100, 101, 104
            },
            new [] {
                2, 8, 9, 22, 29, 31, 32, 36, 43, 74, 75, 77, 78, 83, 85, 87, 105, 110, 113, 117
            },
            new [] {
                1, 26, 39, 40, 41, 44, 53, 76, 80, 91, 93, 111, 112, 118, 119
            },
            new [] {
                25, 34, 37, 38, 42, 62, 79, 92, 106, 108, 116, 121
            },
            new [] {
                6, 27, 63, 73, 84, 90, 109, 114, 115
            }
        };
        
        private bool[] rom = new bool[130];
        private readonly bool[] groups = new bool[130];
        private readonly bool[] states = new bool[130];

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);

            for (var i = 0; i < 130; i++) {
                groups[i] = true;
                states[i] = true;
            }
        }
        
        private void UpdateRom(byte instruction, bool[] tState) {
            var gp1 = (instruction & 1) != 0;
            var gp2 = (instruction & 2) != 0;
            var gp3 = (instruction & 3) == 0;

            foreach (var t in group1Indexes) { groups[t] = gp1; }
            foreach (var t in group2Indexes) { groups[t] = gp2; }
            foreach (var t in group3Indexes) { groups[t] = gp3; }

            for (var i = 0; i < 6; i++) {
                foreach (var j in tStateIndexes[i]) {
                    states[j] = tState[i];
                }
            }
            
            for (var i = 0; i < rom.Length; i++) {
                rom[i] = Bitmask(masks[i], instruction) && groups[i] && states[i];
            }
        }
        
        private static bool Bitmask((byte toCheck, byte toMatch) bits, byte value) {
            var temp = (value >> 2) & 0b111111;
            var match = bits.toMatch & bits.toCheck; // Just a sanity check
            return (temp & bits.toCheck) == match;
        }

        protected override void DoLogicUpdate() {
            ioHandler.ClearOutputs();
            var instruction = ioHandler.GetInputAs<byte>();
            var tState = ioHandler.GrabBoolArrayFromInput(0, 6);
            
            UpdateRom(instruction, tState);
            
            ioHandler.OutputBoolArray(rom);
        }
    }
    
    public class BATPU2ControlUnit : LogicComponent {
        private IOHandler ioHandler;
        private bool[] signals;
        private const int ADD = 0;
        private const int SUB = 1;
        private const int NOR = 2;
        private const int AND = 3;
        private const int XOR = 4;
        private const int RSH = 5;
        private const int UFR = 6;
        private const int Z = 7;
        private const int NZ = 8;
        private const int CS = 9;
        private const int CC = 10;
        private const int ALU_REG = 11;
        private const int ALU_IMM = 12;
        private const int SE_IMM = 13;
        private const int REG_EN = 14;
        private const int WR_A = 15;
        private const int WR_B = 16;
        private const int WR_C = 17;
        private const int STACK_EN = 18;
        private const int STACK_PUSH = 19;
        private const int STACK_POP = 20;
        private const int RAM_EN = 21;
        private const int RAM_R = 22;
        private const int RAM_W = 23;
        private const int PC_EN = 24;
        private const int IMM_DB = 25;
        private const int ALU_DB = 26;
        private const int RAM_DB = 27;
        private const int NPC_AB = 28;
        private const int IMM_AB = 29;
        private const int STACK_AB = 30;
        private const int HALT = 31;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
            signals = new bool[32];
        }

        protected override void DoLogicUpdate() {
            var input = ioHandler.GetInputAs<byte>();
            // Bits: RBCCOOOO
            // R: Reset, B: Branch, CC: Condition, OOOO: Opcode
            var opcode = input & 0xF;
            var condition = (input >> 4) & 0x3;
            var takeBranch = (input & 0b01000000) != 0;
            var reset = (input & 0b10000000) != 0;

            signals[ADD] = opcode is 2 or 9 or >= 14;
            signals[SUB] = opcode is 3;
            signals[NOR] = opcode is 4;
            signals[AND] = opcode is 5;
            signals[XOR] = opcode is 6;
            signals[RSH] = opcode is 7;
            signals[UFR] = opcode is <= 6 and >= 2 or 9;
            signals[Z] = condition is 0;
            signals[NZ] = condition is 1;
            signals[CS] = condition is 2;
            signals[CC] = condition is 3;
            signals[ALU_REG] = opcode is <= 7 and >= 2;
            signals[ALU_IMM] = opcode is 9 or >= 14;
            signals[SE_IMM] = opcode >= 14;
            signals[REG_EN] = opcode is <= 9 and >= 2 or 14;
            signals[WR_A] = opcode is 8 or 9;
            signals[WR_B] = opcode >= 14;
            signals[WR_C] = opcode is <= 7 and >= 2;
            signals[STACK_EN] = opcode is 12 or 13;
            signals[STACK_PUSH] = opcode is 12;
            signals[STACK_POP] = opcode is 13;
            signals[RAM_EN] = opcode >= 14;
            signals[RAM_R] = opcode is 14;
            signals[RAM_W] = opcode is 15;
            signals[PC_EN] = opcode is not 1;
            signals[IMM_DB] = opcode is 8;
            signals[ALU_DB] = opcode is <= 7 and >= 2 or 9;
            signals[RAM_DB] = opcode is 14;
            signals[NPC_AB] = opcode is not (10 or 11 or 12 or 13);
            signals[IMM_AB] = opcode is 10 or 12;
            signals[STACK_AB] = opcode is 13;
            signals[HALT] = opcode is 1;

            if (opcode == 11) {
                if (takeBranch) {
                    signals[NPC_AB] = false;
                    signals[IMM_AB] = true;
                } else {
                    signals[NPC_AB] = true;
                    signals[IMM_AB] = false;
                }
            }

            if (reset) {
                signals[PC_EN] = true;
                signals[NPC_AB] = false;
                signals[IMM_AB] = false;
                signals[STACK_AB] = false;
            }
            
            ioHandler.OutputBoolArray(signals);
        }
    }

    public abstract class DisplayUnit : LogicComponent {
        private IOHandler ioHandler;
        private bool[,] displayData, displayBuffer;
        private bool prevClk;
        private bool clk;
        protected abstract int width { get; }
        protected abstract int height { get; }
        private int pixelOutIndex;
        private byte x, y;
        private bool pixelValue, clearScreen, clearBuffer, pushBuffer, writePixel;
        private readonly int[] indexes = new int[7];
        private int bitMask;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
            displayData = new bool[width, height];
            displayBuffer = new bool[width, height]; // TODO: Might be unnecessary, but it makes the logic easier
            prevClk = false;
            pixelOutIndex = Outputs.Count - 1;
            SetIndexes();
            bitMask = (1 << indexes[0]) - 1; // Assuming the number of bits for x is the same as for y
        }

        protected override void DoLogicUpdate() {
            x = (byte)(ioHandler.GetInputAs<byte>() & bitMask);
            y = (byte)(ioHandler.GetInputAs<byte>(indexes[0]) & bitMask);
            pixelValue = Inputs[indexes[1]].On;
            clearScreen = Inputs[indexes[2]].On;
            clearBuffer = Inputs[indexes[3]].On;
            pushBuffer = Inputs[indexes[4]].On;
            writePixel = Inputs[indexes[5]].On;
            clk = Inputs[indexes[6]].On;
            if (!prevClk && clk) {
                if (writePixel) WritePixelToBuffer();
                if (clearScreen) ClearScreen();
                if (clearBuffer) ClearBuffer();
                if (pushBuffer) PushBufferToDisplay();
            }
            prevClk = clk;

            Outputs[pixelOutIndex].On = displayBuffer[x, y];
        }

        // These values are constant, so we can calculate them once at initialization
        private void SetIndexes() {
            var xSize = (int) Math.Ceiling(Math.Log2(width));
            var ySize = (int) Math.Ceiling(Math.Log2(height));
            indexes[0] = xSize;
            indexes[1] = xSize + ySize;
            indexes[2] = xSize + ySize + 1; // Clear Screen
            indexes[3] = xSize + ySize + 2; // Clear Buffer
            indexes[4] = xSize + ySize + 3; // Push Buffer
            indexes[5] = xSize + ySize + 4; // Write Pixel
            indexes[6] = xSize + ySize + 5; // Clock
        }

        // If the Push Buffer pin is high, The buffer will be pushed to the display.
        // The buffer will not be cleared
        private void PushBufferToDisplay() {
            for (var i = 0; i < width; i++) {
                for (var j = 0; j < height; j++) { displayData[i, j] = displayBuffer[i, j]; }
            }
            ioHandler.OutputBoolArray(displayData); // Update outputs
        }
        
        // If the Write Pixel pin is high, The pixel will be written to the buffer.
        // Note that clearing the buffer on the same tick will override this.
        private void WritePixelToBuffer() {
            if (x < width && y < height) {
                displayBuffer[x, y] = pixelValue;
            }
        }
        
        // If the Clear Buffer pin is high, The buffer will be cleared.
        // Clear Buffer has a higher priority than Write Pixel.
        private void ClearBuffer() {
            for (var i = 0; i < width; i++) {
                for (var j = 0; j < height; j++) { displayBuffer[i, j] = false; }
            }
        }
        
        // If the Clear Screen pin is high, The display will be cleared.
        // This does not affect the buffer.
        private void ClearScreen() {
            for (var i = 0; i < width; i++) {
                for (var j = 0; j < height; j++) { displayData[i, j] = false; }
            }
            ioHandler.ClearOutputs(); // Update outputs
        }
    }

    public class SquareDisplay32 : DisplayUnit {
        protected override int width => 32;
        protected override int height => 32;
    }

    public class GameBoyDisplay : DisplayUnit {
        protected override int width => 160;
        protected override int height => 144;
        
    }
    
    public class SquareDisplay64 : DisplayUnit {
        protected override int width => 64;
        protected override int height => 64;
    }

    public class StackUnit : LogicComponent {
        private IOHandler ioHandler;
        private ushort[] stack;
        private bool prevClk;
        private bool clk;
        private bool pop;
        private bool enable;
        private bool reset;
        private int stackPointer;
        private const int STACK_SIZE = 256;
        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
            stack = new ushort[STACK_SIZE];
            prevClk = false;
            stackPointer = 0;
        }

        protected override void DoLogicUpdate() {
            var address = ioHandler.GetInputAs<ushort>();
            clk = Inputs[16].On;
            pop = Inputs[17].On;
            enable = Inputs[18].On;
            reset = Inputs[19].On;

            if (reset) {
                for (var i = 0; i < STACK_SIZE; i++) {
                    stack[i] = 0;
                }
                stackPointer = 0;
                ioHandler.ClearOutputs();
                return;
            }

            var risingEdge = !prevClk && clk;
            ioHandler.ClearOutputs();
            if (!enable) return;
            if (risingEdge) {
                if (pop) {
                    Pop();
                } else {
                    Push(address);
                }
            } else {
                var outValue = stackPointer << 16 | Peek();
                ioHandler.OutputNumber(outValue);
            }
        }
        
        private void Push(ushort value) {
            stack[stackPointer++] = value;
            if (stackPointer >= STACK_SIZE) { stackPointer -= STACK_SIZE; }
        }
        
        private void Pop() {
            stackPointer--;
            if (stackPointer < 0) { stackPointer += STACK_SIZE; }
        }

        private ushort Peek() {
            return stack[stackPointer];
        }
    }

    public abstract class RomLoader : LogicComponent {
        // Loads the ROM data from a .hex file.
        protected static ushort[] LoadHexData(string fileName) {
            var buffer = new ushort[1024];
            try {
                var lines = System.IO.File.ReadAllLines(fileName);
                // We skip the first line, which is just a header
                for (var i = 1; i < lines.Length; i++) {
                    var line = lines[i].Trim();
                    var value = Convert.ToUInt16(line, 16);
                    buffer[i - 1] = value;
                }
            } catch (Exception ex) {
                Logger.Error("An error occurred while loading the ROM data: " + ex.Message);
            }
            return buffer;
        }
        
        // Loads the ROM data from a .mc file.
        protected static ushort[] LoadMcData(string fileName) {
            var buffer = new ushort[1024];
            try {
                var lines = System.IO.File.ReadAllLines(fileName);
                for (var i = 0; i < lines.Length; i++) {
                    var line = lines[i].Trim();
                    buffer[i] = Convert.ToUInt16(line, 2);
                }
            } catch (Exception ex) {
                Logger.Error("An error occurred while loading the ROM data: " + ex.Message);
            }
            return buffer;
        }
        
        // Loads the ROM data from a binary file.
        protected static byte[] LoadBinData(string fileName, int startAddress = 0) {
            var buffer = new byte[65536];
            try {
                var bytes = System.IO.File.ReadAllBytes(fileName);
                // If the file does not fit in the ROM
                for (var i = startAddress; i < bytes.Length + startAddress; i++) {
                    buffer[i] = bytes[i - startAddress];
                    if (i >= buffer.Length) {
                        Logger.Error("ROM data exceeds the address 0xFFFF. Data will be truncated.");
                        break;
                    }
                }
            } catch (Exception ex) {
                Logger.Error("An error occurred while loading the ROM data: " + ex.Message);
            }
            return buffer;
        }
    }

    public class ProgramRomBatPU : RomLoader {
        private IOHandler ioHandler;
        private ushort[] romData;
        private int prevRomSelect;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
            romData = new ushort[1024];
            prevRomSelect = -1;
        }

        protected override void DoLogicUpdate() {
            var address = ioHandler.GetInputAs<ushort>() & 0x3FF;
            var romSelect = ioHandler.GetInputAs<byte>(10) & 7;

            // To avoid reading the rom file every tick, we only update it when the romSelect changes
            if (romSelect != prevRomSelect) {
                romData = GetRomData(romSelect);
            }

            ioHandler.OutputNumber(romData[address]);
            
            prevRomSelect = romSelect;
        }
        
        // Programs are from https://github.com/mattbatwings/BatPU-2/tree/main/programs
        private static ushort[] GetRomData(int romSelect) {
            return romSelect switch {
                0 => LoadHexData("programs/tetris.hex"),
                1 => LoadHexData("programs/2048.hex"),
                2 => LoadHexData("programs/dvd.hex"),
                3 => LoadHexData("programs/gol.hex"),
                4 => LoadHexData("programs/helloworld.hex"),
                5 => LoadHexData("programs/minesweeper.hex"),
                6 => LoadMcData("programs/maze.mc"),
                _ => new ushort[1024]
            };
        }
    }
    
    public abstract class ProgramRom6502 : RomLoader {
        // TODO: Figure this out without making it confusing...
    }
}