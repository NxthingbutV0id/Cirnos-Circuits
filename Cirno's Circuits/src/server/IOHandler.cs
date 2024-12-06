using System;
using LogicAPI.Server.Components;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using LogicWorld.LogicCode;

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

		public T GetInputAs<T>(int offset = 0) where T : struct, IConvertible {
			bool isByte = typeof(T) == typeof(byte) || typeof(T) == typeof(sbyte);
			bool isShort = typeof(T) == typeof(short) || typeof(T) == typeof(ushort);
			bool isInt = typeof(T) == typeof(int) || typeof(T) == typeof(uint);
			bool isLong = typeof(T) == typeof(long) || typeof(T) == typeof(ulong);
			bool isSigned = 
				typeof(T) == typeof(sbyte) || 
				typeof(T) == typeof(short) || 
				typeof(T) == typeof(int) || 
				typeof(T) == typeof(long);
			if (!typeof(T).IsPrimitive || !(isByte || isShort || isInt || isLong)) {
				throw new ArgumentException("T must be a primitive type.");
			}

			int bitSize = Marshal.SizeOf(typeof(T)) * 8;

			ulong result = 0;

			for (int i = 0; i < bitSize; i++) {
				if (i + offset >= inputs.Count) break;

				if (inputs[i + offset].On) {
					result |= (ulong)1 << i;
				}
			}

			if (!isSigned) {
				return (T)Convert.ChangeType(result, typeof(T));
			}
			
			long signedValue = SignExtend(result, bitSize);
			return (T)Convert.ChangeType(signedValue, typeof(T));
		}

		private static long SignExtend(ulong value, int bitSize) {
			int shift = 64 - bitSize;
			return (long)(value << shift) >> shift;
		}

        public bool[] GrabBoolArrayFromInput(int startPin, int endPin) {
	        bool[] data = new bool[endPin - startPin];

	        for (int i = startPin; i < endPin && i < inputs.Count; i++) {
		        data[i - startPin] = inputs[i].On;
	        }

            return data;
        }

        public void OutputNumber(float number, int bitOffset = 0) {
	        uint n = BitConverter.SingleToUInt32Bits(number);
	        OutputNumber(n, bitOffset);
        }
        
        public void OutputNumber(double number, int bitOffset = 0) {
	        ulong n = BitConverter.DoubleToUInt64Bits(number);
	        OutputNumber(n, bitOffset);
        }

        public void OutputNumber<T>(T number, int bitOffset = 0) where T : struct, IConvertible {
	        bool isByte = typeof(T) == typeof(byte) || typeof(T) == typeof(sbyte);
	        bool isShort = typeof(T) == typeof(short) || typeof(T) == typeof(ushort);
	        bool isInt = typeof(T) == typeof(int) || typeof(T) == typeof(uint);
	        bool isLong = typeof(T) == typeof(long) || typeof(T) == typeof(ulong);
	        if (!typeof(T).IsPrimitive || !(isByte || isShort || isInt || isLong)) {
		        throw new ArgumentException("T must be a primitive type.");
	        }
	        
	        ulong value = Convert.ToUInt64(number);
	        const int bits = sizeof(ulong) * 8;
	        for (int i = bitOffset; i < bits && i < outputs.Count; i++) {
		        ulong num = value >> (i - bitOffset);
		        if (num == 0) break;
		        outputs[i].On = (num & 1) == 1;
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
