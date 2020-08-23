using System.IO;
using Spire.Doc;
using Xunit;

namespace SpireIssues
{
    public class UnitTest1
    {
        public UnitTest1()
        {
            Directory.CreateDirectory("test-outputs");
        }

        [Fact]
        public void OpenAndSave_DoesNotBreakEquation()
        {
            var doc = new Document("inputs/Equation.doc");
            doc.SaveToFile($"test-outputs/{nameof(OpenAndSave_DoesNotBreakEquation)}.docx");
        }

        [Fact]
        public void DoubleOpenAndSave_BreaksEquation_Doc()
        {
            var doc = new Document("inputs/Equation.doc", FileFormat.Doc);
            var ms = new MemoryStream();
            doc.SaveToStream(ms, FileFormat.Docx);
            var reopen = new Document(ms, FileFormat.Docx);
            reopen.SaveToFile($"test-outputs/{nameof(DoubleOpenAndSave_BreaksEquation_Doc)}.docx", FileFormat.Docx);
        }

        [Fact]
        public void DoubleOpenAndSave_BreaksEquation_Docx()
        {
            var doc = new Document("inputs/Equation.docx", FileFormat.Docx);
            var ms = new MemoryStream();
            doc.SaveToStream(ms, FileFormat.Docx);
            var reopen = new Document(ms, FileFormat.Docx);
            reopen.SaveToFile($"test-outputs/{nameof(DoubleOpenAndSave_BreaksEquation_Docx)}.docx", FileFormat.Docx);
        }
    }
}
