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
        

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
        }

        protected override void DoLogicUpdate() {
            //TODO: Implement
        }
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
            ioHandler.ClearOutputs();
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
                0 => LoadHexData(@"C:\Program Files (x86)\Steam\steamapps\common\Logic World\GameData\Cirno's Circuits\programs\tetris.hex"),
                1 => LoadHexData(@"C:\Program Files (x86)\Steam\steamapps\common\Logic World\GameData\Cirno's Circuits\programs\2048.hex"),
                2 => LoadHexData(@"C:\Program Files (x86)\Steam\steamapps\common\Logic World\GameData\Cirno's Circuits\programs\dvd.hex"),
                3 => LoadHexData(@"C:\Program Files (x86)\Steam\steamapps\common\Logic World\GameData\Cirno's Circuits\programs\gol.hex"),
                4 => LoadHexData(@"C:\Program Files (x86)\Steam\steamapps\common\Logic World\GameData\Cirno's Circuits\programs\helloworld.hex"),
                5 => LoadHexData(@"C:\Program Files (x86)\Steam\steamapps\common\Logic World\GameData\Cirno's Circuits\programs\minesweeper.hex"),
                6 => LoadMcData(@"C:\Program Files (x86)\Steam\steamapps\common\Logic World\GameData\Cirno's Circuits\programs\maze.mc"),
                _ => new ushort[1024]
            };
        }
    }
    
    public class SerialToParallel : LogicComponent {
        private IOHandler ioHandler;
        private bool[] dataBuffer;
        private int bitIndex;
        private bool clk, prevClk, enable;
        private const int DataIndex = 0;
        private const int ClockIndex = 1;
        private const int EnableIndex = 2;

        protected override void Initialize() {
            ioHandler = new IOHandler(Inputs, Outputs);
            dataBuffer = new bool[8];
            bitIndex = 0;
            prevClk = false;
            enable = false;
        }

        protected override void DoLogicUpdate() {
            enable = Inputs[EnableIndex].On;
            if (enable && bitIndex == 8) {
                prevClk = clk;
                return;
            }
            if (!enable) {
                bitIndex = 0;
                prevClk = clk;
                return;
            }
            
            ioHandler.ClearOutputs();
            clk = Inputs[ClockIndex].On;
            if (!prevClk && clk) {
                dataBuffer[bitIndex] = Inputs[DataIndex].On;
                bitIndex++;
            }

            if (bitIndex == 8) { ioHandler.OutputBoolArray(dataBuffer); }

            prevClk = clk;
        }
    }
}