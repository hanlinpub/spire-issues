using System;
using System.Threading.Tasks;
using Spire.Doc;
using Xunit;
using Xunit.Abstractions;

namespace Hanlin.SpireIssues
{
    public class DotUpdateTableOfContentsConcurrencyTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public DotUpdateTableOfContentsConcurrencyTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void SingleThread_WithoutFullwidthSymbol_IsOk()
        {
            try
            {
                using var doc = new Document("inputs/OkTemplate.dot");
                doc.UpdateTableOfContents();
            }
            catch (Exception e)
            {
                _outputHelper.WriteLine(e.Message);
                Assert.False(true, "Exception should not be thrown.");
            }
        }

        [Fact]
        public void SingleThread_WithFullwidthSymbol_IsOk()
        {
            try
            {
                using var doc = new Document("inputs/ErrorTemplate.dot");
                doc.UpdateTableOfContents();
            }
            catch (Exception e)
            {
                _outputHelper.WriteLine(e.Message);
                Assert.False(true, "Exception should not be thrown.");
            }
        }

        [Fact]
        public void Concurrent_WithoutFullwidthSymbol_IsOk()
        {
            Parallel.For(0, 10, (_, _) =>
            {
                try
                {
                    using var doc = new Document("inputs/OkTemplate.dot");
                    doc.UpdateTableOfContents();
                }
                catch (Exception e)
                {
                    _outputHelper.WriteLine(e.Message);
                    Assert.False(true, "Exception should not be thrown.");
                }
            });
        }

        // Always fails when run individually. Sometime fail when run with the whole project.
        [Fact]
        public void Concurrent_WithFullwidthSymbol_IsNotThreadSafe()
        {
            Parallel.For(0, 10, (_, _) =>
            {
                try
                {
                    using var doc = new Document("inputs/ErrorTemplate.dot");
                    doc.UpdateTableOfContents();
                }
                catch (Exception e)
                {
                    _outputHelper.WriteLine(e.Message);
                    Assert.False(true, "Exception should not be thrown.");
                }
            });
        }
    }
}