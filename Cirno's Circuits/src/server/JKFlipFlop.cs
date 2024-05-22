using LogicAPI.Server.Components;

namespace CirnosCircuits
{
    public class JKFlipFlop : LogicComponent
    {
        private bool _j;
        private bool _k;
        private bool _clock;
        private bool _prevClock;

        protected override void Initialize() 
        {
            Outputs[0].On = false; //  Q
            Outputs[1].On = true;  // !Q
            _prevClock = false;
        }

        protected override void DoLogicUpdate() 
        {
            _j = Inputs[0].On;
            _k = Inputs[1].On;
            _clock = Inputs[2].On;
            if (!_prevClock && _clock) 
            {
                if (_j && _k) 
                {
                    Outputs[0].On = !Outputs[0].On;
                    Outputs[1].On = !Outputs[1].On;
                    _prevClock = _clock;
                    return;
                }
                if (!_j && _k) 
                {
                    Outputs[0].On = false;
                    Outputs[1].On = true;
                    _prevClock = _clock;
                    return;
                }
                if (_j && !_k) 
                {
                    Outputs[0].On = true;
                    Outputs[1].On = false;
                    _prevClock = _clock;
                    return;
                }
            }
            _prevClock = _clock;
        }
    } // Completed
}