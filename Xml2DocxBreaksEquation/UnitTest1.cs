using System.IO;
using Spire.Doc;
using Xunit;

namespace Xml2DocxBreaksEquation
{
    public class UnitTest1
    {
        public UnitTest1()
        {
            Directory.CreateDirectory("test-outputs");
        }

        [Fact]
        public void OpenAndSave_BreaksEquation()
        {
            var doc = new Document("inputs/Equation.xml");
            doc.SaveToFile($"test-outputs/{nameof(OpenAndSave_BreaksEquation)}.docx");
        }
    }
}
