﻿namespace CirnosCircuits {
    public class ByteRelay : BaseRelay {
        public override int Bits => 8;
    } // Needs Testing

    public class WordRelay : BaseRelay {
        public override int Bits => 16;
    } // Needs Testing

    public class DWordRelay : BaseRelay {
        public override int Bits => 32;
    } // Not Implemented

    public class QWordRelay : BaseRelay {
        public override int Bits => 64;
    } // Not Implemented
}
