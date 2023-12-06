using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMonsterFactory.BL.GameStructure
{
    public class Chapter
    {
        public string ChapterName { get; set; }
        public int RemainingRounds { get; set; } = 10;
        public List<Wave> Waves { get; set; } = new();

        public Chapter(string chapterName = "")
        {
            ChapterName = chapterName;
        }
    }
}
