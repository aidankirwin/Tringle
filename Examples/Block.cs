using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    public class BaseBlock
    {
        // x, y coors of the block; correspond to top left corner of a block's bounding box
        public int X;
        public int Y;
        public int Rotation = 0;

        public int[,]? Data;

        public BaseBlock(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public int[,] DrawBlock(int[,] game)
        {
            for (int i = 0; i < Data.GetLength(0); i++)
            {
                for (int j = 0; j < Data.GetLength(1); j++)
                {
                    // Assume i is never out of bounds

                    // If Y + j is less than 0, do not draw that row
                    if (Y + i < 0) continue;
                    if (Y + i > game.GetLength(0) - 1) continue;
                    if (Data[i, j] == 0) continue;

                    game[Y + i, X + j] = Data[i, j];
                }
            }

            return game;
        }
    }

    public class IBlock : BaseBlock
    {
        public IBlock(int x, int y) : base(x, y)
        {

        }

        public void InitializeBlock(int rotation)
        {
            Rotation = rotation;

            // There are only 2 possible rotations for this block
            Rotation %= 2;

            switch (Rotation)
            {
                case 0:
                    Data = new int[4, 4]
                    {
                        {0, 0, 0, 0 },
                        {0, 0, 0, 0 },
                        {1, 1, 1, 1 },
                        {0, 0, 0, 0 }
                    };
                    break;
                default:
                    Data = new int[4, 4]
                    {
                        {0, 0, 1, 0 },
                        {0, 0, 1, 0 },
                        {0, 0, 1, 0 },
                        {0, 0, 1, 0 }
                    };
                    break;
            }
        }

    }
    public class JBlock : BaseBlock
    {
        public JBlock(int x, int y) : base(x, y)
        {

        }
        public void InitializeBlock(int rotation)
        {
            Rotation = rotation;

            switch (Rotation)
            {
                case 0:
                    Data = new int[3, 3]
                    {
                        {0, 0, 0},
                        {2, 2, 2},
                        {0, 0, 2}
                    };
                    break;
                case 1:
                    Data = new int[3, 3]
                    {
                        {0, 2, 0},
                        {0, 2, 0},
                        {2, 2, 0}
                    };
                    break;
                case 2:
                    Data = new int[3, 3]
                    {
                        {2, 0, 0},
                        {2, 2, 2},
                        {0, 0, 0}
                    };
                    break;
                default:
                    Data = new int[3, 3]
                    {
                        {0, 2, 2},
                        {0, 2, 0},
                        {0, 2, 0}
                    };
                    break;
            }
        }
    }
    public class LBlock : BaseBlock
    {
        public LBlock(int x, int y) : base(x, y)
        {

        }

        public void InitializeBlock(int rotation)
        {
            Rotation = rotation;

            switch (Rotation)
            {
                case 0:
                    Data = new int[3, 3]
                    {
                        {0, 0, 0},
                        {3, 3, 3},
                        {3, 0, 0}
                    };
                    break;
                case 1:
                    Data = new int[3, 3]
                    {
                        {3, 3, 0},
                        {0, 3, 0},
                        {0, 3, 0}
                    };
                    break;
                case 2:
                    Data = new int[3, 3]
                    {
                        {0, 0, 3},
                        {3, 3, 3},
                        {0, 0, 0}
                    };
                    break;
                default:
                    Data = new int[3, 3]
                    {
                        {0, 3, 0},
                        {0, 3, 0},
                        {0, 3, 3}
                    };
                    break;
            }
        }
    }

    public class OBlock : BaseBlock
    {
        public OBlock(int x, int y) : base(x, y)
        {

        }

        public void InitializeBlock()
        {
            Data = new int[4, 4]
            {
                {0, 0, 0, 0 },
                {0, 4, 4, 0 },
                {0, 4, 4, 0 },
                {0, 0, 0, 0 }
            };
        }
    }

    public class SBlock : BaseBlock
    {
        public SBlock(int x, int y) : base(x, y)
        {

        }

        public void InitializeBlock(int rotate)
        {
            Rotation = rotate;
            // There are only 2 possible rotations for this block
            Rotation %= 2;

            switch (Rotation)
            {
                case 0:
                    Data = new int[3, 3]
                    {
                        {0, 0, 0},
                        {0, 5, 5},
                        {5, 5, 0}
                    };
                    break;
                default:
                    Data = new int[3, 3]
                    {
                        {5, 0, 0},
                        {5, 5, 0},
                        {0, 5, 0}
                    };
                    break;
            }
        }
    }

    public class ZBlock : BaseBlock
    {
        public ZBlock(int x, int y) : base(x, y)
        {

        }

        public void InitializeBlock(int rotate)
        {
            Rotation = rotate;

            // There are only 2 possible rotations for this block
            Rotation %= 2;

            switch (Rotation)
            {
                case 0:
                    Data = new int[3, 3]
                    {
                        {0, 0, 0},
                        {6, 6, 0},
                        {0, 6, 6}
                    };
                    break;
                default:
                    Data = new int[3, 3]
                    {
                        {0, 0, 6},
                        {0, 6, 6},
                        {0, 6, 0}
                    };
                    break;
            }
        }
    }

    public class TBlock : BaseBlock
    {
        public TBlock(int x, int y) : base(x, y)
        {

        }

        public void InitializeBlock(int rotate)
        {
            Rotation = rotate;

            switch (Rotation)
            {
                case 0:
                    Data = new int[3, 3]
                    {
                        {0, 0, 0},
                        {7, 7, 7},
                        {0, 7, 0}
                    };
                    break;
                case 1:
                    Data = new int[3, 3]
                    {   
                        {0, 7, 0},
                        {7, 7, 0},
                        {0, 7, 0}
                    };
                    break;
                case 2:
                    Data = new int[3, 3]
                    {
                        {0, 7, 0},
                        {7, 7, 7},
                        {0, 0, 0}
                    };
                    break;
                default:
                    Data = new int[3, 3]
                    {
                        {0, 7, 0},
                        {0, 7, 7},
                        {0, 7, 0}
                    };
                    break;
            }
        }
    }
}
