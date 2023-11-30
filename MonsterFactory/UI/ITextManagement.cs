using MonsterFactory.UI;

public interface ITextManagement : IRead, IWrite, IContinuePrompt
{
    public void WriteColour(string text, ColourTag colourTag, bool isNewLine = true);
}
