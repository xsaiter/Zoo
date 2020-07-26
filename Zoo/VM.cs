using System;

namespace Zoo {
    public class VM {
        public const int DEFAULT_MEM_SIZE = 8 * (1 << 10);
        int _pc;
        int _sp;
        int _cmd;        

        public VM(int memSize = DEFAULT_MEM_SIZE) {
            MSize = memSize;
            M = new int[MSize];
            _pc = 0;
            _sp = MSize;
        }

        public int MSize { get; }
        public int[] M { get; }

        public void Run() {
            while ((_cmd = M[_pc++]) != Cmds.STOP) {
                if (_cmd >= 0) {
                    M[--_sp] = _cmd;
                } else {
                    switch (_cmd) {
                        case Cmds.ADD:
                            ++_sp;
                            M[_sp] += M[_sp - 1];
                            break;
                        case Cmds.SUB:
                            ++_sp;
                            M[_sp] -= M[_sp - 1];
                            break;
                        case Cmds.MUL:
                            ++_sp;
                            M[_sp] *= M[_sp - 1];
                            break;
                        case Cmds.DIV:
                            ++_sp;
                            M[_sp] /= M[_sp - 1];
                            break;
                        case Cmds.MOD:
                            ++_sp;
                            M[_sp] %= M[_sp - 1];
                            break;
                        case Cmds.NEG:
                            M[_sp] = -M[_sp];
                            break;
                        case Cmds.LOAD:
                            M[_sp] = M[M[_sp]];
                            break;
                        case Cmds.SAVE:
                            M[M[_sp + 1]] = M[_sp];
                            _sp += 2;
                            break;
                        case Cmds.DUP:
                            --_sp; 
                            M[_sp] = M[_sp + 1];
                            break;
                        case Cmds.DROP:
                            ++_sp;
                            break;
                        case Cmds.SWAP:
                            var buf = M[_sp]; 
                            M[_sp] = M[_sp + 1]; 
                            M[_sp + 1] = buf;
                            break;
                        case Cmds.OVER:
                            --_sp; 
                            M[_sp] = M[_sp + 2];
                            break;
                        case Cmds.GOTO:
                            _pc = M[_sp++];
                            break;
                        case Cmds.EQ:
                            if (M[_sp + 2] == M[_sp + 1]) {
                                _pc = M[_sp];
                            }                                
                            _sp += 3;
                            break;
                        case Cmds.NE:
                            if (M[_sp + 2] != M[_sp + 1]) {
                                _pc = M[_sp];
                            }                                
                            _sp += 3;
                            break;
                        case Cmds.LE:
                            if (M[_sp + 2] <= M[_sp + 1]) {
                                _pc = M[_sp];
                            }                                
                            _sp += 3;
                            break;
                        case Cmds.LT:
                            if (M[_sp + 2] < M[_sp + 1]) {
                                _pc = M[_sp];
                            }
                            _sp += 3;
                            break;
                        case Cmds.GE:
                            if (M[_sp + 2] >= M[_sp + 1]) {
                                _pc = M[_sp];
                            }
                            _sp += 3;
                            break;
                        case Cmds.GT:
                            if (M[_sp + 2] > M[_sp + 1]) {
                                _pc = M[_sp];
                            }                                
                            _sp += 3;
                            break;
                        case Cmds.IN:
                            Console.Write('?');
                            M[--_sp] = int.Parse(Console.ReadLine());
                            break;
                        case Cmds.OUT:
                            var n = M[_sp] - M[_sp + 1].ToString().Length;
                            for (var i = 0; i < n; ++i) {
                                Console.Write(" ");
                            }                                
                            Console.Write(M[_sp + 1]);
                            _sp += 2;
                            break;
                        case Cmds.OUT_LN:
                            Console.WriteLine();
                            break;
                        default:
                            Console.WriteLine("invalid operation code");
                            M[_pc] = Cmds.STOP;
                            break;
                    }
                }
            }
        }

        public static class Cmds {
            public const int
                STOP = -1,
                ADD = -2,
                SUB = -3,
                MUL = -4,
                DIV = -5,
                MOD = -6,
                NEG = -7,

                LOAD = -8,
                SAVE = -9,

                DUP = -10,
                DROP = -11,
                SWAP = -12,
                OVER = -13,

                GOTO = -14,
                EQ = -15,
                NE = -16,
                LE = -17,
                LT = -18,
                GE = -19,
                GT = -20,

                IN = -21,
                OUT = -22,
                OUT_LN = -23;
        }
    }
}