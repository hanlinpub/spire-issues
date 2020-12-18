using System.IO;
using Spire.Doc;
using Xunit;

namespace EquationFieldCorruption
{
    public class UnitTest1
    {
        public UnitTest1()
        {
            Directory.CreateDirectory("outputs");
        }

        [Fact]
        public void EquationField_Corruption_DocSource()
        {
            var doc = new Document("inputs/DocA.doc");
            doc.SaveToFile("outputs/DocA_EquationBecameCorrupted.doc");
        }
    }
}
