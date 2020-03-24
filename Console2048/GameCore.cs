using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2048
{
    class GameCore
    {
        private int[,] map;
        private int[] mergeArray;
        private int[] removeZeroArray;
        private List<Location> emptyLocations;
        private Random random;
        private int[,] originaLMap;
        private int[,] formerMap;

        public int[,] Map
        {
            get { return this.map; }
        }

        public int IsChange { get; set; }

        public GameCore()
        {
            map = new int[4, 4];
            mergeArray = new int[4];
            removeZeroArray = new int[4];
            emptyLocations = new List<Location>(16);
            random = new Random();
            originaLMap = new int[4, 4];
            formerMap = new int[4, 4];
        }

        private void RemoveZero()
        {
            int index = 0;
            Array.Clear(removeZeroArray, 0, 4);
            for (int i = 0; i < mergeArray.Length; i++)
            {
                if (mergeArray[i] != 0)
                {
                    removeZeroArray[index++] = mergeArray[i];
                }
            }
            removeZeroArray.CopyTo(mergeArray, 0);
        }

        private void Merge()
        {
            RemoveZero();

            for (int i = 0; i < mergeArray.Length - 1; i++)
            {
                if (mergeArray[i] != 0 && mergeArray[i] == mergeArray[i + 1])
                {
                    mergeArray[i] += mergeArray[i + 1];
                    mergeArray[i + 1] = 0;
                }
            }
            RemoveZero();
        }

        private void MoveUp()
        {
            for (int c = 0; c < map.GetLength(1); c++)
            {
                for (int r = 0; r < map.GetLength(0); r++)
                {
                    mergeArray[r] = map[r, c];
                }
                Merge();
                for (int r = 0; r < map.GetLength(0); r++)
                {
                    map[r, c] = mergeArray[r];
                }
            }
        }

        private void MoveDown()
        {
            for (int c = 0; c < map.GetLength(1); c++)
            {
                for (int r = map.GetLength(0) - 1; r >= 0; r--)
                {
                    mergeArray[3 - r] = map[r, c];
                }
                Merge();
                for (int r = map.GetLength(0) - 1; r >= 0; r--)
                {
                    map[r, c] = mergeArray[3-r];
                }
            }
        }

        private void MoveLeft()
        {
            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    mergeArray[c] = map[r, c];
                }
                Merge();
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    map[r, c] = mergeArray[c];
                }
            }
        }

        private void MoveRight()
        {
            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = map.GetLength(1) - 1; c >= 0; c--)
                {
                    mergeArray[3 - c] = map[r, c];
                }
                Merge();
                for (int c = map.GetLength(1) - 1; c >= 0; c--)
                {
                    map[r, c] = mergeArray[3-c];
                }
            }
        }

        private void MoveRetract()
        {
            Array.Copy(formerMap, map, formerMap.Length);
        }

        public void Move(MoveDirection direction)
        {
            Array.Copy(map, originaLMap, map.Length);
            IsChange = 0;

            if (direction == MoveDirection.Retract)
            {
                MoveRetract();
                IsChange = -1;
            }
            else
            {
                Array.Copy(map, formerMap, map.Length);
                switch (direction)
                {
                    case MoveDirection.Up:
                        MoveUp();
                        break;
                    case MoveDirection.Down:
                        MoveDown();
                        break;
                    case MoveDirection.Left:
                        MoveLeft();
                        break;
                    case MoveDirection.Right:
                        MoveRight();
                        break;
                }
                IsChange = CompareMap(originaLMap, map);
            }
        }

        private int CompareMap(int[,] original, int[,] current)
        {
            for (int r=0; r<map.GetLength(0); r++)
            {
                for (int c=0; c<map.GetLength(1); c++)
                {
                    if (map[r,c] != original[r,c])
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }

        #region number generator
        private void EmptyCollator()
        {
            emptyLocations.Clear();
            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    if (map[r, c] == 0)
                    {
                        emptyLocations.Add(new Location(r, c));
                    }
                }
            }
        }

        public void NumberGeneratation()
        {
            EmptyCollator();
            if (emptyLocations.Count > 0)
            {
                int randomIndex = random.Next(0, emptyLocations.Count);
                Location tempLocation = emptyLocations[randomIndex];
                map[tempLocation.RIndex, tempLocation.CIndex] = random.Next(0, 10) == 1 ? 4 : 2;
            }
        }
        #endregion



    }
}
