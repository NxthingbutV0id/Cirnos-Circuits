using LogicAPI.Server.Components;

namespace CirnosCircuits
{
    public class SRLatch : LogicComponent
    {
        private bool _set;
        private bool _reset;
        private bool _q;
        private bool _qBar;

        protected override void Initialize() 
        {
            _q = false;
            _qBar = true;
        }

        protected override void DoLogicUpdate() 
        {
            _set = Inputs[0].On; _reset = Inputs[1].On;

            for (var i = 0; i < 3; ++i) 
            {
                _qBar = !(_set || _q);
                _q = !(_reset || _qBar);
            }

            Outputs[0].On = _q;
            Outputs[1].On = _qBar;
        }
    } // Completed
}