using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Monsters;
using TheMonsterFactory.DataCollection;

namespace TheMonsterFactory.BL.GameStructure
{
    public static class ChapterManager
    {
        public static Chapters Chapters { get; } = new();
        public static int WaveCounter { get; set; } = 0;

        public static List<Monster> GetWave(int chapterIndex)
        {
            List<Monster> wave = Chapters.ChapterList[chapterIndex].Waves[WaveCounter].WaveContent;
            WaveCounter++;

            return wave;
        }

        public static int GetWavesInChapter(int chapterIndex)
        {
            return Chapters.ChapterList[chapterIndex].Waves.Count;
        }
        public static int GetChapterCount()
        {
            return Chapters.ChapterList.Count;
        }
    }
}
