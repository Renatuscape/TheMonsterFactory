using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.GameStructure;
using TheMonsterFactory.BL.Monsters;
using TheMonsterFactory.DataAccess;

namespace TheMonsterFactory.DataCollection
{
    public class Chapters
    {
        NameGenerator _names = new();
        public List<Chapter> ChapterList { get; set; } = new();

        public Chapters()
        {
            CreateChapters();
        }

        public void CreateChapters()
        {
            NameAndAdd(new Chapter
            {
                Waves =
                {
                    new Wave
                    {
                        WaveContent =
                        {
                            new Goblin(_names.GetRandomName(), 1),
                            new Goblin(_names.GetRandomName(), 1),
                            new Goblin(_names.GetRandomName(), 1),
                            new Goblin(_names.GetRandomName(), 2)
                        }
                    },
                    new Wave
                    {
                        WaveContent =
                        {
                            new Goblin(_names.GetRandomName(), 1),
                            new Goblin(_names.GetRandomName(), 1),
                            new Spectre(_names.GetRandomName(), 1),
                            new Goblin(_names.GetRandomName(), 2)
                        }
                    },
                    new Wave
                    {
                        WaveContent =
                        {
                            new Goblin(_names.GetRandomName(), 1),
                            new Goblin(_names.GetRandomName(), 2),
                            new Bolethan(_names.GetRandomName(), 2),
                            new Spectre(_names.GetRandomName(), 2)
                        }
                    }
                }
            });

            NameAndAdd(new Chapter
            {
                Waves =
                {
                    new Wave
                    {
                        WaveContent =
                        {
                            new Goblin(_names.GetRandomName(), 2),
                            new Goblin(_names.GetRandomName(), 2),
                            new Goblin(_names.GetRandomName(), 2),
                            new Spectre(_names.GetRandomName(), 2)
                        }
                    },
                    new Wave
                    {
                        WaveContent =
                        {
                            new Goblin(_names.GetRandomName(), 2),
                            new Bolethan(_names.GetRandomName(), 2),
                            new Spectre(_names.GetRandomName(), 3),
                            new Goblin(_names.GetRandomName(), 2)
                        }
                    },
                    new Wave
                    {
                        WaveContent =
                        {
                            new Goblin(_names.GetRandomName(), 3),
                            new Bolethan(_names.GetRandomName(), 2),
                            new Bolethan(_names.GetRandomName(), 2),
                            new Spectre(_names.GetRandomName(), 2)
                        }
                    }
                }
            });
        }

        public void NameAndAdd(Chapter chapter)
        {
            ChapterList.Add(chapter);
            chapter.ChapterName = $"Chapter {ChapterList.Count}";
        }
    }
}
