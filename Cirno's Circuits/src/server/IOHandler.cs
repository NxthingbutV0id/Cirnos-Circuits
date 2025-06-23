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

		/// <summary>
		///
		/// Gets the input value as a specific type.
		///
		/// <typeparam name="T">The type to convert the input to.</typeparam>
		///
		/// <param name="offset">The number of input pins to skip before reading.</param>
		/// 
		/// </summary>
		public T GetInputAs<T>(int offset = 0) where T : struct, IConvertible {
			bool isSigned = 
				typeof(T) == typeof(sbyte) || 
				typeof(T) == typeof(short) || 
				typeof(T) == typeof(int) || 
				typeof(T) == typeof(long);

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

		/// <summary>
		///
		/// 
		/// 
		/// </summary>
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

        public void OutputNumber(byte number, int bitOffset = 0) {
	        for (int i = bitOffset; i < 8 + bitOffset && i < outputs.Count; i++) {
		        byte num = (byte)(number >> (i - bitOffset));
		        if (num == 0) break;
		        outputs[i].On = (num & 1) == 1;
	        }
        }
        
        public void OutputNumber(sbyte number, int bitOffset = 0) {
	        OutputNumber((byte)number, bitOffset);
        }
        
        public void OutputNumber(ushort number, int bitOffset = 0) {
	        for (int i = bitOffset; i < 16 + bitOffset && i < outputs.Count; i++) {
		        ushort num = (ushort)(number >> (i - bitOffset));
		        if (num == 0) break;
		        outputs[i].On = (num & 1) == 1;
	        }
        }
        
        public void OutputNumber(short number, int bitOffset = 0) {
	        OutputNumber((ushort)number, bitOffset);
        }
        
        public void OutputNumber(uint number, int bitOffset = 0) {
	        for (int i = bitOffset; i < 32 + bitOffset && i < outputs.Count; i++) {
		        uint num = number >> (i - bitOffset);
		        if (num == 0) break;
		        outputs[i].On = (num & 1) == 1;
	        }
        }
        
        public void OutputNumber(int number, int bitOffset = 0) {
	        OutputNumber((uint)number, bitOffset);
        }
        
        public void OutputNumber(ulong number, int bitOffset = 0) {
	        for (int i = bitOffset; i < 64 + bitOffset && i < outputs.Count; i++) {
		        ulong num = number >> (i - bitOffset);
		        if (num == 0) break;
		        outputs[i].On = (num & 1) == 1;
	        }
        }
        
        public void OutputNumber(long number, int bitOffset = 0) {
	        OutputNumber((ulong)number, bitOffset);
        }

		public void OutputBoolArray(bool[] arr, int offset = 0) {
			for (int i = 0; i < arr.Length; i++) {
				if (i + offset >= outputs.Count) break;
				outputs[i + offset].On = arr[i];
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
