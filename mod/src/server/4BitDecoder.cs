using LogicAPI.Server.Components;
using LogicAPI.Server;
using LogicLog;
using LogicWorld.Server.Circuitry;
using System.Collections.Generic;

namespace CirnosCircuits
{
    public class IVBitDecoder : LogicComponent
    {
        protected override void DoLogicUpdate()
        {
            foreach (var output in Outputs) {
                output.On = false;
            }
            
            int index = InputValue(Inputs);

            Outputs[index].On = true;
        }

        private static int InputValue(IReadOnlyList<IInputPeg> inputs)
        {
            int result = 0;

            for (int i = 0; i < inputs.Count; i++)
            {
                if (inputs[i].On)
                {
                    result += 1 << i;
                }
            }

            return result;
        }
    }
}