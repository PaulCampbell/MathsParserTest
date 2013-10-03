using System;
using NUnit;
using Shouldly;


namespace NiceScreen
{
	[TestFixture]
	public class MathsParserTests
	{

		[Test]
		public void CanAdd()
		{
			var parser = new MathsParser("3a2");

			var result = parser.Parse ();
			result.ShouldBe (5);
		}

	}
}

