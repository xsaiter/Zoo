namespace Zoo {
    public class VM {
        public const int DEFAULT_MEM_SIZE = 8 * (1 << 10);
        readonly int[] _m;
        readonly int _memSize;

        public VM(int memSize = DEFAULT_MEM_SIZE) {
            _memSize = memSize;
            _m = new int[_memSize];
        }

        public int MemSize() => _memSize;

        public void Run() {

        }
    }
}