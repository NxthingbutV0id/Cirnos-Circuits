using System;
using System.Collections.Generic;
using LogicAPI.Server.Components;

namespace CirnosCircuits 
{
	public class BCDToBinary : LogicComponent 
    {
	    private IOHandler _ioHandler;

	    protected override void Initialize()
	    {
		    _ioHandler = new IOHandler(Inputs, Outputs);
	    }

		protected override void DoLogicUpdate() 
		{
			var result = 0ul;
			var temp = new byte[10];
			
			for (var i = 0; i < 10; i++)
			{
				temp[i] = _ioHandler.GetInputAsU8(8 * i);
			}

			var power = 0;
			for (var i = 0; i < 10; i++)
			{
				var num = (ulong)temp[i] & 0xf;
				if (num > 9)
					num = 9;
				
				result += num * Pow(10, power);

				num = (ulong)(temp[i] >> 4) & 0xf;
				if (num > 9)
					num = 9;
				
				result += num * Pow(10, power + 1);

				power += 2;
			}

			_ioHandler.OutputNumber(result);
		}

		private static ulong Pow(int n, int p)
		{
			return (ulong)Math.Pow(n, p);
		}
    } // Completed
}
