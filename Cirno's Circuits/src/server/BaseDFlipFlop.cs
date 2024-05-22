using LogicAPI.Server.Components;

namespace CirnosCircuits
{
    public abstract class BaseDFlipFlop : LogicComponent 
    {
        protected abstract int Bits { get; }
        private bool _prevClk;
        private bool _clk;
        protected override void Initialize() 
        {
            _prevClk = false;
        }

        protected override void DoLogicUpdate() 
        {
            _clk = Inputs[Outputs.Count].On;
            
            if (!_prevClk && _clk) 
                for (var i = 0; i < Bits; i++) 
                    Outputs[i].On = Inputs[i].On;
            
            _prevClk = _clk;
        }
    }
}
