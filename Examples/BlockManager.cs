using Tringle;

namespace Program
{
    public class BlockManager
    {
        // Board at current frame
        public int[,] CurrentBoard;

        // Board with only static blocks
        public int[,] BaseBoard;

        private int m_currentBlock = 1;

        // Container for current block info
        private BaseBlock m_baseBlock;
        // Container for active block
        private BaseBlock m_block;

        // Only false when the current block is touching the ground or another block is below it
        private bool m_isFalling = false;

        private bool[] m_lastKeyState = new bool[1024];

        public Game.GameState State = Game.GameState.PLAYING;

        public enum Collision
        {
            LEFT,
            RIGHT,
            DOWN,
            UP
        }

        public BlockManager(int[,] board)
        {
            BaseBoard = board;
            CurrentBoard = new int[board.GetLength(0), board.GetLength(1)];
            Array.Copy(BaseBoard, CurrentBoard, board.GetLength(0) * board.GetLength(1));

            // Starting position for all blocks (just above visible window)
            m_baseBlock = new(3, -4);
        }

        public bool CheckCollisions(Collision collision)
        {
            switch (collision)
            {
                case Collision.LEFT:
                    for (int i = 0; i < m_block.Data.GetLength(0); i++)
                    {
                        for (int j = 0; j < m_block.Data.GetLength(1); j++)
                        {
                            if (m_block.Y + i < 0) continue;
                            if (m_block.Data[i, j] == 0) continue;
                            if (m_block.X + j - 1 < 0) return true;
                            if (BaseBoard[m_block.Y + i, m_block.X + j - 1] != 0) return true;
                        }
                    }
                    break;
                case Collision.RIGHT:
                    for (int i = 0; i < m_block.Data.GetLength(0); i++)
                    {
                        for (int j = 0; j < m_block.Data.GetLength(1); j++)
                        {
                            if (m_block.Y + i < 0) continue;
                            if (m_block.Data[i, j] == 0) continue;
                            if (m_block.X + j + 1 >= BaseBoard.GetLength(1)) return true;
                            if (BaseBoard[m_block.Y + i, m_block.X + j + 1] != 0) return true;
                        }
                    }
                    break;
                case Collision.DOWN:
                    for (int i = 0; i < m_block.Data.GetLength(0); i++)
                    {
                        for (int j = 0; j < m_block.Data.GetLength(1); j++)
                        {
                            if (m_block.Y + i < 0) continue;
                            if (m_block.Data[i, j] == 0) continue;
                            if (m_block.Y + i + 1 >= BaseBoard.GetLength(0)) return true;
                            if (BaseBoard[m_block.Y + i + 1, m_block.X + j] != 0) return true;
                        }
                    }
                    break;
                case Collision.UP:
                    if (m_block.Y + 1 < 0) return true;
                    break;
            }
            return false;
        }

        public bool CheckRotation()
        {
            BaseBlock currBlock = m_block;
            m_block.Rotation = (m_baseBlock.Rotation + 1) % 4;

            if(!CheckCollisions(Collision.DOWN) && !CheckCollisions(Collision.LEFT) && !CheckCollisions(Collision.RIGHT))
            {
                m_block = currBlock;
                return true;
            }

            m_block = currBlock;
            return false;
        }

        // Called once per second, moves block forward only if not colliding in the y-direction
        public void GetInput()
        {
            if (Input.CheckKey(Input.UpArrow) && !m_lastKeyState[(int)Input.UpArrow] && CheckRotation())
            {
                m_lastKeyState[(int)Input.UpArrow] = true;
                m_baseBlock.Rotation = (m_baseBlock.Rotation + 1) % 4;
            }

            if (Input.CheckKey(Input.RightArrow) && !m_lastKeyState[(int)Input.RightArrow] && !CheckCollisions(Collision.RIGHT))
            {
                m_lastKeyState[(int)Input.RightArrow] = true;
                m_baseBlock.X++;
            }

            if (Input.CheckKey(Input.LeftArrow) && !m_lastKeyState[(int)Input.LeftArrow] && !CheckCollisions(Collision.LEFT))
            {
                m_lastKeyState[(int)Input.LeftArrow] = true;
                m_baseBlock.X--;
            }

            if(!Input.CheckKey(Input.UpArrow)) m_lastKeyState[(int)Input.UpArrow] = false;
            if(!Input.CheckKey(Input.RightArrow)) m_lastKeyState[(int)Input.RightArrow] = false;
            if(!Input.CheckKey(Input.LeftArrow)) m_lastKeyState[(int)Input.LeftArrow] = false;

            if (Input.CheckKey(Input.Space) && !m_lastKeyState[(int)Input.Space])
            {
                m_lastKeyState[(int)Input.Space] = true;

                if (State == Game.GameState.PLAYING) State = Game.GameState.PAUSED;
                else if (State == Game.GameState.PAUSED) State = Game.GameState.PLAYING;
            }

            if (!Input.CheckKey(Input.Space)) m_lastKeyState[(int)Input.Space] = false;
        }

        public void UpdateBlock()
        {
            // Copy stationary blocks to active game board
            Array.Copy(BaseBoard, CurrentBoard, BaseBoard.GetLength(0) * BaseBoard.GetLength(1));

            switch (m_currentBlock)
            {
                case 1:
                    // IBlock
                    m_block = new IBlock(m_baseBlock.X, m_baseBlock.Y);
                    ((IBlock)m_block).InitializeBlock(m_baseBlock.Rotation);
                    break;
                case 2:
                    // JBlock
                    m_block = new JBlock(m_baseBlock.X, m_baseBlock.Y);
                    ((JBlock)m_block).InitializeBlock(m_baseBlock.Rotation);
                    break;
                case 3:
                    // LBlock
                    m_block = new LBlock(m_baseBlock.X, m_baseBlock.Y);
                    ((LBlock)m_block).InitializeBlock(m_baseBlock.Rotation);
                    break;
                case 4:
                    // OBlock
                    m_block = new OBlock(m_baseBlock.X, m_baseBlock.Y);
                    ((OBlock)m_block).InitializeBlock();
                    break;
                case 5:
                    // SBlock
                    m_block = new SBlock(m_baseBlock.X, m_baseBlock.Y);
                    ((SBlock)m_block).InitializeBlock(m_baseBlock.Rotation);
                    break;
                case 6:
                    // ZBlock
                    m_block = new ZBlock(m_baseBlock.X, m_baseBlock.Y);
                    ((ZBlock)m_block).InitializeBlock(m_baseBlock.Rotation);
                    break;
                case 7:
                    // TBlock
                    m_block = new TBlock(m_baseBlock.X, m_baseBlock.Y);
                    ((TBlock)m_block).InitializeBlock(m_baseBlock.Rotation);
                    break;
                default:
                    // Something is wrong, return an IBlock for good luck
                    m_block = new IBlock(m_baseBlock.X, m_baseBlock.Y);
                    ((IBlock)m_block).InitializeBlock(m_baseBlock.Rotation);
                    break;
            }

            // Check if block is touching ground / another block below
            if (CheckCollisions(Collision.DOWN))
            {
                m_isFalling = false;

                if (CheckCollisions(Collision.UP))
                {
                    State = Game.GameState.GAMEOVER;
                }
            }

            m_block.DrawBlock(CurrentBoard);

            if (!m_isFalling)
            {
                Array.Copy(CurrentBoard, BaseBoard, BaseBoard.GetLength(0) * BaseBoard.GetLength(1));

                // Generate next block
                Random r = new Random();
                m_currentBlock = r.Next(1, 8);

                // Reset XY
                m_baseBlock.X = 3;
                m_baseBlock.Y = -4;

                // set isFalling to true
                m_isFalling = true;
            }

            LineClear();
            m_baseBlock.Y++;
        }

        public void LineClear()
        {
            bool fullLine = true;
            Stack<int> lines = new(); 

            for(int i = 0; i < BaseBoard.GetLength(0); i++)
            {
                fullLine = true;

                for(int j = 0; j < BaseBoard.GetLength(1); j++)
                {
                    if(BaseBoard[i, j] == 0)
                    {
                        fullLine = false;
                        break;
                    }
                }

                if (fullLine) lines.Push(i);
            }

            for(int i = 0; i < lines.Count; i++)
            {
                int line = lines.Pop();

                for(int k = line; k > 0; k--)
                {
                    for(int j = 0; j < BaseBoard.GetLength(1); j++)
                    {
                        BaseBoard[k, j] = BaseBoard[k - 1, j];
                    }
                }
            }
        }
    }
}
