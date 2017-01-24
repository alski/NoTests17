using TrackMyMedicine.ViewModel;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework.AppContainer;
using Should;

namespace UnitTestProject.Tests
{
    [TestClass]
    public class ColorParseTests
    {
        [UITestMethod]
        public void ShouldConvertHexChars()
        {
            Hex.Parse('0').ShouldEqual(0);
            Hex.Parse('1').ShouldEqual(1);
            Hex.Parse('2').ShouldEqual(2);
            Hex.Parse('3').ShouldEqual(3);
            Hex.Parse('4').ShouldEqual(4);
            Hex.Parse('5').ShouldEqual(5);
            Hex.Parse('6').ShouldEqual(6);
            Hex.Parse('7').ShouldEqual(7);
            Hex.Parse('8').ShouldEqual(8);
            Hex.Parse('9').ShouldEqual(9);
            Hex.Parse('a').ShouldEqual(10);
            Hex.Parse('b').ShouldEqual(11);
            Hex.Parse('c').ShouldEqual(12);
            Hex.Parse('d').ShouldEqual(13);
            Hex.Parse('e').ShouldEqual(14);
            Hex.Parse('f').ShouldEqual(15);
            Hex.Parse('A').ShouldEqual(10);
            Hex.Parse('B').ShouldEqual(11);
            Hex.Parse('C').ShouldEqual(12);
            Hex.Parse('D').ShouldEqual(13);
            Hex.Parse('E').ShouldEqual(14);
            Hex.Parse('F').ShouldEqual(15);
        }
    }
}