namespace Zoo {
    public class VM {
        public const int DEFAULT_MEM_SIZE = 8 * (1 << 10);
        int _pc;
        int _sp;
        int _cmd;

        public VM(int memSize = DEFAULT_MEM_SIZE) {
            MemSize = memSize;
            Mem = new int[MemSize];
            _pc = 0;
            _sp = MemSize;
        }

        public int MemSize { get; }
        public int[] Mem { get; }

        public void Run() {
            while ((_cmd = Mem[_pc++]) != Cmds.STOP) { }
        }

        public static class Cmds {
            public const int STOP = -1;
            public const int ADD = -2;
        }
    }
}