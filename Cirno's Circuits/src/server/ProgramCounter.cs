using LogicAPI.Server.Components;

namespace CirnosCircuits 
{
	public class ProgramCounter : LogicComponent 
    { 
        private uint _counter;
        private bool _clk;
        private bool _prevClk;
        private bool _jumpEnable;
        private IOHandler _ioHandler;

        protected override void Initialize()
        {
	        _ioHandler = new IOHandler(Inputs, Outputs);
			_counter = 0;
			_prevClk = false;
		}

		protected override void DoLogicUpdate() 
        {
			_clk = Inputs[Inputs.Count - 1].On;
			_jumpEnable = Inputs[Inputs.Count - 2].On;

			if (!_prevClk && _clk) 
            {
				if (_jumpEnable) 
					SetCounter(_ioHandler.GetInputAsU32());
                else 
                {
					if (_counter == uint.MaxValue)
						_counter = 0;
					else 
						_counter++; 
				}
			}
			_prevClk = _clk;
		}

		private void SetCounter(uint input) => _counter = input;
    } // Not Implemented
}
