using System.IO;
using Spire.Doc;
using Xunit;

namespace EquationBecomesImage
{
    public class UnitTest1
    {
        public UnitTest1()
        {
            Directory.CreateDirectory("test-outputs");
        }

        /// <summary>
        /// Manual verification: Open the output .docx file and notice that the equation has become an image.
        /// </summary>
        [Fact]
        public void Spire_8_10_0_AndAbove_OpenAndSaveInDocx_EquationBecomesImage()
        {
            var doc = new Document("test-inputs/Equation.doc");
            doc.SaveToFile($"test-outputs/{nameof(Spire_8_10_0_AndAbove_OpenAndSaveInDocx_EquationBecomesImage)}.docx");
        }
    }
}