using System.IO;
using Spire.Doc;
using Spire.Doc.Interface;
using Xunit;

namespace CharacterFormatLoss
{
    public class UnitTest1
    {
        public UnitTest1()
        {
            Directory.CreateDirectory("outputs");
        }

        /// <summary>
        /// The output document will contain proper character border and shading.
        /// </summary>
        [Fact]
        public void CloseAfter_CharacterFormatting_Preserved()
        {
            var docA = new Document("inputs/DocA.docx");
            var docB = new Document("inputs/DocB.docx");

            var copied = docB.Sections[0].Paragraphs[0].Clone();
            docA.Sections[0].Paragraphs.Add((IParagraph)copied);

            var path = $"outputs/{nameof(CloseAfter_CharacterFormatting_Preserved)}-output.docx";
            docA.SaveToFile(path);

            // Close document B after saving document A
            docB.Close();
            docA.Close();
        }

        /// <summary>
        /// The output document will lose all character border and shading formatting.
        /// </summary>
        [Fact]
        public void CloseBefore_CharacterFormatting_Lost()
        {
            var docA = new Document("inputs/DocA.docx");
            var docB = new Document("inputs/DocB.docx");

            var copied = docB.Sections[0].Paragraphs[0].Clone();
            docA.Sections[0].Paragraphs.Add((IParagraph)copied);

            // Close document B before saving document A.
            // Will cause document A to lose character border and shading.
            docB.Close();

            var path = $"outputs/{nameof(CloseBefore_CharacterFormatting_Lost)}-output.docx";
            docA.SaveToFile(path);

            docA.Close();
        }
    }
}
