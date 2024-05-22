using LogicAPI.Server.Components;

namespace CirnosCircuits
{
    public abstract class BaseRelay : LogicComponent 
    {
        protected abstract int Bits { get; }
        private IInputPeg[] _inputsA;
        private IInputPeg[] _inputsB;
        private bool _wasOpen;

        protected override void Initialize() 
        {
            _inputsA = new IInputPeg[Bits];
            _inputsB = new IInputPeg[Bits];

            for (var i = 0; i < Bits; i++) 
            {
                _inputsA[i] = Inputs[i];
                _inputsB[i] = Inputs[i + Bits];
            }
        }

        protected override void DoLogicUpdate() 
        {
            var isOpen = Inputs[Bits << 1].On;
            
            if (_wasOpen == isOpen)
                return;
            
            if (_inputsA == null || _inputsB == null)
                return;
            
            if (isOpen) 
            {
                for (var i = 0; i < Bits; i++)
                    _inputsA[i].AddPhasicLinkWith(_inputsB[i]);
            } 
            else 
            {
                for (var i = 0; i < Bits; i++)
                    _inputsA[i].RemovePhasicLinkWith(_inputsB[i]);
            }
            _wasOpen = isOpen;
        }

        public override bool InputAtIndexShouldTriggerComponentLogicUpdates(int inputIndex) 
        {
            return inputIndex == (Bits << 1);
        }
    }
}