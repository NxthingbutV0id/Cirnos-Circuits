using LogicAPI.Server.Components;

namespace CirnosCircuits 
{
	public class EdgeDetector : LogicComponent 
	{
		private bool _prevTick;
		protected override void Initialize() 
		{
			_prevTick = false;
		}

		protected override void DoLogicUpdate() 
		{
			Outputs[0].On = _prevTick != Inputs[0].On;
			if (_prevTick != Inputs[0].On) 
				QueueLogicUpdate();
			
            _prevTick = Inputs[0].On;
        }
	} // Completed
}
