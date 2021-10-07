using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Interface;
using Xunit;

namespace DocEquationDistortion
{
    public class UnitTest1
    {
        private string[] Inputs => new[]
        {
            "db3b7a3b6eae4d3bba5efa17e9db1ead",
            "d742548a733a4e468d835510550efb67",
            "049d7156b70d41b3a92335b95a080c31",
            "dbb5116f3d5c4c869d1b0624f25d11d7",
            "fc77c05e2e9d4b20b8848187f8abdf83",
            "049fc6511d794a64a0db4288a236e6f6",
            "afad9bc025004b459994c5cde012abde",
            "d743e36e70ff46cfa426792db8090162",
            "9c7ad5f9756c4a959a4fd314ba9cb0c8",
            "d86d4451b8564f0aad369648c9b2942d",
            "bc8d5749330847c0b896c0550b2798f6",
            "807f483715ca4e7481c685727e560dcf",
        };

        [Fact]
        public void Test()
        {
            var random = new Random();
            var repeats = 10;

            for (int i = 0; i < repeats; i++)
            {
                using var outputDoc = new Document("Inputs/Template.docx");
                foreach (var input in random.Shuffle(Inputs))
                {
                    using var inputDoc = new Document($"Inputs/{input}.doc");
                    AppendItem(outputDoc, inputDoc);
                }
                outputDoc.SaveToFile($"Outputs/{i}-DocEquationDistortionTest.doc");
            }
        }

        private void AppendItem(Document baseDoc, Document itemDoc)
        {
            var baseTable = baseDoc.GetItemTable();
            var itemTable = itemDoc.GetItemTable();

            itemTable.TableFormat.HorizontalAlignment = RowAlignment.Left;
            baseTable.TableFormat.IsBreakAcrossPages = true;

            var insertingRow = itemTable.Rows[0].Clone();

            baseTable.Rows.Add(insertingRow);
        }
    }

    static class DocumentExtensions
    {
        public static ITable GetItemTable(this Document doc)
        {
            var tables = doc.Sections[0].Tables;

            if (tables.Count == 0)
            {
                throw new FormatException("Document does not contain item table");
            }
            else if (tables.Count == 1)
            {
                return doc.Sections[0].Tables[0];
            }
            else
            {
                return doc.Sections[0].Tables[1];
            }
        }
    }

    static class RandomExtensions
    {
        // https://stackoverflow.com/a/110570/494297
        public static T[] Shuffle<T>(this Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }

            return array;
        }
    }
}
