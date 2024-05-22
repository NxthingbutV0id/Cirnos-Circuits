using System;
using LogicAPI.Server.Components;
using System.Collections.Generic;

namespace CirnosCircuits 
{
	public class IOHandler 
	{
		private readonly IReadOnlyList<IInputPeg> _inputs;
		private readonly IReadOnlyList<IOutputPeg> _outputs;

		public IOHandler(IReadOnlyList<IInputPeg> inputs, IReadOnlyList<IOutputPeg> outputs) 
		{
			_inputs = inputs;
			_outputs = outputs;
		}

		public void ClearOutputs() 
		{
			foreach (var peg in _outputs) {
				peg.On = false;
			}
		}

		public ulong GetInputAsU64(int offset = 0) 
		{
			var result = 0ul;
			for (var i = 0; i < 64; i++) 
			{
				if (i + offset >= _inputs.Count) 
					break;
				
				if (_inputs[i + offset].On) 
					result |= 1ul << i;
			}
			return result;
		}
		
		public long GetInputAsI64(int offset = 0) 
		{
			var result = 0L;
			for (var i = 0; i < 64; i++) 
			{
				if (i + offset >= _inputs.Count) 
					break;
				
				if (_inputs[i + offset].On) 
					result |= 1L << i;
			}
			return result;
		}
		
		public uint GetInputAsU32(int offset = 0) 
		{
			var result = 0u;
			for (var i = 0; i < 32; i++) 
			{
				if (i + offset >= _inputs.Count) 
					break;
				
				if (_inputs[i + offset].On) 
					result |= 1u << i;
			}
			return result;
		}
		
		public int GetInputAsI32(int offset = 0) 
		{
			var result = 0;
			for (var i = 0; i < 32; i++) 
			{
				if (i + offset >= _inputs.Count) 
					break;
				
				if (_inputs[i + offset].On) 
					result |= 1 << i;
			}
			return result;
		}
		
		public ushort GetInputAsU16(int offset = 0) 
		{
			ushort result = 0;
			for (var i = 0; i < 16; i++) 
			{
				if (i + offset >= _inputs.Count) 
					break;
				
				if (_inputs[i + offset].On) 
					result |= (ushort)(1 << i);
			}
			return result;
		}
		
		public short GetInputAsI16(int offset = 0) 
		{
			short result = 0;
			for (var i = 0; i < 16; i++) 
			{
				if (i + offset >= _inputs.Count) 
					break;
				
				if (_inputs[i + offset].On) 
					result |= (short)(1 << i);
			}
			return result;
		}
		
		public byte GetInputAsU8(int offset = 0) 
		{
			byte result = 0;
			for (var i = 0; i < 8; i++) 
			{
				if (i + offset >= _inputs.Count) 
					break;
				
				if (_inputs[i + offset].On) 
					result |= (byte)(1 << i);
			}
			return result;
		}
		
		public sbyte GetInputAsI8(int offset = 0) 
		{
			sbyte result = 0;
			for (var i = 0; i < 8; i++) 
			{
				if (i + offset >= _inputs.Count) 
					break;
				
				if (_inputs[i + offset].On) 
					result |= (sbyte)(1 << i);
			}
			return result;
		}

		public bool[] GrabBoolArrayFromInput(int offset = 0) 
		{
			var data = new bool[_inputs.Count];

            for (var i = offset; i < data.Length; i++)
                data[i] = _inputs[i].On;

            return data;
        }

        public bool[] GrabBoolArrayFromInput(int startPin, int endPin) 
        {
            var data = new bool[endPin - startPin];

            for (var i = startPin; i < endPin && i < _inputs.Count; i++)
				data[i - startPin] = _inputs[i].On;

            return data;
        }

        public void OutputNumber(float number, int offset = 0)
        {
	        var n = BitConverter.SingleToUInt32Bits(number);
	        OutputNumber(n, offset);
        }
        
        public void OutputNumber(double number, int offset = 0)
        {
	        var n = BitConverter.DoubleToUInt64Bits(number);
	        OutputNumber(n, offset);
        }

        public void OutputNumber(ulong number, int offset = 0) 
        {
			for (var i = offset; i < sizeof(ulong) * 8 && i < _outputs.Count; i++)
				_outputs[i].On = ((number >> (i - offset)) & 1) == 1;
		}

		public void OutputNumber(long number, int offset = 0) 
		{
			for (var i = offset; i < sizeof(long) * 8 && i < _outputs.Count; i++)
				_outputs[i].On = ((number >> i) & 1) == 1;
		}

		public void OutputNumber(uint number, int offset = 0) 
		{
			for (var i = offset; i < sizeof(uint) * 8 && i < _outputs.Count; i++)
				_outputs[i].On = ((number >> i) & 1) == 1;
		}

        public void OutputNumber(int number, int offset = 0) 
        {
			for (var i = offset; i < sizeof(int) * 8 && i < _outputs.Count; i++)
				_outputs[i].On = ((number >> i) & 1) == 1;
		}

        public void OutputNumber(ushort number, int offset = 0) 
        {
			for (var i = offset; i < sizeof(ushort) * 8 && i < _outputs.Count; i++)
				_outputs[i].On = ((number >> i) & 1) == 1;
		}

        public void OutputNumber(short number, int offset = 0) 
        {
			for (var i = offset; i < sizeof(short) * 8 && i < _outputs.Count; i++)
				_outputs[i].On = ((number >> i) & 1) == 1;
		}

        public void OutputNumber(byte number, int offset = 0) 
        {
			for (var i = offset; i < sizeof(byte) * 8 && i < _outputs.Count; i++)
				_outputs[i].On = ((number >> i) & 1) == 1;
		}

        public void OutputNumber(sbyte number, int offset = 0) 
        {
			for (var i = offset; i < sizeof(sbyte) * 8 && i < _outputs.Count; i++)
				_outputs[i].On = ((number >> i) & 1) == 1;
		}

        public void OutputNumber((int start, int end) range, ulong number) 
        {
			if (range.start >= range.end) 
				return; 
			
			for (var i = range.start; i < range.end; i++) 
				_outputs[i].On = ((number >> i) & 1) == 1;
		}

        public void OutputNumber((int start, int end) range, long number) 
        {
			if (range.start >= range.end) 
				return;
			
			for (var i = range.start; i < range.end; i++) 
				_outputs[i].On = ((number >> i) & 1) == 1;
		}

        public void OutputNumber((int start, int end) range, uint number) 
        {
	        if (range.start >= range.end) 
		        return;
	        
			for (var i = range.start; i < range.end; i++) 
				_outputs[i].On = ((number >> i) & 1) == 1;
		}

        public void OutputNumber((int start, int end) range, int number) 
        {
	        if (range.start >= range.end) 
		        return;

			for (var i = range.start; i < range.end; i++) 
				_outputs[i].On = ((number >> i) & 1) == 1;
		}

        public void OutputNumber((int start, int end) range, ushort number) 
        {
	        if (range.start >= range.end) 
		        return;

			for (var i = range.start; i < range.end; i++) 
				_outputs[i].On = ((number >> i) & 1) == 1;
		}

        public void OutputNumber((int start, int end) range, short number) 
        {
	        if (range.start >= range.end) 
		        return;

			for (var i = range.start; i < range.end; i++) 
				_outputs[i].On = ((number >> i) & 1) == 1;
		}

        public void OutputNumber((int start, int end) range, byte number)
        {
	        if (range.start >= range.end) 
		        return;

			for (var i = range.start; i < range.end; i++) 
				_outputs[i].On = ((number >> i) & 1) == 1;
		}

        public void OutputNumber((int start, int end) range, sbyte number) 
        {
	        if (range.start >= range.end) 
		        return;

			for (var i = range.start; i < range.end; i++) 
				_outputs[i].On = ((number >> i) & 1) == 1;
		}

		public void OutputBoolArray(bool[] arr, int offset = 0) 
		{
			for (var i = 0; i < arr.Length; i++) 
				_outputs[i + offset].On = arr[i - offset];
		}

		public void OutputBoolArray(bool[][] arr) 
		{
			for (var i = 0; i < arr.Length; i++) 
				for (var j = 0; j < arr[i].Length; j++) 
					_outputs[arr[i].Length * i + j].On = arr[i][j];
		}

		public static ulong Pow(ulong n, int pow) 
		{
			ulong ret = 1;
			while (pow != 0) 
			{
				if ((pow & 1) == 1) 
					ret *= n;
				
				n *= n;
				pow >>= 1;
			}
			return ret;
		}
	}
}
