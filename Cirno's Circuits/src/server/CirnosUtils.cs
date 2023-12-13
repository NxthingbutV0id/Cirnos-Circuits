using LogicAPI.Server.Components;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace CirnosCircuits {
	public class Utils {
		private readonly IReadOnlyList<IInputPeg> inputs;
		private readonly IReadOnlyList<IOutputPeg> outputs;

		public Utils(IReadOnlyList<IInputPeg> inputs, IReadOnlyList<IOutputPeg> outputs) {
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
		/// <returns></returns>
		public T InputValue<T>() where T : IBitwiseOperators<T, T, T>, IShiftOperators<T, int, T>, IBinaryInteger<T> {
			T result = T.Zero;

			for (int i = 0; i < inputs.Count; i++) {
				if (inputs[i].On) {
					result |= T.One << i;
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
		public T InputValue<T>(int startPin, int endPin)
			where T : IBitwiseOperators<T, T, T>, IShiftOperators<T, int, T>, IBinaryInteger<T> {
			T result = T.Zero;

			for (int i = 0; (i < endPin) && (i + startPin < inputs.Count); i++) {
				if (inputs[i + startPin].On) {
					result |= T.One << i;
				}
			}

			return result;
		}

		/// <summary>
		/// Turns the binary value of the Input pins into a number, starting from the offset to the end
		/// </summary>
		/// <param name="offset"></param>
		/// <returns></returns>
		public T InputValue<T>(int offset)
			where T : IBitwiseOperators<T, T, T>, IShiftOperators<T, int, T>, IBinaryInteger<T> {
			T result = T.Zero;

			for (int i = 0; i + offset < inputs.Count; i++) {
				if (inputs[i + offset].On) {
					result |= T.One << i;
				}
			}

			return result;
		}

		//OutputInteger(number, offset = 0)
		public void OutputInteger(ulong number, int offset = 0) {
			for (int i = offset; i < (sizeof(ulong) * 8) + offset; i++) {
				outputs[i].On = ((number >> (i - offset)) & 1) == 1;
			}
		}

        public void OutputInteger(long number, int offset = 0) {
            for (int i = offset; i < (sizeof(long) * 8) + offset; i++) {
                outputs[i].On = ((number >> i) & 1) == 1;
            }
        }

        public void OutputInteger(uint number, int offset = 0) {
            for (int i = offset; i < (sizeof(uint) * 8) + offset; i++) {
                outputs[i].On = ((number >> i) & 1) == 1;
            }
        }

        public void OutputInteger(int number, int offset = 0) {
            for (int i = offset; i < (sizeof(int) * 8) + offset; i++) {
                outputs[i].On = ((number >> i) & 1) == 1;
            }
        }

        public void OutputInteger(ushort number, int offset = 0) {
            for (int i = offset; i < (sizeof(ushort) * 8) + offset; i++) {
                outputs[i].On = ((number >> i) & 1) == 1;
            }
        }

        public void OutputInteger(short number, int offset = 0) {
            for (int i = offset; i < (sizeof(short) * 8) + offset; i++) {
                outputs[i].On = ((number >> i) & 1) == 1;
            }
        }

        public void OutputInteger(byte number, int offset = 0) {
            for (int i = offset; i < (sizeof(byte) * 8) + offset; i++) {
                outputs[i].On = ((number >> i) & 1) == 1;
            }
        }

        public void OutputInteger(sbyte number, int offset = 0) {
            for (int i = offset; i < (sizeof(sbyte) * 8) + offset; i++) {
                outputs[i].On = ((number >> i) & 1) == 1;
            }
        }

        //OutputInteger((start, end) range, number)
        public void OutputInteger((int start, int end) range, ulong number) {
			if (range.start >= range.end) { return; }
			for (int i = range.start; i < range.end; i++) {
				outputs[i].On = ((number >> i) & 1) == 1;
			}
		}

        public void OutputInteger((int start, int end) range, long number) {
            if (range.start >= range.end) { return; }
            for (int i = range.start; i < range.end; i++) {
                outputs[i].On = ((number >> i) & 1) == 1;
            }
        }

        public void OutputInteger((int start, int end) range, uint number) {
            if (range.start >= range.end) { return; }
            for (int i = range.start; i < range.end; i++) {
                outputs[i].On = ((number >> i) & 1) == 1;
            }
        }

        public void OutputInteger((int start, int end) range, int number) {
            if (range.start >= range.end) { return; }
            for (int i = range.start; i < range.end; i++) {
                outputs[i].On = ((number >> i) & 1) == 1;
            }
        }

        public void OutputInteger((int start, int end) range, ushort number) {
            if (range.start >= range.end) { return; }
            for (int i = range.start; i < range.end; i++) {
                outputs[i].On = ((number >> i) & 1) == 1;
            }
        }

        public void OutputInteger((int start, int end) range, short number) {
            if (range.start >= range.end) { return; }
            for (int i = range.start; i < range.end; i++) {
                outputs[i].On = ((number >> i) & 1) == 1;
            }
        }

        public void OutputInteger((int start, int end) range, byte number) {
            if (range.start >= range.end) { return; }
            for (int i = range.start; i < range.end; i++) {
                outputs[i].On = ((number >> i) & 1) == 1;
            }
        }

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
			for (int i = offset; i < arr.Length + offset; i++) {
				outputs[i].On = arr[i - offset];
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
	}
}
