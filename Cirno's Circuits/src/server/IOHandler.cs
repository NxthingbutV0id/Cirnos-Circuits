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

		/// <summary>
		/// Clears the Output pins to all 0
		/// </summary>
		public void ClearOutputs() {
			for (int i = 0; i < outputs.Count; i++) {
				outputs[i].On = false;
			}
		}

        /// <summary>
        /// Turns the binary value of the Input pins into a number.
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public ulong GrabValueFromInput(int offset = 0) {
			ulong result = 0;

			for (int i = offset; i < inputs.Count; i++) {
				if (inputs[i].On) {
					result |= 1ul << i - offset;
				}
			}

			return result;
		}

		/// <summary>
		/// Turns the binary value of the Input pins into a number, from the startPin, up to but not including the endPin.
		/// </summary>
		/// <param name="startPin"></param>
		/// <param name="endPin"></param>
		/// <returns></returns>
		public ulong GrabValueFromInput(int startPin, int endPin) {
			ulong result = 0;

			for (int i = startPin; (i < endPin) && (i < inputs.Count); i++) {
				if (inputs[i].On) {
					result |= 1ul << i - startPin;
				}
			}

			return result;
		}

        public long GrabValueFromInput(int startPin, int endPin, bool signExtend = true) {
            long result = 0;

            for (int i = startPin; (i < endPin) && (i < inputs.Count); i++) {
                if (inputs[i].On) {
                    result |= 1L << i - startPin;
                }
            }

			int bits = endPin - startPin;

			if (((result >> (bits - 1)) & 1) == 1) {
				result |= -1L << bits;
			}

            return result;
        }

        /// <summary>
        /// Converts inputs into an array of booleans
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool[] GrabBoolArrayFromInput(int offset = 0) {
			bool[] data = new bool[inputs.Count];

            for (int i = offset; i < data.Length; i++) {
                data[i] = inputs[i].On;
            }

            return data;
        }

        /// <summary>
		/// Converts inputs from the start pin to the end pin exclusive into an array of booleans (size of endPin - startPin)
		/// </summary>
		/// <param name="startPin"></param>
		/// <param name="endPin"></param>
		/// <returns></returns>
        public bool[] GrabBoolArrayFromInput(int startPin, int endPin) {
            bool[] data = new bool[endPin - startPin];

            for (int i = startPin; (i < endPin) && (i < inputs.Count); i++) {
				data[i - startPin] = inputs[i].On;
            }

            return data;
        }

        /// <summary>
		/// Outputs an unsigned 64 bit integer
		/// </summary>
		/// <param name="number"></param>
		/// <param name="offset"></param>
        public void OutputInteger(ulong number, int offset = 0) {
			for (int i = offset; i < (sizeof(ulong) * 8) && i < outputs.Count; i++) {
				outputs[i].On = ((number >> (i - offset)) & 1) == 1;
			}
		}

		/// <summary>
		/// Outputs a signed 64 bit integer
		/// </summary>
		/// <param name="number"></param>
		/// <param name="offset"></param>
		public void OutputInteger(long number, int offset = 0) {
			for (int i = offset; i < (sizeof(long) * 8); i++) {
				outputs[i].On = ((number >> i) & 1) == 1;
			}
		}

		/// <summary>
		/// Outputs a unsigned 32 bit integer
		/// </summary>
		/// <param name="number"></param>
		/// <param name="offset"></param>
		public void OutputInteger(uint number, int offset = 0) {
			for (int i = offset; i < (sizeof(uint) * 8); i++) {
				outputs[i].On = ((number >> i) & 1) == 1;
			}
		}

        /// <summary>
        /// Outputs a signed 32 bit integer
        /// </summary>
        /// <param name="number"></param>
        /// <param name="offset"></param>
        public void OutputInteger(int number, int offset = 0) {
			for (int i = offset; i < (sizeof(int) * 8); i++) {
				outputs[i].On = ((number >> i) & 1) == 1;
			}
		}

        /// <summary>
        /// Outputs a unsigned 16 bit integer
        /// </summary>
        /// <param name="number"></param>
        /// <param name="offset"></param>
        public void OutputInteger(ushort number, int offset = 0) {
			for (int i = offset; i < (sizeof(ushort) * 8); i++) {
				outputs[i].On = ((number >> i) & 1) == 1;
			}
		}

        /// <summary>
        /// Outputs a signed 16 bit integer
        /// </summary>
        /// <param name="number"></param>
        /// <param name="offset"></param>
        public void OutputInteger(short number, int offset = 0) {
			for (int i = offset; i < (sizeof(short) * 8); i++) {
				outputs[i].On = ((number >> i) & 1) == 1;
			}
		}

        /// <summary>
        /// Outputs a unsigned 8 bit integer
        /// </summary>
        /// <param name="number"></param>
        /// <param name="offset"></param>
        public void OutputInteger(byte number, int offset = 0) {
			for (int i = offset; i < (sizeof(byte) * 8); i++) {
				outputs[i].On = ((number >> i) & 1) == 1;
			}
		}

        /// <summary>
        /// Outputs a signed 8 bit integer
        /// </summary>
        /// <param name="number"></param>
        /// <param name="offset"></param>
        public void OutputInteger(sbyte number, int offset = 0) {
			for (int i = offset; i < (sizeof(sbyte) * 8); i++) {
				outputs[i].On = ((number >> i) & 1) == 1;
			}
		}

        /// <summary>
		/// Outputs an unsigned 64 bit integer
		/// </summary>
		/// <param name="range"></param>
		/// <param name="number"></param>
        public void OutputInteger((int start, int end) range, ulong number) {
			if (range.start >= range.end) { return; }
			for (int i = range.start; i < range.end; i++) {
				outputs[i].On = ((number >> i) & 1) == 1;
			}
		}

        /// <summary>
		/// Outputs a signed 64 bit integer
		/// </summary>
		/// <param name="range"></param>
		/// <param name="number"></param>
        public void OutputInteger((int start, int end) range, long number) {
			if (range.start >= range.end) { return; }
			for (int i = range.start; i < range.end; i++) {
				outputs[i].On = ((number >> i) & 1) == 1;
			}
		}

        /// <summary>
		/// Outputs a unsigned 32 bit integer
		/// </summary>
		/// <param name="range"></param>
		/// <param name="number"></param>
        public void OutputInteger((int start, int end) range, uint number) {
			if (range.start >= range.end) { return; }
			for (int i = range.start; i < range.end; i++) {
				outputs[i].On = ((number >> i) & 1) == 1;
			}
		}

        /// <summary>
		/// Outputs a signed 32 bit integer
		/// </summary>
		/// <param name="range"></param>
		/// <param name="number"></param>
        public void OutputInteger((int start, int end) range, int number) {
			if (range.start >= range.end) { return; }
			for (int i = range.start; i < range.end; i++) {
				outputs[i].On = ((number >> i) & 1) == 1;
			}
		}

        /// <summary>
        /// Outputs a unsigned 16 bit integer
        /// </summary>
        /// <param name="range"></param>
        /// <param name="number"></param>
        public void OutputInteger((int start, int end) range, ushort number) {
			if (range.start >= range.end) { return; }
			for (int i = range.start; i < range.end; i++) {
				outputs[i].On = ((number >> i) & 1) == 1;
			}
		}

        /// <summary>
        /// Outputs a signed 16 bit integer
        /// </summary>
        /// <param name="range"></param>
        /// <param name="number"></param>
        public void OutputInteger((int start, int end) range, short number) {
			if (range.start >= range.end) { return; }
			for (int i = range.start; i < range.end; i++) {
				outputs[i].On = ((number >> i) & 1) == 1;
			}
		}

        /// <summary>
        /// Outputs a unsigned 8 bit integer
        /// </summary>
        /// <param name="range"></param>
        /// <param name="number"></param>
        public void OutputInteger((int start, int end) range, byte number) {
			if (range.start >= range.end) { return; }
			for (int i = range.start; i < range.end; i++) {
				outputs[i].On = ((number >> i) & 1) == 1;
			}
		}

        /// <summary>
        /// Outputs a signed 8 bit integer
        /// </summary>
        /// <param name="range"></param>
        /// <param name="number"></param>
        public void OutputInteger((int start, int end) range, sbyte number) {
			if (range.start >= range.end) { return; }
			for (int i = range.start; i < range.end; i++) {
				outputs[i].On = ((number >> i) & 1) == 1;
			}
		}

		/// <summary>
		/// Sets the output pins using a list of booleans
		/// </summary>
		/// <param name="arr"></param>
		public void OutputBoolArray(bool[] arr, int offset = 0) {
			for (int i = 0; i < arr.Length; i++) {
				outputs[i + offset].On = arr[i - offset];
			}
		}

		/// <summary>
		/// Sets the output pins, from 0 to the size of the input, to a 2D array of booleans
		/// </summary>
		/// <param name="arr"></param>
		public void OutputBoolArray(bool[][] arr) {
			for (int i = 0; i < arr.Length; i++) {
				for (int j = 0; j < arr[i].Length; j++) {
					outputs[arr[i].Length * i + j].On = arr[i][j];
				}
			}
		}

		public static ulong Pow(ulong n, int pow) {
			ulong ret = 1;
			while (pow != 0) {
				if ((pow & 1) == 1) {
					ret *= n;
				}
				n *= n;
				pow >>= 1;
			}
			return ret;
		}
	}
}
