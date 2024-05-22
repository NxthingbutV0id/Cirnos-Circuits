using LogicAPI.Server.Components;
using System;

namespace CirnosCircuits 
{
    public class SidewaysAND : LogicComponent 
    {
        protected override void DoLogicUpdate() 
        {
            Outputs[0].On = Inputs[0].On & Inputs[1].On;
        }
    } // Completed

    public class SidewaysOR : LogicComponent 
    {
        protected override void DoLogicUpdate() 
        {
            Outputs[0].On = Inputs[0].On | Inputs[1].On;
        }
    } // Completed

    public class SidewaysXOR : LogicComponent 
    {
        protected override void DoLogicUpdate() 
        {
            Outputs[0].On = Inputs[0].On ^ Inputs[1].On;
        }
    } // Completed

    public class SidewaysNAND : LogicComponent 
    {
        protected override void DoLogicUpdate() 
        {
            Outputs[0].On = !(Inputs[0].On & Inputs[1].On);
        }
    } // Completed

    public class SidewaysNOR : LogicComponent 
    {
        protected override void DoLogicUpdate() 
        {
            Outputs[0].On = !(Inputs[0].On | Inputs[1].On);
        }
    } // Completed

    public class SidewaysXNOR : LogicComponent 
    {
        protected override void DoLogicUpdate() 
        {
            Outputs[0].On = !(Inputs[0].On ^ Inputs[1].On);
        }
    } // Completed
}
