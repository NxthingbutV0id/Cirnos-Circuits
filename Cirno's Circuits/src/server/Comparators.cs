namespace CirnosCircuits {
    public class ByteComparator : BaseComparator {
        public override int Bits => 8;
    } // Needs Testing

    public class WordComparator : BaseComparator {
        public override int Bits => 16;
    } // Needs Testing

    public class DWordComparator : BaseComparator {
        public override int Bits => 32;
    } // Not Implemented

    public class QWordComparator : BaseComparator {
        public override int Bits => 64;
    } // Not Implemented
}
