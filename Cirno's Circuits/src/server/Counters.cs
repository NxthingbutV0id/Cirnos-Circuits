using LogicAPI.Server.Components;

namespace CirnosCircuits {
	public class ByteCounter: LogicComponent { 
		private byte counter;
		private bool clk;
		private bool prevClk;
		private bool jumpEnable;
		private IOHandler ioHandler;

		protected override void Initialize() {
			ioHandler = new IOHandler(Inputs, Outputs);
			counter = 0;
			prevClk = false;
		}

		protected override void DoLogicUpdate() {
			clk = Inputs[9].On;
			jumpEnable = Inputs[8].On;

			if (!prevClk && clk) {
				ioHandler.ClearOutputs();
				byte x = ioHandler.GetInputAs<byte>();
				if (jumpEnable) {
					SetCounter(x);
				} else {
					counter = (byte)((counter + x) & 0xFF);
				}
				ioHandler.OutputNumber(counter);
			}
			prevClk = clk;
		}

		private void SetCounter(byte input) => counter = input;
	}
	
	public class WordCounter: LogicComponent { 
		private ushort counter;
		private bool clk;
		private bool prevClk;
		private bool jumpEnable;
		private IOHandler ioHandler;

		protected override void Initialize() {
			ioHandler = new IOHandler(Inputs, Outputs);
			counter = 0;
			prevClk = false;
		}

		protected override void DoLogicUpdate() {
			clk = Inputs[17].On;
			jumpEnable = Inputs[16].On;

			if (!prevClk && clk) {
				ioHandler.ClearOutputs();
				ushort x = ioHandler.GetInputAs<ushort>();
				if (jumpEnable) {
					SetCounter(x);
				} else {
					counter = (ushort)((counter + x) & 0xFFFF);
				}
				ioHandler.OutputNumber(counter);
			}
			prevClk = clk;
		}

		private void SetCounter(ushort input) => counter = input;
	}
	
	public class DWordCounter: LogicComponent { 
		private uint counter;
		private bool clk;
		private bool prevClk;
		private bool jumpEnable;
		private IOHandler ioHandler;

		protected override void Initialize() {
			ioHandler = new IOHandler(Inputs, Outputs);
			counter = 0;
			prevClk = false;
		}

		protected override void DoLogicUpdate() {
			clk = Inputs[33].On;
			jumpEnable = Inputs[32].On;

			if (!prevClk && clk) {
				ioHandler.ClearOutputs();
				uint x = ioHandler.GetInputAs<uint>();
				if (jumpEnable) {
					SetCounter(x);
				} else {
					counter = (counter + x) & 0xFFFFFFFF;
				}
				ioHandler.OutputNumber(counter);
			}
			prevClk = clk;
		}

		private void SetCounter(uint input) => counter = input;
	}
	
	public class QWordCounter: LogicComponent { 
		private ulong counter;
		private bool clk;
		private bool prevClk;
		private bool jumpEnable;
		private IOHandler ioHandler;

		protected override void Initialize() {
			ioHandler = new IOHandler(Inputs, Outputs);
			counter = 0;
			prevClk = false;
		}

		protected override void DoLogicUpdate() {
			clk = Inputs[65].On;
			jumpEnable = Inputs[64].On;

			if (!prevClk && clk) {
				ioHandler.ClearOutputs();
				ulong x = ioHandler.GetInputAs<ulong>();
				if (jumpEnable) {
					SetCounter(x);
				} else {
					counter = (counter + x) & 0xFFFFFFFFFFFFFFFF;
				}
				ioHandler.OutputNumber(counter);
			}
			prevClk = clk;
		}

		private void SetCounter(ulong input) => counter = input;
	}
}
