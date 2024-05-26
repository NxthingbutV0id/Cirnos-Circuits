using LogicAPI.Server.Components;

namespace CirnosCircuits { 
    public class ByteRelay: BaseRelay {
        protected override int Bits => 8;
    } // Needs Testing

    public class WordRelay: BaseRelay {
        protected override int Bits => 16;
    } // Needs Testing

    public class DWordRelay: BaseRelay {
        protected override int Bits => 32;
    } // Not Implemented

    public class QWordRelay: BaseRelay {
        protected override int Bits => 64;
    } // Not Implemented
}
