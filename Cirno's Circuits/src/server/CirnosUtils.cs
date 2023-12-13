using LogicAPI.Server.Components;
using LogicWorld.ClientCode;
using System.Collections.Generic;
using System.Numerics;

namespace CirnosCircuits {
	public class Utils {
		private IReadOnlyList<IInputPeg> inputs;
		private IReadOnlyList<IOutputPeg> outputs;

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

		/// <summary>
		/// Sets the output pins from 0 to n-1 for an n bit number, Types T: (s)byte, (u)short, (u)int, (u)long 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		public void OutputInteger<T>(T value)
			where T : IShiftOperators<T, int, T>, IBitwiseOperators<T, T, T>, IEqualityOperators<T, T, bool>, IBinaryInteger<T> {
			switch (value) {
				case byte:
					for (int i = 0; i < 8; i++) {
						outputs[i].On = ((value >> i) & T.One) == T.One;
					}
					break;
				case ushort:
					for (int i = 0; i < 16; i++) {
						outputs[i].On = ((value >> i) & T.One) == T.One;
					}
					break;
				case uint:
					for (int i = 0; i < 32; i++) {
						outputs[i].On = ((value >> i) & T.One) == T.One;
					}
					break;
				case ulong:
					for (int i = 0; i < 64; i++) {
						outputs[i].On = ((value >>> i) & T.One) == T.One;
					}
					break;
				case sbyte:
					for (int i = 0; i < 8; i++) {
						outputs[i].On = ((value >>> i) & T.One) == T.One;
					}
					break;
				case short:
					for (int i = 0; i < 16; i++) {
						outputs[i].On = ((value >>> i) & T.One) == T.One;
					}
					break;
				case int:
					for (int i = 0; i < 32; i++) {
						outputs[i].On = ((value >>> i) & T.One) == T.One;
					}
					break;
				case long:
					for (int i = 0; i < 64; i++) {
						outputs[i].On = ((value >>> i) & T.One) == T.One;
					}
					break;
			}
		}

		/// <summary>
		/// Sets the output pins from offset to (n-1) + offset for an n bit number, Types T: (s)byte, (u)short, (u)int, (u)long
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="offset"></param>
		/// <param name="value"></param>
		public void OutputInteger<T>(int offset, T value)
			where T : IShiftOperators<T, int, T>, IBitwiseOperators<T, T, T>, IEqualityOperators<T, T, bool>, IBinaryInteger<T> {
			switch (value) {
				case byte:
					for (int i = offset; i < 8 + offset; i++) {
						outputs[i].On = ((value >> i) & T.One) == T.One;
					}
					break;
				case ushort:
					for (int i = offset; i < 16 + offset; i++) {
						outputs[i].On = ((value >> i) & T.One) == T.One;
					}
					break;
				case uint:
					for (int i = offset; i < 32 + offset; i++) {
						outputs[i].On = ((value >> i) & T.One) == T.One;
					}
					break;
				case ulong:
					for (int i = offset; i < 64 + offset; i++) {
						outputs[i].On = ((value >>> i) & T.One) == T.One;
					}
					break;
				case sbyte:
					for (int i = offset; i < 8 + offset; i++) {
						outputs[i].On = ((value >>> i) & T.One) == T.One;
					}
					break;
				case short:
					for (int i = offset; i < 16 + offset; i++) {
						outputs[i].On = ((value >>> i) & T.One) == T.One;
					}
					break;
				case int:
					for (int i = offset; i < 32 + offset; i++) {
						outputs[i].On = ((value >>> i) & T.One) == T.One;
					}
					break;
				case long:
					for (int i = offset; i < 64 + offset; i++) {
						outputs[i].On = ((value >>> i) & T.One) == T.One;
					}
					break;
			}
		}

		/// <summary>
		/// Sets the output pins from start pin up to and not including end
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="range"></param>
		/// <param name="value"></param>
		public void OutputInteger<T>((int start, int end) range, T value)
			where T : IShiftOperators<T, int, T>, IBitwiseOperators<T, T, T>, IEqualityOperators<T, T, bool>, IBinaryInteger<T> {
			if (range.start >= range.end) { return; }
			switch (value) {
				case byte:
				case ushort:
				case uint:
				case ulong:
					for (int i = range.start; i < range.end; i++) {
						outputs[i].On = ((value >> i) & T.One) == T.One;
					}
					break;
				case sbyte:
				case short:
				case int:
				case long:
					for (int i = range.start; i < range.end; i++) {
						outputs[i].On = ((value >>> i) & T.One) == T.One;
					}
					break;
			}
		}

		public void OutputBoolArray(bool[] arr) {
			for (int i = 0; i < arr.Length; i++) {
				outputs[i].On = arr[i];
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
