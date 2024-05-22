using LogicAPI.Server.Components;

namespace CirnosCircuits 
{
    public class ByteDLatch : BaseDLatch {
        protected override int Bits => 8;
    } // Needs testing

    public class WordDLatch : BaseDLatch 
    {
        protected override int Bits => 16;
    } // Not Implemented

    public class DWordDLatch : BaseDLatch 
    {
        protected override int Bits => 32;
    } // Not Implemented

    public class QWordDLatch : BaseDLatch 
    {
        protected override int Bits => 64;
    } // Not Implemented

    public class DFlipFlop : BaseDFlipFlop 
    {
        protected override int Bits => 1;
    } // Needs testing

    public class ByteDFlipFlop : BaseDFlipFlop 
    {
        protected override int Bits => 8;
    } // Needs testing

    public class WordDFlipFlop : BaseDFlipFlop 
    {
        protected override int Bits => 16;
    } // Needs testing

    public class DWordDFlipFlop : BaseDFlipFlop 
    {
        protected override int Bits => 32;
    } // Not Implemented

    public class QWordDFlipFlop : BaseDFlipFlop 
    {
        protected override int Bits => 64;
    } // Not Implemented
}
