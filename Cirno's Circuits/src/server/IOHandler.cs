using System;
using LogicAPI.Server.Components;
using System.Collections.Generic;

namespace CirnosCircuits {
	public class IOHandler {
		private readonly IReadOnlyList<IInputPeg> inputs;
		private readonly IReadOnlyList<IOutputPeg> outputs;

		public IOHandler(IReadOnlyList<IInputPeg> inputs, IReadOnlyList<IOutputPeg> outputs) {
			this.inputs = inputs;
			this.outputs = outputs;
		}

		public void ClearOutputs() {
			foreach (var peg in outputs) {
				peg.On = false;
			}
		}

		public ulong GetInputAsU64(int offset = 0) {
			ulong result = 0ul;
			for (int i = 0; i < 64; i++) {
				if (i + offset >= inputs.Count) break;

				if (inputs[i + offset].On) {
					result |= 1ul << i;
				}
			}
			return result;
		}
		
		public long GetInputAsI64(int offset = 0) {
			long result = 0L;
			for (int i = 0; i < 64; i++) {
				if (i + offset >= inputs.Count) break;

				if (inputs[i + offset].On) {
					result |= 1L << i;
				}
			}
			return result;
		}
		
		public uint GetInputAsU32(int offset = 0) {
			uint result = 0u;
			for (int i = 0; i < 32; i++) {
				if (i + offset >= inputs.Count) break;

				if (inputs[i + offset].On) {
					result |= 1u << i;
				}
			}
			return result;
		}
		
		public int GetInputAsI32(int offset = 0) {
			int result = 0;
			for (int i = 0; i < 32; i++) {
				if (i + offset >= inputs.Count) break;

				if (inputs[i + offset].On) {
					result |= 1 << i;
				}
			}
			return result;
		}
		
		public ushort GetInputAsU16(int offset = 0) {
			ushort result = 0;
			for (int i = 0; i < 16; i++) {
				if (i + offset >= inputs.Count) break;

				if (inputs[i + offset].On) {
					result |= (ushort)(1 << i);
				}
			}
			return result;
		}
		
		public short GetInputAsI16(int offset = 0) {
			short result = 0;
			for (int i = 0; i < 16; i++) {
				if (i + offset >= inputs.Count) break;

				if (inputs[i + offset].On) {
					result |= (short)(1 << i);
				}
			}
			return result;
		}
		
		public byte GetInputAsU8(int offset = 0) {
			byte result = 0;
			for (int i = 0; i < 8; i++) {
				if (i + offset >= inputs.Count) break;

				if (inputs[i + offset].On) {
					result |= (byte)(1 << i);
				}
			}
			return result;
		}
		
		public sbyte GetInputAsI8(int offset = 0) {
			sbyte result = 0;
			for (int i = 0; i < 8; i++) {
				if (i + offset >= inputs.Count) break;

				if (inputs[i + offset].On) {
					result |= (sbyte)(1 << i);
				}
			}
			return result;
		}

		public bool[] GrabBoolArrayFromInput(int offset = 0) {
			bool[] data = new bool[inputs.Count];

			for (int i = offset; i < data.Length; i++) {
				data[i] = inputs[i].On;
			}

            return data;
        }

        public bool[] GrabBoolArrayFromInput(int startPin, int endPin) {
	        bool[] data = new bool[endPin - startPin];

	        for (int i = startPin; i < endPin && i < inputs.Count; i++) {
		        data[i - startPin] = inputs[i].On;
	        }

            return data;
        }

        public void OutputNumber(float number, int offset = 0) {
	        uint n = BitConverter.SingleToUInt32Bits(number);
	        OutputNumber(n, offset);
        }
        
        public void OutputNumber(double number, int offset = 0) {
	        ulong n = BitConverter.DoubleToUInt64Bits(number);
	        OutputNumber(n, offset);
        }

        public void OutputNumber(ulong number, int offset = 0) {
	        for (int i = offset; i < sizeof(ulong) * 8 && i < outputs.Count; i++) {
		        outputs[i].On = ((number >> (i - offset)) & 1) == 1;
	        }
		}

		public void OutputNumber(long number, int offset = 0) {
			for (int i = offset; i < sizeof(long) * 8 && i < outputs.Count; i++) {
				outputs[i].On = ((number >> i) & 1) == 1;
			}
		}

		public void OutputNumber(uint number, int offset = 0) {
			for (int i = offset; i < sizeof(uint) * 8 && i < outputs.Count; i++) {
				outputs[i].On = ((number >> i) & 1) == 1;
			}
		}

        public void OutputNumber(int number, int offset = 0) {
	        for (int i = offset; i < sizeof(int) * 8 && i < outputs.Count; i++) {
		        outputs[i].On = ((number >> i) & 1) == 1;
	        }
		}

        public void OutputNumber(ushort number, int offset = 0) {
	        for (int i = offset; i < sizeof(ushort) * 8 && i < outputs.Count; i++) {
		        outputs[i].On = ((number >> i) & 1) == 1;
	        }
		}

        public void OutputNumber(short number, int offset = 0) {
	        for (int i = offset; i < sizeof(short) * 8 && i < outputs.Count; i++) {
		        outputs[i].On = ((number >> i) & 1) == 1;
	        }
		}

        public void OutputNumber(byte number, int offset = 0) {
	        for (int i = offset; i < sizeof(byte) * 8 && i < outputs.Count; i++) {
		        outputs[i].On = ((number >> i) & 1) == 1;
	        }
		}

        public void OutputNumber(sbyte number, int offset = 0) {
	        for (int i = offset; i < sizeof(sbyte) * 8 && i < outputs.Count; i++) {
		        outputs[i].On = ((number >> i) & 1) == 1;
	        }
		}

        public void OutputNumber((int start, int end) range, ulong number) {
			if (range.start >= range.end) return;

			for (int i = range.start; i < range.end; i++) {
				outputs[i].On = ((number >> i) & 1) == 1;
			}
		}

        public void OutputNumber((int start, int end) range, long number) {
			if (range.start >= range.end) return;

			for (int i = range.start; i < range.end; i++) {
				outputs[i].On = ((number >> i) & 1) == 1;
			}
		}

        public void OutputNumber((int start, int end) range, uint number) {
	        if (range.start >= range.end) return;

	        for (int i = range.start; i < range.end; i++) {
		        outputs[i].On = ((number >> i) & 1) == 1;
	        }
		}

        public void OutputNumber((int start, int end) range, int number) {
	        if (range.start >= range.end) return;

	        for (int i = range.start; i < range.end; i++) {
		        outputs[i].On = ((number >> i) & 1) == 1;
	        }
		}

        public void OutputNumber((int start, int end) range, ushort number) {
	        if (range.start >= range.end) return;

	        for (int i = range.start; i < range.end; i++) {
		        outputs[i].On = ((number >> i) & 1) == 1;
	        }
		}

        public void OutputNumber((int start, int end) range, short number) {
	        if (range.start >= range.end) return;

	        for (int i = range.start; i < range.end; i++) {
		        outputs[i].On = ((number >> i) & 1) == 1;
	        }
		}

        public void OutputNumber((int start, int end) range, byte number) {
	        if (range.start >= range.end) return;

	        for (int i = range.start; i < range.end; i++) {
		        outputs[i].On = ((number >> i) & 1) == 1;
	        }
		}

        public void OutputNumber((int start, int end) range, sbyte number) {
	        if (range.start >= range.end) return;

	        for (int i = range.start; i < range.end; i++) {
		        outputs[i].On = ((number >> i) & 1) == 1;
	        }
		}

		public void OutputBoolArray(bool[] arr, int offset = 0) {
			for (int i = 0; i < arr.Length; i++) {
				outputs[i + offset].On = arr[i - offset];
			}
		}

		public void OutputBoolArray(bool[][] arr) {
			for (int i = 0; i < arr.Length; i++) {
				for (int j = 0; j < arr[i].Length; j++) {
					outputs[arr[i].Length * i + j].On = arr[i][j];
				}
			}
		}
	}
}
