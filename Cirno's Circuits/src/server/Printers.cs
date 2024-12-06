using System;
using System.Globalization;
using LogicAPI.Server.Components;

namespace CirnosCircuits {
	public class BytePrinter: LogicComponent {
		private bool prevClk;
		private bool clk;
		private IOHandler ioHandler;

		protected override void Initialize() {
			prevClk = false;
			ioHandler = new IOHandler(Inputs, Outputs);
		}

		protected override void DoLogicUpdate() {
			sbyte s = ioHandler.GetInputAs<sbyte>();
			byte u = ioHandler.GetInputAs<byte>();
			clk = Inputs[8].On;

			if (clk && !prevClk) {
				string usb = Convert.ToString(u, 2).PadLeft(8, '0');
				string usd = u.ToString(CultureInfo.CurrentCulture);
				string ssd = s.ToString(CultureInfo.CurrentCulture);
				string usx = Convert.ToString(u, 16).PadLeft(2, '0');;
				string msg = "Value:\n";
				msg += $"\tBin: {usb}\n";

				if (s < 0) {
					msg += $"\tDec: {usd} or {ssd}\n";
				} else {
					msg += $"\tDec: {usd}\n";
				}

				msg += $"\tHex: {usx}";
			
				Logger.Info(msg);
			}

			prevClk = clk;
		}
	}
	
	public class WordPrinter: LogicComponent {
		private bool prevClk;
		private bool clk;
		private IOHandler ioHandler;

		protected override void Initialize() {
			prevClk = false;
			ioHandler = new IOHandler(Inputs, Outputs);
		}

		protected override void DoLogicUpdate() {
			short s = ioHandler.GetInputAs<short>();
			ushort u = ioHandler.GetInputAs<ushort>();
			clk = Inputs[16].On;

			if (clk && !prevClk) {
				string usb = Convert.ToString(u, 2).PadLeft(16, '0');
				string usd = u.ToString(CultureInfo.CurrentCulture);
				string ssd = s.ToString(CultureInfo.CurrentCulture);
				string usx = Convert.ToString(u, 16).PadLeft(4, '0');
				string msg = "Value:\n";
				msg += $"\tBin: {usb}\n";

				if (s < 0) {
					msg += $"\tDec: {usd} or {ssd}\n";
				} else {
					msg += $"\tDec: {usd}\n";
				}

				msg += $"\tHex: {usx}";
			
				Logger.Info(msg);
			}

			prevClk = clk;
		}
	}
	
	public class DWordPrinter: LogicComponent {
		private bool prevClk;
		private bool clk;
		private IOHandler ioHandler;

		protected override void Initialize() {
			prevClk = false;
			ioHandler = new IOHandler(Inputs, Outputs);
		}

		protected override void DoLogicUpdate() {
			int s = ioHandler.GetInputAs<int>();
			uint u = ioHandler.GetInputAs<uint>();
			clk = Inputs[32].On;

			if (clk && !prevClk) {
				string usb = Convert.ToString(u, 2).PadLeft(32, '0');
				string usd = u.ToString(CultureInfo.CurrentCulture);
				string ssd = s.ToString(CultureInfo.CurrentCulture);
				string usx = Convert.ToString(u, 16).PadLeft(8, '0');
				string msg = "Value:\n";
				msg += $"\tBin: {usb}\n";

				if (s < 0) {
					msg += $"\tDec: {usd} or {ssd}\n";
				} else {
					msg += $"\tDec: {usd}\n";
				}
				msg += $"\tHex: {usx}";
			
				Logger.Info(msg);
			}

			prevClk = clk;
		}
	}
	
	public class QWordPrinter: LogicComponent {
		private bool prevClk;
		private bool clk;
		private IOHandler ioHandler;

		protected override void Initialize() {
			prevClk = false;
			ioHandler = new IOHandler(Inputs, Outputs);
		}

		protected override void DoLogicUpdate() {
			long s = ioHandler.GetInputAs<long>();
			ulong u = ioHandler.GetInputAs<ulong>();
			clk = Inputs[64].On;

			if (clk && !prevClk) {
				var usb = Convert.ToString(s, 2).PadLeft(64, '0');
				var usd = u.ToString(CultureInfo.CurrentCulture);
				var ssd = s.ToString(CultureInfo.CurrentCulture);
				var usx = Convert.ToString(s, 16).PadLeft(16, '0');
				var msg = "Value:\n";
				msg += $"\tBin: {usb}\n";
				
				if (s < 0) {
					msg += $"\tDec: {usd} or {ssd}\n";
				} else {
					msg += $"\tDec: {usd}\n";
				}
				msg += $"\tHex: {usx}";
			
				Logger.Info(msg);
			}

			prevClk = clk;
		}
	}
	
	public class HalfPrinter: LogicComponent {
		private bool prevClk;
		private bool clk;
		private IOHandler ioHandler;

		protected override void Initialize() {
			prevClk = false;
			ioHandler = new IOHandler(Inputs, Outputs);
		}

		protected override void DoLogicUpdate() {
			ushort u = ioHandler.GetInputAs<ushort>();
			Half num = BitConverter.UInt16BitsToHalf(u);
			clk = Inputs[16].On;

			if (clk && !prevClk) {
				string us = Convert.ToString(u, 2).PadLeft(16, '0');
				string ns = Convert.ToString(num);
				string ux = Convert.ToString(u, 16).PadLeft(4, '0');
				string msg = "Value:\n";
				msg += $"\tBin: {us}\n";
				msg += $"\tDec: {ns}\n";
				msg += $"\tHex: {ux}";
				Logger.Info(msg);
			}

			prevClk = clk;
		}
	}
	
	public class SinglePrinter: LogicComponent {
		private bool prevClk;
		private bool clk;
		private IOHandler ioHandler;

		protected override void Initialize() {
			prevClk = false;
			ioHandler = new IOHandler(Inputs, Outputs);
		}

		protected override void DoLogicUpdate() {
			
			uint u = ioHandler.GetInputAs<uint>();
			float num = BitConverter.UInt32BitsToSingle(u);
			clk = Inputs[32].On;

			if (clk && !prevClk) {
				string us = Convert.ToString(u, 2).PadLeft(32, '0');
				string ns = Convert.ToString(num, CultureInfo.CurrentCulture);
				string ux = Convert.ToString(u, 16).PadLeft(8, '0');
				string msg = "Value:\n";
				msg += $"\tBin: {us}\n";
				msg += $"\tDec: {ns}\n";
				msg += $"\tHex: {ux}";
				Logger.Info(msg);
			}

			prevClk = clk;
		}
	}
	
	public class DoublePrinter: LogicComponent {
		private bool prevClk;
		private bool clk;
		private IOHandler ioHandler;

		protected override void Initialize() {
			prevClk = false;
			ioHandler = new IOHandler(Inputs, Outputs);
		}

		protected override void DoLogicUpdate() {
			long u = ioHandler.GetInputAs<long>();
			double num = BitConverter.Int64BitsToDouble(u);
			clk = Inputs[64].On;

			if (clk && !prevClk) {
				string us = Convert.ToString(u, 2).PadLeft(64, '0');
				string ns = Convert.ToString(num, CultureInfo.CurrentCulture);
				string ux = Convert.ToString(u, 16).PadLeft(16, '0');
				string msg = "Value:\n";
				msg += $"\tBin: {us}\n";
				msg += $"\tDec: {ns}\n";
				msg += $"\tHex: {ux}";
				Logger.Info(msg);
			}

			prevClk = clk;
		}
	}
}