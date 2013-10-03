using System;
using NUnit.Framework;
using Shouldly;

namespace NiceScreen.Tests
{
	[TestFixture]
	public class AcceptanceTests
	{
		[TestCase("3a2c4", 20)]
		[TestCase("32a2d2",17)]
		[TestCase("500a10b66c32",14208)]
		[TestCase("3ae4c66fb32",235)]
		[TestCase("3c4d2aee2a4c41fc4f",990)]
		public void RunAcceptanceTests(string inputString, double result)
		{
			var parser = new MathsParser(inputString);

			var output = parser.Parse ();
			output.ShouldBe(result);
		}
	}
}

