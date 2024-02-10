namespace CirnosCircuits {
    public class Multiplexer1BitSelect : BaseMultiplexer {
        public override int InputBits => 2;
    } // Needs Testing

    public class Multiplexer2BitSelect : BaseMultiplexer {
        public override int InputBits => 4;
    } // Needs Testing

    public class Multiplexer3BitSelect : BaseMultiplexer {
        public override int InputBits => 8;
    } // Needs Testing

    public class Multiplexer4BitSelect : BaseMultiplexer {
        public override int InputBits => 16;
    } // Needs Testing
}