using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMonsterFactory.BL.GamePlayLogic.CreatureCreation.Monsters;
using TheMonsterFactory.BL.GameStructure;
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
            NameAndAdd(new Chapter // CHAPTER 1
            {
                RemainingRounds = 6,
                Waves =
                {
                    new Wave
                    {
                        WaveContent =
                        {
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
                            new Goblin(_names.GetRandomName(), 1),
                            new Bolethan(_names.GetRandomName(), 2),
                            new Spectre(_names.GetRandomName(), 2)
                        }
                    }
                }
            });

            NameAndAdd(new Chapter  // CHAPTER 2
            {
                RemainingRounds = 8,
                Waves =
                {
                    new Wave
                    {
                        WaveContent =
                        {
                            new Goblin(_names.GetRandomName(), 2),
                            new Goblin(_names.GetRandomName(), 2),
                            new Spectre(_names.GetRandomName(), 3)
                        }
                    },
                    new Wave
                    {
                        WaveContent =
                        {
                            new Goblin(_names.GetRandomName(), 1),
                            new Bolethan(_names.GetRandomName(), 2),
                            new Spectre(_names.GetRandomName(), 3),
                            new Goblin(_names.GetRandomName(), 2)
                        }
                    },
                    new Wave
                    {
                        WaveContent =
                        {
                            new Goblin(_names.GetRandomName(), 2),
                            new Bolethan(_names.GetRandomName(), 2),
                            new Bolethan(_names.GetRandomName(), 2),
                            new Spectre(_names.GetRandomName(), 2)
                        }
                    },
                    new Wave
                    {
                        WaveContent =
                        {
                            new Vileblade(_names.GetRandomName(), 3),
                            new Spectre(_names.GetRandomName(), 2)
                        }
                    }
                }
            });

            NameAndAdd(new Chapter  // CHAPTER 3
            {
                RemainingRounds = 7,
                Waves =
                {
                    new Wave
                    {
                        WaveContent =
                        {
                            new Goblin(_names.GetRandomName(), 2),
                            new Goblin(_names.GetRandomName(), 2),
                            new Goblin(_names.GetRandomName(), 2),
                            new Goblin(_names.GetRandomName(), 2),
                            new Goblin(_names.GetRandomName(), 2)
                        }
                    },
                    new Wave
                    {
                        WaveContent =
                        {
                            new Bolethan(_names.GetRandomName(), 3),
                            new Bolethan(_names.GetRandomName(), 3),
                            new Bolethan(_names.GetRandomName(), 3),
                            new Vileblade(_names.GetRandomName(), 3),
                        }
                    },
                    new Wave
                    {
                        WaveContent =
                        {
                            new Vileblade(_names.GetRandomName(), 4),
                            new Spectre(_names.GetRandomName(), 3),
                            new Spectre(_names.GetRandomName(), 3),
                            new Spectre(_names.GetRandomName(), 3)
                        }
                    }
                }
            });
            NameAndAdd(new Chapter  // CHAPTER 4
            {
                RemainingRounds = 5,
                Waves =
                {
                    new Wave
                    {
                        WaveContent =
                        {
                            new Bolethan(_names.GetRandomName(), 5),
                        }
                    },
                    new Wave
                    {
                        WaveContent =
                        {
                            new Goblin(_names.GetRandomName(), 4),
                            new Goblin(_names.GetRandomName(), 4),
                            new Bolethan(_names.GetRandomName(), 4),
                            new Spectre(_names.GetRandomName(), 6),
                        }
                    },
                    new Wave
                    {
                        WaveContent =
                        {
                            new Vileblade(_names.GetRandomName(), 4),
                            new Vileblade(_names.GetRandomName(), 4),
                            new Vileblade(_names.GetRandomName(), 4),
                            new Spectre(_names.GetRandomName(), 5)
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
