using System.Xml.Linq;
using Spire.Doc;
using Spire.Doc.Interface;

namespace TableFormatLoss;

public class UnitTest1
{
    public UnitTest1()
    {
        Directory.CreateDirectory("outputs");
    }

    private Document GetTemplateDocument()
    {
        MemoryStream streamRes = new MemoryStream();
        FileStream fileStream = new FileStream($"inputs/template.doc", FileMode.Open, FileAccess.Read, FileShare.Read);
        fileStream.CopyTo(streamRes);
        fileStream.Flush();
        fileStream.Close();

        return new Document(streamRes);
    }

    private void CopyTo(Document source, Document target)
    {
        foreach (Section section in source.Sections)
        {
            var targetSection = target.AddSection(); ;
            foreach (DocumentObject child in section.Body.ChildObjects)
            {
                var copied = child.Clone();
                targetSection.Body.ChildObjects.Add(copied);
            }
        }
    }

    [Fact]
    public void DisposeAfter_TableFormatting_Preserved()
    {
        var docA = GetTemplateDocument();
        var docB = new Document("inputs/TableWidthLoss.doc");

        CopyTo(docB, docA);

        var path = $"outputs/{nameof(DisposeAfter_TableFormatting_Preserved)}-output.doc";

        docA.SaveToFile(path);

        docB.Dispose();
        docB.Close();
        docA.Close();
    }

    [Fact]
    public void DisposeBefore_TableFormatting_Lost()
    {
        var docA = GetTemplateDocument();
        var docB = new Document("inputs/TableWidthLoss.doc");

        CopyTo(docB, docA);

        docB.Dispose();

        var path = $"outputs/{nameof(DisposeBefore_TableFormatting_Lost)}-output.doc";

        docA.SaveToFile(path);

        docA.Close();
        docB.Close();

    }
}