using System.IO;
using System.Runtime.InteropServices;
using FluentAssertions;
using Microsoft.Office.Interop.Word;
using Xunit;
using Document = Spire.Doc.Document;

namespace Issue_006_MathTypeSaveCorruption
{
    /// <summary>
    /// This issue is likely not a big in Spire.Doc as the behaviour is reproducible with the Office Word application.
    /// That is, manually saving the document in question as .docx using Word would cause the same exception to be thrown.
    /// The problem does not occur when the document is saved as .doc.
    /// </summary>
    public class Issue006
    {
        private string OutputPath { get; } = $"test-outputs/{nameof(Issue006)}";

        public Issue006()
        {
            Directory.CreateDirectory(OutputPath);
        }

        [Fact]
        public void CorruptedMathTypeEquation_SaveDocumentAsDocx_CallToWordOpenXMLWillNoLongerWork()
        {
            var inputPath = "test-inputs/CH7594-Corrupted MathType Equation.doc";

            this.Invoking(y => y.GetOpenXml(inputPath)).Should().NotThrow("before save call to WordOpenXML still works");

            var outputFilePath = $"{OutputPath}/{nameof(CorruptedMathTypeEquation_SaveDocumentAsDocx_CallToWordOpenXMLWillNoLongerWork)}.docx";
            using var doc = new Document(inputPath);
            doc.SaveToFile(outputFilePath);

            this.Invoking(y => y.GetOpenXml(outputFilePath)).Should().ThrowExactly<COMException>("after save call to WordOpenXML no longer works");
        }

        [Fact]
        public void CorruptedMathTypeEquation_SaveDocumentAsDoc_CallToWordOpenXMLWillNoLongerWork()
        {
            var inputPath = "test-inputs/CH7594-Corrupted MathType Equation.doc";

            this.Invoking(y => y.GetOpenXml(inputPath)).Should().NotThrow("before save call to WordOpenXML still works");

            var outputFilePath = $"{OutputPath}/{nameof(CorruptedMathTypeEquation_SaveDocumentAsDoc_CallToWordOpenXMLWillNoLongerWork)}.doc";
            using var doc = new Document(inputPath);
            doc.SaveToFile(outputFilePath);

            this.Invoking(y => y.GetOpenXml(outputFilePath)).Should().NotThrow("call to WordOpenXML still works as document is saved as .doc");
        }

        private string GetOpenXml(string filePath)
        {
            Application word = null;
            Microsoft.Office.Interop.Word.Document doc = null;
            string openXml;
            try
            {
                word = new Application()
                {
                    DisplayAlerts = WdAlertLevel.wdAlertsNone
                };

                doc = word.Documents.Open(Directory.GetCurrentDirectory() + "/" + filePath);

                openXml = doc.InlineShapes[2].Range.WordOpenXML;

            }
            finally
            {
                doc?.Close();
                word?.Quit();
            }

            return openXml;
        }
    }
}