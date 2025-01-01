using System.IO;
using LogicAPI.Server.Components;

namespace CirnosCircuits {
	public class ProgramReader : LogicComponent {
		private IOHandler ioHandler;
		private byte[] program = new byte[65536];
		private const string FilePath = "/programs/program.bin";

		protected override void Initialize() {
			ioHandler = new IOHandler(Inputs, Outputs);
			LoadProgram();
		}

		protected override void DoLogicUpdate() {
			ioHandler.ClearOutputs();
			ushort address = ioHandler.GetInputAs<ushort>();
			if (address > 0xFFFE) return;
			int instruction = (program[address + 1] << 8) | program[address];
			ioHandler.OutputNumber(instruction);
		}

		private void LoadProgram() {
			if (File.Exists(FilePath)) { program = File.ReadAllBytes(FilePath); }
		}
	}
}