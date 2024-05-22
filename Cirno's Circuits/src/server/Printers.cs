using System;
using System.Globalization;
using LogicAPI.Server.Components;

namespace CirnosCircuits
{
	public class BytePrinter : LogicComponent
	{
		private bool _prevClk;
		private bool _clk;
		private IOHandler _ioHandler;

		protected override void Initialize()
		{
			_prevClk = false;
			_ioHandler = new IOHandler(Inputs, Outputs);
		}

		protected override void DoLogicUpdate()
		{
			var s = _ioHandler.GetInputAsI8();
			var u = _ioHandler.GetInputAsU8();
			_clk = Inputs[8].On;

			if (_clk && !_prevClk)
			{
				var usb = Convert.ToString(u, 2).PadLeft(8, '0');
				var usd = u.ToString(CultureInfo.CurrentCulture);
				var ssd = s.ToString(CultureInfo.CurrentCulture);
				var usx = Convert.ToString(u, 16).PadLeft(2, '0');;
				var msg = "Value:\n";
				msg += $"\tBin: {usb}\n";
				
				if (s < 0)
					msg += $"\tDec: {usd} or {ssd}\n";
				else
					msg += $"\tDec: {usd}\n";
				
				msg += $"\tHex: {usx}";
			
				Logger.Info(msg);
			}

			_prevClk = _clk;
		}
	}
	
	public class WordPrinter : LogicComponent
	{
		private bool _prevClk;
		private bool _clk;
		private IOHandler _ioHandler;

		protected override void Initialize()
		{
			_prevClk = false;
			_ioHandler = new IOHandler(Inputs, Outputs);
		}

		protected override void DoLogicUpdate()
		{
			var s = _ioHandler.GetInputAsI16();
			var u = _ioHandler.GetInputAsU16();
			_clk = Inputs[16].On;

			if (_clk && !_prevClk)
			{
				var usb = Convert.ToString(u, 2).PadLeft(16, '0');
				var usd = u.ToString(CultureInfo.CurrentCulture);
				var ssd = s.ToString(CultureInfo.CurrentCulture);
				var usx = Convert.ToString(u, 16).PadLeft(4, '0');
				var msg = "Value:\n";
				msg += $"\tBin: {usb}\n";
				
				if (s < 0)
					msg += $"\tDec: {usd} or {ssd}\n";
				else
					msg += $"\tDec: {usd}\n";
				
				msg += $"\tHex: {usx}";
			
				Logger.Info(msg);
			}

			_prevClk = _clk;
		}
	}
	
	public class DWordPrinter : LogicComponent
	{
		private bool _prevClk;
		private bool _clk;
		private IOHandler _ioHandler;

		protected override void Initialize()
		{
			_prevClk = false;
			_ioHandler = new IOHandler(Inputs, Outputs);
		}

		protected override void DoLogicUpdate()
		{
			var s = _ioHandler.GetInputAsI32();
			var u = _ioHandler.GetInputAsU32();
			_clk = Inputs[32].On;

			if (_clk && !_prevClk)
			{
				var usb = Convert.ToString(u, 2).PadLeft(32, '0');
				var usd = u.ToString(CultureInfo.CurrentCulture);
				var ssd = s.ToString(CultureInfo.CurrentCulture);
				var usx = Convert.ToString(u, 16).PadLeft(8, '0');
				var msg = "Value:\n";
				msg += $"\tBin: {usb}\n";
				
				if (s < 0)
					msg += $"\tDec: {usd} or {ssd}\n";
				else
					msg += $"\tDec: {usd}\n";
				
				msg += $"\tHex: {usx}";
			
				Logger.Info(msg);
			}

			_prevClk = _clk;
		}
	}
	
	public class QWordPrinter : LogicComponent
	{
		private bool _prevClk;
		private bool _clk;
		private IOHandler _ioHandler;

		protected override void Initialize()
		{
			_prevClk = false;
			_ioHandler = new IOHandler(Inputs, Outputs);
		}

		protected override void DoLogicUpdate()
		{
			var s = _ioHandler.GetInputAsI64();
			var u = _ioHandler.GetInputAsU64();
			_clk = Inputs[64].On;

			if (_clk && !_prevClk)
			{
				var usb = Convert.ToString(s, 2).PadLeft(64, '0');
				var usd = u.ToString(CultureInfo.CurrentCulture);
				var ssd = s.ToString(CultureInfo.CurrentCulture);
				var usx = Convert.ToString(s, 16).PadLeft(16, '0');
				var msg = "Value:\n";
				msg += $"\tBin: {usb}\n";
				
				if (s < 0)
					msg += $"\tDec: {usd} or {ssd}\n";
				else
					msg += $"\tDec: {usd}\n";
				
				msg += $"\tHex: {usx}";
			
				Logger.Info(msg);
			}

			_prevClk = _clk;
		}
	}
	
	public class HalfPrinter : LogicComponent
	{
		private bool _prevClk;
		private bool _clk;
		private IOHandler _ioHandler;

		protected override void Initialize()
		{
			_prevClk = false;
			_ioHandler = new IOHandler(Inputs, Outputs);
		}

		protected override void DoLogicUpdate()
		{
			var u = _ioHandler.GetInputAsU16();
			var num = BitConverter.UInt16BitsToHalf(u);
			_clk = Inputs[16].On;

			if (_clk && !_prevClk)
			{
				var us = Convert.ToString(u, 2).PadLeft(16, '0');
				var ns = Convert.ToString(num);
				var ux = Convert.ToString(u, 16).PadLeft(4, '0');
				var msg = "Value:\n";
				msg += $"\tBin: {us}\n";
				msg += $"\tDec: {ns}\n";
				msg += $"\tHex: {ux}";
				Logger.Info(msg);
			}

			_prevClk = _clk;
		}
	}
	
	public class SinglePrinter : LogicComponent
	{
		private bool _prevClk;
		private bool _clk;
		private IOHandler _ioHandler;

		protected override void Initialize()
		{
			_prevClk = false;
			_ioHandler = new IOHandler(Inputs, Outputs);
		}

		protected override void DoLogicUpdate()
		{
			
			var u = _ioHandler.GetInputAsU32();
			var num = BitConverter.UInt32BitsToSingle(u);
			_clk = Inputs[32].On;

			if (_clk && !_prevClk)
			{
				var us = Convert.ToString(u, 2).PadLeft(32, '0');
				var ns = Convert.ToString(num, CultureInfo.CurrentCulture);
				var ux = Convert.ToString(u, 16).PadLeft(8, '0');
				var msg = "Value:\n";
				msg += $"\tBin: {us}\n";
				msg += $"\tDec: {ns}\n";
				msg += $"\tHex: {ux}";
				Logger.Info(msg);
			}

			_prevClk = _clk;
		}
	}
	
	public class DoublePrinter : LogicComponent
	{
		private bool _prevClk;
		private bool _clk;
		private IOHandler _ioHandler;

		protected override void Initialize()
		{
			_prevClk = false;
			_ioHandler = new IOHandler(Inputs, Outputs);
		}

		protected override void DoLogicUpdate()
		{
			var u = _ioHandler.GetInputAsI64();
			var num = BitConverter.Int64BitsToDouble(u);
			_clk = Inputs[64].On;

			if (_clk && !_prevClk)
			{
				var us = Convert.ToString(u, 2).PadLeft(64, '0');
				var ns = Convert.ToString(num, CultureInfo.CurrentCulture);
				var ux = Convert.ToString(u, 16).PadLeft(16, '0');
				var msg = "Value:\n";
				msg += $"\tBin: {us}\n";
				msg += $"\tDec: {ns}\n";
				msg += $"\tHex: {ux}";
				Logger.Info(msg);
			}

			_prevClk = _clk;
		}
	}
}