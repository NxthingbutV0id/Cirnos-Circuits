using LogicAPI.Server.Components;
using LogicWorld.ClientCode;
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

		/// <summary>
		/// Sets the output pins from 0 to n-1 for an n bit number, Types T: (s)byte, (u)short, (u)int, (u)long 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		public void OutputInteger<T>(T value) where T : notnull, IBinaryInteger<T> {
			dynamic val = value;
			for (int i = 0; i < (Unsafe.SizeOf<T>() * 8); i++) {
				outputs[i].On = ((val >> i) & 1) == 1;
			}
		}

		/// <summary>
		/// Sets the output pins from offset to (n-1) + offset for an n bit number, Types T: (s)byte, (u)short, (u)int, (u)long
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="offset"></param>
		/// <param name="value"></param>
		public void OutputInteger<T>(int offset, T value) where T : notnull, IBinaryInteger<T> {
			dynamic val = value;
			for (int i = offset; i < (Unsafe.SizeOf<T>() * 8) + offset; i++) {
				outputs[i].On = ((val >> i) & 1) == 1;
			}
		}

		/// <summary>
		/// Sets the output pins from start pin up to and not including end
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="range"></param>
		/// <param name="value"></param>
		public void OutputInteger<T>((int start, int end) range, T value) where T : notnull, IBinaryInteger<T> {
			if (range.start >= range.end) { return; }
			dynamic val = value;
			for (int i = range.start; i < range.end; i++) {
				outputs[i].On = ((val >> i) & 1) == 1;
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
