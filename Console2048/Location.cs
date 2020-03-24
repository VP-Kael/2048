using System;

namespace Console2048
{
    struct Location
    {
        public int RIndex { get; set; }

        public int CIndex { get; set; }

        public Location(int rIndex, int cIndex):this()
        {
            this.RIndex = rIndex;
            this.CIndex = cIndex;
        }
    }
}
